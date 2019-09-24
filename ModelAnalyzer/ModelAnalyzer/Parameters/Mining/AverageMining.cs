using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Mining
{
    class AverageMining : SingleParameter
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

            float eu = calculator.UpdatedSingleValue(typeof(EUPartyAmount));
            float au = calculator.UpdatedSingleValue(typeof(AUPartyAmount));
            float mc = calculator.UpdatedSingleValue(typeof(MiningAUCoef));

            value = unroundValue = eu / (au * mc);

            return calculationReport;
        }
    }
}
