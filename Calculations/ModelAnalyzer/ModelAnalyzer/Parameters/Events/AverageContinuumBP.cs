using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Services;
using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.Parameters.Events
{
    class AverageContinuumBP : FloatArrayParameter
    {
        public AverageContinuumBP()
        {
            type = ParameterType.Inner;
            title = "Среднее кол-во очков ветви в континууме";
            details = "Сумма произведений очков ветви на шанс срабатывания ветви на каждой карте континуума";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.branchPoints);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            int maxpa = (int)RequestParameter<MaxPlayersAmount>(calculator).GetValue();
            var deck = RequestParameter<MainDeck>(calculator).deck;

            if (!calculationReport.IsSuccess)
                return calculationReport;

            List<float> branchesPoints = Enumerable.Repeat(0f, maxpa).ToList();
            foreach (var card in deck)
                branchesPoints = EventCardsAnalizer.PointsByAppend(branchesPoints, card);

            values = unroundValues = branchesPoints;

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var maxpa = (int)storage.Parameter<MaxPlayersAmount>().GetValue();

            ValidateSize(maxpa, report);
            return report;
        }
    }
}
