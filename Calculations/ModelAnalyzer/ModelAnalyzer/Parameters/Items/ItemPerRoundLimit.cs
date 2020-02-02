using System;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Items
{
    class ItemPerRoundLimit : FloatSingleParameter
    {
        public ItemPerRoundLimit()
        {
            type = ParameterType.Out;
            title = "Лимит покупок за раунд";
            details = "Указывает максимальное число предметов, которые можно укпить за один раунд";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float flc = RequestParmeter<FullLoadCoefficient>(calculator).GetValue();
            float ra = RequestParmeter<RoundAmount>(calculator).GetValue();
            float bia = RequestParmeter<BaseItemsAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = ra * flc / bia;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
