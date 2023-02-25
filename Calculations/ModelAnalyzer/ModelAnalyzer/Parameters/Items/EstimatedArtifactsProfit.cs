using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Items
{
    class EstimatedArtifactsProfit : FloatSingleParameter
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

            float abip = RequestParameter<AverageBaseItemsProfit>(calculator).GetValue();
            float apc = RequestParameter<ArtifactsProfitCoefficient>(calculator).GetValue();
            float ipc = RequestParameter<ItemPriceCoefficient>(calculator).GetValue();
            float ecp = RequestParameter<EventCreationPrice>(calculator).GetValue();

            value = unroundValue = abip * (1 - ipc) * apc + ecp / 2;

            return calculationReport;
        }
    }
}
