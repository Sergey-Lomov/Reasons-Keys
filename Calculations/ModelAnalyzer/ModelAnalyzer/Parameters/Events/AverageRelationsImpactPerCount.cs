using ModelAnalyzer.Services;
using ModelAnalyzer.Services.FieldAnalyzer;
using System.Collections.Generic;

namespace ModelAnalyzer.Parameters.Events
{
    class AverageRelationsImpactPerCount : FloatArrayParameter
    {
        public AverageRelationsImpactPerCount()
        {
            type = ParameterType.Inner;
            title = "Среднее воздействие связей по кол-ву";
            details = "Массив, содержащий среднее арифметическое от влияния всех возможных комбинаций соседей для карт с n обратными связями.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.eventsWeight);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float rip = RequestParameter<RelationImpactPower>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float squareSum(int n) => MathAdditional.Sum(0, n, i => i*i);
            float sum(int n) => MathAdditional.Sum(0, n, i => i);

            unroundValues = new List<float>() { 0 };
            for (int n = 1; n < Field.nearesNodesAmount; n++)
                unroundValues.Add(rip * squareSum(n) / sum(n));

            values = unroundValues;

            return calculationReport;
        }
    }
}
