using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Timing;

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

            float eu = calculator.UpdatedParameter<EUPartyAmount>().GetValue();
            float au = calculator.UpdatedParameter<AUPartyAmount>().GetValue();
            float mc = calculator.UpdatedParameter<MiningAUCoef>().GetValue();

            value = unroundValue = eu / (au * mc);

            return calculationReport;
        }
    }
}
