using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI
{
    public partial class MainForm : Form, IParameterRowDelegate
    {
        private readonly string fileDialogTitle = "Выберите файл";

        Storage storage = new Storage();
        Calculator calculator = new Calculator();
        Validator validator = new Validator();
        FilesManager filesManager = new FilesManager();
        UIFactory uiFactory = new UIFactory();

        private Dictionary<Parameter, Panel> parametersRows = new Dictionary<Parameter, Panel>();

        public MainForm()
        {
            InitializeComponent();
            MainLayout.RowStyles.Clear();
            MainLayout.HorizontalScroll.Enabled = false;

            new ModelFactory().LoadModel(storage);
            UpdateTagsPanel();
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

        private void UpdateTagsPanel ()
        {
            var tags = storage.UniquesTags();

            tagsCLB.Items.Clear();
            foreach (var tag in tags.OrderBy(t => t.title))
                tagsCLB.Items.Add(tag, true);
        }

        private void ReloadTable ()
        {
            MainLayout.AutoScroll = false;
            MainLayout.SuspendLayout();

            MainLayout.Controls.Clear();
            MainLayout.RowCount = 0;
            parametersRows.Clear();

            Dictionary<ParameterType, CheckBox> typesBoxes = new Dictionary<ParameterType, CheckBox> {
                {ParameterType.In, inCB},
                {ParameterType.Out, outCB},
                {ParameterType.Inner, innerCB},
                {ParameterType.Indicator, indicatorCB}};

            var allTags = storage.UniquesTags();
            var enabledTags = allTags.Where(t => tagsCLB.CheckedItems.Contains(t)).ToList();

            ParameterType[] prioritized = {ParameterType.Indicator, ParameterType.Out, ParameterType.In, ParameterType.Inner};
            string titleFilter = titleFilterTB.Text.Length > 0 ? titleFilterTB.Text : null;

            foreach (ParameterType type in prioritized)
            {
                if (typesBoxes[type].Checked)
                {
                    List<Parameter> parameters = storage.Parameters(new ParameterType[] {type}, enabledTags, titleFilter);
                    if (parameters.Count == 0)
                        continue;

                    MainLayout.RowCount += parameters.Count;

                    if (titleFilter == null)
                    {
                        MainLayout.RowCount += 1;
                        Panel header = uiFactory.HeaderForParameterType(type);

                        if (MainLayout.Controls.Count == 0)
                            header.Margin = new Padding(header.Margin.Left, 0, header.Margin.Right, header.Margin.Bottom);

                        MainLayout.Controls.Add(header);
                    }

                    foreach (Parameter parameter in parameters)
                    {
                        var validation = parameter.Validate(validator, storage);
                        Panel row = uiFactory.RowForParameter(parameter, this, validation);
                        MainLayout.Controls.Add(row);
                        parametersRows[parameter] = row;
                    }
                }
            }

            Invalidate(true);
            MainLayout.ResumeLayout(true);
            MainLayout.AutoScroll = true;
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

        public void HandleValueClick(Parameter parameter)
        {
            if (parameter.type != ParameterType.In)
                return;

            ParameterEditForm edit = uiFactory.EditFormForParameter(parameter);
            edit.TopLevel = true;

            try
            {
                edit.ShowDialog();

                if (edit.IsModelUpdateNecessary())
                {
                    Calculate(edit, null);
                }
                else if (edit.IsParameterUpdateNecessary())
                {
                    var row = parametersRows[parameter];
                    var validation = parameter.Validate(validator, storage);
                    var newRow = uiFactory.RowForParameter(parameter, this, validation);

                    var rowIndex = MainLayout.GetPositionFromControl(row);

                    MainLayout.SuspendLayout();
                    MainLayout.Controls.Remove(row);
                    MainLayout.Controls.Add(newRow, rowIndex.Column, rowIndex.Row);
                    MainLayout.ResumeLayout();

                    parametersRows[parameter] = newRow;
                }
            }
            catch (MAException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void HandleTitleClick(Parameter parameter)
        {
            ParameterValidationReport validation = parameter.Validate(validator, storage);
            Form details = uiFactory.DetailsFormForParameter(parameter, validation);
            if (details != null)
            {
                details.TopLevel = true;
                details.Text = parameter.title;
                details.Show();
            }
        }

        private void tagsCLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadTable();
        }

        private void uncheckAllTagsButton_Click(object sender, EventArgs e)
        {
            foreach (int i in tagsCLB.CheckedIndices)
                tagsCLB.SetItemCheckState(i, CheckState.Unchecked);

            ReloadTable();
        }

        private void checkAllTagsButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tagsCLB.Items.Count; i++)
                tagsCLB.SetItemCheckState(i, CheckState.Checked);

            ReloadTable();
        }
    }
}
