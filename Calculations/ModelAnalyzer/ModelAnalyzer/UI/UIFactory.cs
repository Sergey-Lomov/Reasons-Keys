using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters;
using ModelAnalyzer.Parameters.Topology;

using ModelAnalyzer.UI.Factories;
using ModelAnalyzer.UI.DetailsForms;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.UI
{
    class UIFactory
    {
        readonly Dictionary<Type, Type> detailsFormsTypes = new Dictionary<Type, Type>();

        private readonly ParametersRowsFactory parameters = new ParametersRowsFactory();
        private readonly TimingRowsFactory timings = new TimingRowsFactory();
        private readonly CalculationReportRowsFactory calculation = new CalculationReportRowsFactory();
        private readonly EditFormsFactory editForms = new EditFormsFactory();

        public UIFactory()
        {
            detailsFormsTypes.Add(typeof(FloatArrayParameter), typeof(DigitalParameterDetailsForm));
            detailsFormsTypes.Add(typeof(FloatSingleParameter), typeof(DigitalParameterDetailsForm));
            detailsFormsTypes.Add(typeof(BoolParameter), typeof(BoolDetailsForm));
            detailsFormsTypes.Add(typeof(RoutesMap), typeof(DigitalParameterDetailsForm));
            detailsFormsTypes.Add(typeof(RelationTemplatesUsage), typeof(DigitalParameterDetailsForm));
            detailsFormsTypes.Add(typeof(PairsArrayParameter), typeof(PairsArrayDetailsForm));
            detailsFormsTypes.Add(typeof(DeckParameter), typeof(EventsDeckDetailsForm));
            detailsFormsTypes.Add(typeof(FieldNodesFloatParameter), typeof(FieldNodesDetailsForm));
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

        public Panel RowForUnusedParameter(Parameter parameter)
        {
            return parameters.RowForParameter(parameter, null, null);
        }

        public Panel RowForTiming(string title, double duration)
        {
            return timings.RowFor(title, duration);
        }

        public Panel RowForTiming(OperationReport report)
        {
            return timings.RowForReport(report);
        }

        // Calculation results
        public Panel HeaderForGadeGroup(int grade)
        {
            return calculation.HeaderForGadeGroup(grade);
        }

        public Panel HeaderForIssues(string title)
        {
            return calculation.HeaderForIssues(title);
        }

        public Panel RowForReport(OperationReport report)
        {
            return calculation.RowForReport(report);
        }

        public Panel ParameterComparasionRow(Parameter left, Parameter right)
        {
            return calculation.ParameterComparasionRow(left, right);
        }
    }
}
