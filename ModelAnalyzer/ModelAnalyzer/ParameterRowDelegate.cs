using System;
using System.Windows.Forms;

namespace ModelAnalyzer
{
    internal interface IParameterRowDelegate
    {
        void HandleValueClick(Parameter parameter, Label valueLabel);
        void HandleTitleClick(Parameter parameter, Label titleLabel);
    }
}
