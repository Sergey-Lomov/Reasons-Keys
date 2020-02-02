using System;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Events
{
    class BranchPointsAllocation_Standard : BranchPointsAllocation
    {
        public BranchPointsAllocation_Standard()
        {
            type = ParameterType.Inner;
            title = "Распределение очков ветвей (основная вариация)";
            details = "Очки ветвей на картах располагаются согласно набору правил, которые подробно описанны в основном документе по механике";
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            return Calculate(calculator, true);
        }
    }
}
