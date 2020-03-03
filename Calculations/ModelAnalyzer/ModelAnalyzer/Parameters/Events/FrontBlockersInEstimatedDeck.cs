using ModelAnalyzer.DataModels;
using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.PlayerInitial;

namespace ModelAnalyzer.Parameters.Events
{
    class FrontBlockersInEstimatedDeck : FloatArrayParameter
    {
        public FrontBlockersInEstimatedDeck()
        {
            type = ParameterType.Inner;
            title = "Кол-во блокираторов вперед на всех картах в оценочной колоде";
            details = "Оценочная колода - колода континуума плюс начальные карты игроков. Очевидно, что состав этой колоды зависит от кол-ва игроков. При игре вшестером оценочная колода это сумма стартовой колоды и колоды континуума.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            int minpa = (int)calculator.UpdatedParameter<MinPlayersAmount>().GetValue();
            int maxpa = (int)calculator.UpdatedParameter<MaxPlayersAmount>().GetValue();
            var mainDeck = calculator.UpdatedParameter<MainDeckCore>();
            var startDeck = calculator.UpdatedParameter<StartDeck>();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            ClearValues();

            var tfbc = mainDeck.RelationsAmount(RelationType.blocker, RelationDirection.front);
            var tfbs = startDeck.RelationsAmount(RelationType.blocker, RelationDirection.front);

            float tfba(int pa) => tfbc + tfbs * (float)pa / maxpa;

            for (int pa = minpa; pa <= maxpa; pa++)
                unroundValues.Add(tfba(pa));

            values = unroundValues;

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var minpa = (int)storage.Parameter<MinPlayersAmount>().GetValue();
            var maxpa = (int)storage.Parameter<MaxPlayersAmount>().GetValue();

            ValidateSize(maxpa - minpa + 1, report);
            return report;
        }
    }
}
