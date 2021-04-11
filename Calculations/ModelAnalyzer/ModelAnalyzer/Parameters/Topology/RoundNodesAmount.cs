﻿using System;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Topology
{
    class RoundNodesAmount : FloatSingleParameter
    {
        public RoundNodesAmount()
        {
            type = ParameterType.Inner;
            title = "Кол-во раунд-узлов";
            details = "Общее кол-во всех узлов во всех раундах";
            fractionalDigits = 0;
            tags.Add(ParameterTag.timing);
            tags.Add(ParameterTag.topology);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var pa = (int)RequestParmeter<PhasesAmount>(calculator).GetValue();
            var pd = RequestParmeter<PhasesDuration>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            Func<float, double> roundNodesAtRadius = r => 6 * r;
            Func<float, double> roundNodesAtPhase = p => pd[(int)p] * MathAdditional.sum((int)p, pa - 1, roundNodesAtRadius);
            value = unroundValue = (float)MathAdditional.sum(0, pa - 1, roundNodesAtPhase);

            return calculationReport;
        }
    }
}