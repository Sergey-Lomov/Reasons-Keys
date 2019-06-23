using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ModelAnalyzer
{
    public partial class MainForm : Form, IParameterRowDelegate
    {
        private readonly string fileDialogTitle = "Выберите файл";

        Storage storage = new Storage();
        Calculator calculator = new Calculator();
        Validator validator = new Validator();
        FilesManager filesManager = new FilesManager();

        public MainForm()
        {
            InitializeComponent();
            MainLayout.RowStyles.Clear();
            MainLayout.HorizontalScroll.Visible = false;

            new ParametersFactory().LoadModel(storage);
            try
            { 
                filesManager.ReadValuesFromDefault(storage);
                Calculate(false);
            }
            catch (MAException ex)
            {
                MessageBox.Show(ex.Message);
            }

            ReloadTable();
        }

        private void SaveToFile(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = filesManager.fileFilter,
                Title = fileDialogTitle
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = System.IO.Path.GetFullPath(saveFileDialog.FileName);
                filesManager.WriteValuesToFile(storage, path);
            }
        }

        private void LoadFromFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = filesManager.fileFilter,
                Title = fileDialogTitle
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            { 
                string path = System.IO.Path.GetFullPath(openFileDialog.FileName);
                try
                {
                    filesManager.ReadValuesFromFile(storage, path);
                }
                catch (MAException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            ReloadTable();
        }

        private void ReloadTable ()
        {
            MainLayout.Visible = false;
            MainLayout.Controls.Clear();
            MainLayout.RowCount = 0;
            var factory = new UIFactory();

            Dictionary<ParameterType, CheckBox> typesBoxes = new Dictionary<ParameterType, CheckBox> {
                {ParameterType.In, inCB},
                {ParameterType.Out, outCB},
                {ParameterType.Inner, innerCB},
                {ParameterType.Indicator, indicatorCB}};

            ParameterType[] prioritized = {ParameterType.Indicator, ParameterType.Out, ParameterType.In, ParameterType.Inner};
            string titleFilter = titleFilterTB.Text.Length > 0 ? titleFilterTB.Text : null;

            foreach (ParameterType type in prioritized)
            {
                if (typesBoxes[type].Checked)
                {
                    List<Parameter> parameters = storage.Parameters(new ParameterType[] {type}, titleFilter);
                    if (parameters.Count == 0)
                        continue;

                    MainLayout.RowCount += parameters.Count;

                    if (titleFilter == null)
                    {
                        MainLayout.RowCount += 1;
                        Panel header = factory.HeaderForParameterType(type);
                        MainLayout.Controls.Add(header);
                    }

                    foreach (Parameter parameter in parameters)
                    {
                        var validation = parameter.Validate(validator, storage);
                        Panel row = factory.RowForParameter(parameter, this, validation);
                        MainLayout.Controls.Add(row);
                    }
                }
            }

            Invalidate(true);
            MainLayout.Visible = true;
        }

        private void Calculate(bool showReport = true)
        {
            try
            {
                var calculationReport = calculator.CalculateModel(storage);
                var validationReport = validator.ValidateModel(storage);
                ReloadTable();

                if (showReport)
                {
                    CalculationReportForm report = new CalculationReportForm();
                    report.SetData(validationReport, calculationReport);
                    report.Show();
                }

                filesManager.WriteValuesToDefault(storage);
            }
            catch (MAException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Calculate(object sender, EventArgs e)
        {
            Calculate();
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            ReloadTable();
        }

        public void HandleValueClick(Parameter parameter, Label valueLabel)
        {
            EditForm edit = new EditForm() { TopLevel = true };

            edit.SetValue(parameter.ValueToString());
            if (edit.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    parameter.SetupByString(edit.GetValue());
                    if (edit.calculation)
                    {
                        Calculate(edit, null);
                    }
                    else
                    {
                        valueLabel.Text = parameter.ValueToString();
                    }
                }
                catch (MAException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public void HandleTitleClick(Parameter parameter, Label titleLabel)
        {
            ParameterDetailsForm details = new ParameterDetailsForm() { TopLevel = true };
            ParameterValidationReport validation = parameter.Validate(validator, storage);
            details.SetParameter(parameter, validation);
            details.ShowDialog();
        }
    }
}
