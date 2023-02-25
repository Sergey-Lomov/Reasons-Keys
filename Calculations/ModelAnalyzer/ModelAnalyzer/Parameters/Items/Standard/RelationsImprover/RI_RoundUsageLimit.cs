using System;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Standard.RelationsImprover
{
    class RI_RoundUsageLimit : FloatSingleParameter
    {
        public RI_RoundUsageLimit()
        {
            type = ParameterType.Out;
            title = "УС: максимальное кол-во использований за ход";
            details = "Параметр определяющий кол-во раз, которое игрок может использовать УС в течении одного хода.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ebirp = RequestParameter<EstimatedBaseItemRoundProfit>(calculator).GetValue();
            float riup = RequestParameter<RI_OneUsagePureProfit>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = ebirp / riup;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
