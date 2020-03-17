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
            var bcbp = RequestParmeter<AverageContinuumBP>(calculator).GetValue();
            float kea = RequestParmeter<KeyEventsAmount>(calculator).GetValue();
            float iea = StartDeck.InitialEventsAmount;
            float maxpa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            float minpa = RequestParmeter<MinPlayersAmount>(calculator).GetValue();
            float ketbp = RequestParmeter<KeyEventsTotalBrachPoints>(calculator).GetValue();
            var deck = RequestParmeter<MainDeck>(calculator).deck;

            if (!calculationReport.IsSuccess)
                return calculationReport;

            ClearValues();

            float akebp = keca / kea * ketbp;
            float aiebp = (1 - 2) * ieca / iea;
            float abcbp = bcbp.Average();

            float aceca(float pa) => pa * (nkeca - ieca);
            float acebp(float pa) => abcbp * aceca(pa) / cna;
            float abp(float pa) => akebp + aiebp + acebp(pa);

            for (int pa = (int)minpa; pa <= maxpa; pa++)
            {
                var value = abp(pa);
                Console.WriteLine("Acebp " + pa + " : " + acebp(pa));
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
