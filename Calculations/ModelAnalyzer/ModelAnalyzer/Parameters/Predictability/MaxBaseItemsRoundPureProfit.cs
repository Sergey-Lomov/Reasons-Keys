using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Items.Standard.BaseWeapon;
using ModelAnalyzer.Parameters.Items.Standard.RelationsImprover;

namespace ModelAnalyzer.Parameters.Predictability
{
    class MaxBaseItemsRoundPureProfit : FloatSingleParameter
    {
        public MaxBaseItemsRoundPureProfit()
        {
            type = ParameterType.Inner;
            title = "Максимальная чистая выгодность предметов за ход";
            details = "Максимум выгоды, который игрок может получить от испольозвания базовых предметов в рамках одного хода";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
            tags.Add(ParameterTag.predictability);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float bwupp = RequestParameter<BW_OneUsagePureProfit>(calculator).GetValue();
            float riupp = RequestParameter<RI_OneUsagePureProfit>(calculator).GetValue();
            float rirul = RequestParameter<RI_RoundUsageLimit>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = bwupp + riupp * rirul;

            return calculationReport;
        }
    }
}
