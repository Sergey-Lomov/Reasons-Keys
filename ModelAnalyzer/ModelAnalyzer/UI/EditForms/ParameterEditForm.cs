using System.Windows.Forms;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI
{
    public class ParameterEditForm : Form
    {
        virtual internal bool IsParameterUpdateNecessary() { return false; }
        virtual internal bool IsModelUpdateNecessary() { return false; }

        virtual internal void SetParameter(Parameter p) { }
        virtual internal Parameter GetParameter() { return null;}
    }
}
