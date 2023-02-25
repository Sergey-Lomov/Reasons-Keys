using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Standard.RelationsImprover
{
    class RI_OneUsagePureProfit : FloatSingleParameter
    {
        public RI_OneUsagePureProfit()
        {
            type = ParameterType.Inner;
            title = "УС: чистая выгодность одного использования";
            details = "Параметр обозначающий выгодность, которую может принести УС за одно применение. Рассчитывается на основе общей выгодности этого предмета, но с вычетом из нее цены.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float rip = RequestParmeter<RI_Profit>(calculator).GetValue();
            float rica = RequestParmeter<RI_ChargesAmount>(calculator).GetValue();
            float ipc = RequestParmeter<ItemPriceCoefficient>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = rip * (1 - ipc) / rica;

            return calculationReport;
        }
    }
}
