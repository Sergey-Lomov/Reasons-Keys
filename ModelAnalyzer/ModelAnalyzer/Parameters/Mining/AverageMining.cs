using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Mining
{
    class AverageMining : FloatSingleParameter
    {
        public AverageMining()
        {
            type = ParameterType.Inner;
            title = "Средняя добыча ТЗ";
            details = "Средняя добыча подразумевает, что игрок хаотично перемещается по полю и добывает ТЗ в случайных узлах.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.mining);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eu = RequestParmeter<EUPartyAmount>(calculator).GetValue();
            float ma = RequestParmeter<MiningAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = eu / ma;

            return calculationReport;
        }
    }
}
