using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI
{
    public partial class UnavailableEditingForm : ParameterEditForm
    {
        private Parameter parameter;

        public UnavailableEditingForm()
        {
            InitializeComponent();
        }

        internal override bool IsModelUpdateNecessary()
        {
            return false;
        }

        internal override bool IsParameterUpdateNecessary()
        {
            return false;
        }

        internal override void SetParameter(Parameter p)
        {
            parameter = p;
        }

        internal override Parameter GetParameter()
        {
            return parameter;
        }
    }
}
