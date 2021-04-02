using ModelAnalyzer.Services;
using System.Collections.Generic;

namespace ModelAnalyzer.Parameters.Events
{
    class AverageRelationsImpactPerCount : FloatArrayParameter
    {
        public AverageRelationsImpactPerCount()
        {
            type = ParameterType.Inner;
            title = "Среднее воздействие связей по кол-ву";
            details = "Массив, содержащий среднее арифметическое от влияния всех возможных комбинаций соседей для карт с n обратными связями. Но значения в массиве измеряются не единицами воздействия игроков, а единицами воздействия связей. То-есть 1 обозначает воздействие одной связи, а не воздействие силой 1.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.eventsWeight);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float i = RequestParmeter<RelationImpactPower>(calculator).GetValue();
            float ec = RequestParmeter<EstimatedRelationsLoosing>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float c = (1 - ec) / 2;
            unroundValues = new List<float>();

            // Calculations described at mechanic document
            unroundValues.Add(0);
            unroundValues.Add(2 * i * c);
            unroundValues.Add(4 * i * (ec*c + c*c));
            unroundValues.Add(6 * i * (ec*ec*c + 2*c*c*c + 2*c*c*ec));

            values = unroundValues;

            return calculationReport;
        }
    }
}
