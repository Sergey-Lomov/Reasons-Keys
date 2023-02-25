using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseWeapon
{
    class BW_OneUsagePureProfit : FloatSingleParameter
    {
        public BW_OneUsagePureProfit()
        {
            type = ParameterType.Inner;
            title = "БО: чистая выгодность одного использования";
            details = "Чистая (с учетом цены предмета) выгодность одного выстрела из базового оружия, со средним кол-вом улучшений";
            fractionalDigits = 2;
            tags.Add(ParameterTag.baseItems);
            tags.Add(ParameterTag.items);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float bwp = RequestParmeter<BW_Profit>(calculator).GetValue();
            float ipc = RequestParmeter<ItemPriceCoefficient>(calculator).GetValue();
            float saa = RequestParmeter<AtackAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = bwp * (1 - ipc) / saa;

            return calculationReport;
        }
    }
}
