using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.Parameters.Events
{
    using BranchPiar = ValueTuple<int, int>;

    class BranchPointsAllocation_Symmetric : BranchPointsAllocation
    {
        public BranchPointsAllocation_Symmetric()
        {
            type = ParameterType.Inner;
            title = "Распределение очков ветвей (симметричная вариация)";
            details = "Очки ветвей на картах располагаются согласно набору правил, которые подробно описанны в основном документе по механике";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            return Calculate(calculator, false);
        }
    }
}
