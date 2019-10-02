using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using ModelAnalyzer.Parameters;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.Topology;

using ModelAnalyzer.UI.Factories;

namespace ModelAnalyzer.UI
{
    class UIFactory
    {
        Dictionary<Type, Type> detailsFormsTypes = new Dictionary<Type, Type>();

        private readonly ParametersRowsFactory parameters = new ParametersRowsFactory();
        private readonly CalculationReportRowsFactory calculation = new CalculationReportRowsFactory();
        private readonly EditFormsFactory editForms = new EditFormsFactory();

        public UIFactory()
        {
            detailsFormsTypes.Add(typeof(FloatArrayParameter), typeof(DigitalParameterDetailsForm));
            detailsFormsTypes.Add(typeof(FloatSingleParameter), typeof(DigitalParameterDetailsForm));
            detailsFormsTypes.Add(typeof(RoutesMap), typeof(DigitalParameterDetailsForm));   
            detailsFormsTypes.Add(typeof(PairsArrayParameter), typeof(PairsArrayDetailsForm));
            detailsFormsTypes.Add(typeof(DeckParameter), typeof(EventsDeckDetailsForm));
        }

        public ParameterEditForm EditFormForParameter (Parameter parameter)
        {
            return editForms.EditFormForParameter(parameter);
        }

        public Form DetailsFormForParameter (Parameter parameter, ParameterValidationReport validation)
        {
            var formTypes = detailsFormsTypes.Where(pt => pt.Key.IsAssignableFrom(parameter.GetType()));
            if (formTypes.Count() == 0)
                return null;

            var formType = formTypes.First().Value;
            if (typeof(IParameterDetailsForm).IsAssignableFrom(formType) && formType.IsSubclassOf(typeof(Form)))
            {
                var form = (IParameterDetailsForm)Activator.CreateInstance(formType);
                form.SetParameter(parameter, validation);
                return (Form)form;
            }

            return null;
        }

        // Parameters table
        public Panel HeaderForParameterType(ParameterType type)
        {
            return parameters.HeaderForParameterType(type);
        }

        public Panel RowForParameter(Parameter parameter, IParameterRowDelegate rowDelegate, ParameterValidationReport validation)
        {
            return parameters.RowForParameter(parameter, rowDelegate, validation);
        }

        // Calculatino results
        public Panel HeaderForIssues(string title)
        {
            return calculation.HeaderForIssues(title);
        }

        public Panel RowForValidationReport(ParameterValidationReport report)
        {
            return calculation.RowForValidationReport(report);
        }

        public Panel RowForCalculationReport(ParameterCalculationReport report)
        {
            return calculation.RowForCalculationReport(report);
        }
    }
}
