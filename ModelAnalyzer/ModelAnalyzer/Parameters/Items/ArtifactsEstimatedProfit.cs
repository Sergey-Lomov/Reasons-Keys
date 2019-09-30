using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Items
{
    class EstimatedArtifactsProfit : SingleParameter
    {
        public EstimatedArtifactsProfit()
        {
            type = ParameterType.Inner;
            title = "Расчетная выгодность артефактов";
            details = "Выгодность, которую должны иметь артифакты. Каждый артифакт подстраивает свои параметры таким образом, чтобы его выгодность была ккак можно ближе к этому значению. Если реальная выгодность артифакта будет отличаться от расчетной более чем на 20% должна быть показанна проблема валидации выгодности данного артефакта.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float abip = calculator.UpdatedSingleValue(typeof(AverageBaseItemsProfit));
            float apc = calculator.UpdatedSingleValue(typeof(ArtifactsProfitCoefficient));
            float ipc = calculator.UpdatedSingleValue(typeof(ItemPriceCoefficient));
            float ecp = calculator.UpdatedSingleValue(typeof(EventCreationPrice));

            value = unroundValue = abip * (1 - ipc) * apc + ecp;

            return calculationReport;
        }
    }
}
