using System;

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

            var pa = (int)RequestParameter<PhasesAmount>(calculator).GetValue();
            var pd = RequestParameter<PhasesDuration>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float roundNodesAtRadius(float r) => 6 * r;
            float roundNodesAtPhase(float p) => pd[(int)p] * MathAdditional.Sum((int)p, pa - 1, roundNodesAtRadius);
            value = unroundValue = MathAdditional.Sum(0, pa - 1, roundNodesAtPhase);

            return calculationReport;
        }
    }
}
