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
    class EstimatedGameBP : FloatArrayParameter
    {
        public EstimatedGameBP()
        {
            type = ParameterType.Inner;
            title = "Расчетное кол-во очков ветви за партию";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.branchPointsTrack);
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

            ClearValues();

            var negativeBP = new BranchPoint(0, -1);
            var positiveBP = new BranchPoint(0, +1);
            float cnea = deck.Where(c => c.branchPoints.ContainsBranchPoint(negativeBP)).Count();
            float cpea = deck.Where(c => c.branchPoints.ContainsBranchPoint(positiveBP)).Count();

            float akebp = keca / kea * ketbp;
            float aiebp = (1 - 2) * ieca / iea;

            float aceca(float pa) => pa * (nkeca - ieca);
            float acpea(float pa) => aceca(pa) * cpea / cna;
            float acnea(float pa) => aceca(pa) * cnea / cna;
            float acebp(float pa) => acpea(pa) - acnea(pa);
            float abp(float pa) => akebp + aiebp + acebp(pa);

            for (int pa = (int)minpa; pa <= maxpa; pa++)
            {
                var value = abp(pa);
                unroundValues.Add(value);
                values.Add(value);
            }

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var minpa = storage.Parameter<MinPlayersAmount>().GetValue();
            var maxpa = storage.Parameter<MaxPlayersAmount>().GetValue();
            ValidateSize(maxpa - minpa + 1, report);
            return report;
        }
    }
}
