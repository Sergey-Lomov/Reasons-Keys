using System.Windows.Forms;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI
{
    internal interface IParameterRowDelegate
    {
        void HandleValueClick(Parameter parameter);
        void HandleTitleClick(Parameter parameter);
    }
}
