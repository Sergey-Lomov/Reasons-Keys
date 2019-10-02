using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Topology
{
    class MinDistancesPairsAmount : FloatArrayParameter
    {
        private const string arraySizeIssue = "Кол-во фаз в массиве длительности фаз и в карте путей не совпадают";

        public MinDistancesPairsAmount()
        {
            type = ParameterType.Inner;
            title = "Кол-во пар с минимальными расстояниями";
            details = "Этот параметр отвечает за подсчет кол-ва пар узлов, группируя их по минимальным расстояниям. То-есть он является массивом, индексированным минимальным расстояние между узлами пары, уменьшенным на 1, так как пар узлов между которыми расстояние 0 не существует, а индексация начинается с 0. На нулевом месте идет кол-во пар узлов, между которыми минимальное расстояние 1, на первом месте - кол-во пар с минимальным расстоянием 2 и т.д. С течением фаз кол-во пар с определенным расстоянием будет меняться. В этом параметре учитываются все пары во всех раундах.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.topology);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float[] pd = calculator.UpdatedArrayValue(typeof(PhasesDuration));
            var phasesRoutes = calculator.UpdatedParameter<RoutesMap>().phasesRoutes;

            unroundValues.Clear();
            values.Clear();

            if (pd.Length != phasesRoutes.Keys.Count)
            {
                calculationReport.Failed(arraySizeIssue);
                return calculationReport;
            }

            var distancesAmount = new Dictionary<int, int>();

            foreach (var phase in phasesRoutes.Keys)
                foreach (var route in phasesRoutes[phase])
                {
                    var distance = route.distance;
                    if (distancesAmount.ContainsKey(distance))
                        distancesAmount[distance] += (int)pd[phase];
                    else
                        distancesAmount[distance] = (int)pd[phase];
                }

            var maxDistance = distancesAmount.Keys.Max();
            for (int i = 1; i <= maxDistance; i++)
                if (distancesAmount.ContainsKey(i))
                    // In this calculation routes p1 -> p2 and p2 -> p1 may be handle like 1 route. By this reason routes amount devided at half.
                    unroundValues.Add(distancesAmount[i] / 2);
                else
                    unroundValues.Add(0);

            values = unroundValues;

            return calculationReport;
        }
    }
}
