using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.PlayerInitial;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.Parameters.Activities
{
    class AverageUnkeyEventsConcreteBranchPoints : FloatArrayParameter
    {
        readonly int[] BranchPointsAmounts = {2, 2, 1, 1, 1, 1, 0};

        public AverageUnkeyEventsConcreteBranchPoints()
        {
            type = ParameterType.Inner;
            title = "Среднее кол-во очков конкретной ветви на разыгранных не решающих событиях";
            details = "Имеется ввиду кол-во очков одной конкретной ветви на не решающих событиях, разыгранных за партию. Зависит от кол-ва игроков.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            int minpa = (int)RequestParmeter<MinPlayersAmount>(calculator).GetValue();
            int maxpa = (int)RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            float iewc = RequestParmeter<InitialEventsWeightCoefficient>(calculator).GetValue();
            float nkeca = RequestParmeter<UnkeyEventCreationAmount>(calculator).GetValue();
            float aiebp = RequestParmeter<AverageInitialEventsBranchPoints>(calculator).GetValue();
            List<float> bpal = RequestParmeter<BrachPointsTemplatesAllocation>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float iea = StartDeck.InitialEventsAmount;
            int[] bpam = BranchPointsAmounts;

            // For calculation details see mechanic document
            float acebp = 0; //Average brach points in continuum event
            for (int i = 0; i < bpal.Count; i++)
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
