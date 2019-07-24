using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Parameters.PlayerInitial;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.Parameters.Activities
{
    class AverageUnkeyEventsConcreteBranchPoints : ArrayParameter
    {
        readonly int[] BranchPointsAmounts = {2, 2, 1, 1, 1, 1, 0};

        public AverageUnkeyEventsConcreteBranchPoints()
        {
            type = ParameterType.Inner;
            title = "Среднее кол-во очков конкретной ветви на разыгранных не решающих событиях";
            details = "Имеется ввиду кол-во очков одной конкретной ветви на не решающих событиях, разыгранных за партию. Зависит от кол-ва игроков.";
            fractionalDigits = 2;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            int minpa = (int)calculator.UpdateSingleValue(typeof(MinPlayersAmount));
            int maxpa = (int)calculator.UpdateSingleValue(typeof(MaxPlayersAmount));

            float kebpc = calculator.UpdateSingleValue(typeof(KeyEventsBrachPointsCoefficient));
            float iea = calculator.UpdateSingleValue(typeof(InitialEventsAmount));
            float iewc = calculator.UpdateSingleValue(typeof(InitialEventsWeightCoefficient));
            float nkeca = calculator.UpdateSingleValue(typeof(UnkeyEventCreationAmount));
            float cna = calculator.UpdateSingleValue(typeof(ContinuumNodesAmount));
            float aiebp = calculator.UpdateSingleValue(typeof(AverageInitialEventsBranchPoints));

            float[] bpal = calculator.UpdateArrayValue(typeof(BrachPointsTemplatesAllocation));
            int[] bpam = BranchPointsAmounts;

            // For calculation details see mechanic document
            float acebp = 0; //Average brach points in continuum event
            for (int i = 0; i < bpal.Length; i++)
                acebp += bpal[i] * bpam[i];
            acebp = acebp / bpal.Sum();

            float eaiea = nkeca * 1 / (1 + iewc) * iewc;

            List<float> euebp = new List<float>(maxpa - minpa + 1);
            for (int pa = minpa; pa <= maxpa; pa++)
            {
                float acea = (nkeca - Math.Min(eaiea, iea)) * pa;
                float aiea = Math.Min(eaiea, iea) * pa;
                float abpp = (acea * acebp + aiea * aiebp) / maxpa;
                euebp.Add(abpp);
            }

            values = unroundValues = euebp;

            return calculationReport;
        }
    }
}
