using System;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.PlayerInitial;
using ModelAnalyzer.DataModels;

namespace ModelAnalyzer.Parameters.BranchPointsTrack
{
    class EstimatedGameBP_Combinatoric : FloatSingleParameter
    {
        public EstimatedGameBP_Combinatoric()
        {
            type = ParameterType.Inner;
            title = "Расчетное кол-во очков ветви за партию";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.branchPoints);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float cna = RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();
            float keca = RequestParmeter<KeyEventCreationAmount>(calculator).GetValue();
            float nkeca = RequestParmeter<NokeyEventCreationAmount>(calculator).GetValue();
            float ieca = RequestParmeter<InitialEventCreationAmount>(calculator).GetValue();
            float kea = RequestParmeter<KeyEventsAmount>(calculator).GetValue();
            float iea = StartDeck.InitialEventsAmount;
            float maxpa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            float minpa = RequestParmeter<MinPlayersAmount>(calculator).GetValue();
            float ketbp = RequestParmeter<KeyEventsTotalBrachPoints>(calculator).GetValue();
            var deck = RequestParmeter<MainDeck>(calculator).deck;

            if (!calculationReport.IsSuccess)
                return calculationReport;

            var negativeBP = new BranchPoint(0, -1);
            var positiveBP = new BranchPoint(0, +1);
            float cnea = deck.Where(c => c.branchPoints.ContainsBranchPoint(negativeBP)).Count();
            float cpea = deck.Where(c => c.branchPoints.ContainsBranchPoint(positiveBP)).Count();

            float akebp = keca / kea * ketbp;
            float aiebp = (1 - 2) * ieca / iea;
            float aceca = (maxpa + minpa) / 2 * (nkeca - ieca);

            double comb(float chosen, float total) => MathAdditional.combinationsAmount((int)chosen, (int)total);
            double cesa = comb(aceca, cna);
            Func<float, double> cpesa = n => comb(n, cpea) * comb(aceca - n, cna - cpea);
            Func<float, double> cnesa = n => comb(n, cnea) * comb(aceca - n, cna - cnea);
            double acpea = MathAdditional.sum(0, (int)cpea, n => cpesa(n) * n) / cesa;
            double acnea = MathAdditional.sum(0, (int)cnea, n => cnesa(n) * n) / cesa;
            float acebp = (float)(acpea - acnea);

            value = unroundValue = akebp + aiebp + acebp;

            return calculationReport;
        }
    }
}
