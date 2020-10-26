using System;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Events
{
    class BranchPointsAllocation_Symmetric : BranchPointsAllocation
    {
        public BranchPointsAllocation_Symmetric()
        {
            type = ParameterType.Inner;
            title = "Распределение очков ветвей (симметричная вариация)";
            details = "Очки ветвей на картах располагаются согласно набору правил, которые подробно описанны в основном документе по механике";
            tags.Add(ParameterTag.branchPoints);
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            return Calculate(calculator, false);
        }
    }
}
