using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Timing;
using ModelAnalyzer.Parameters.Moving;

namespace ModelAnalyzer.Parameters.Activities
{
    class AUPriceProportion : SingleParameter
    {
        public AUPriceProportion()
        {
            type = ParameterType.Inner;
            title = "Пропорция стоимости ЕА";
            details = "Та часть полной стоимости какого либо предмета иил действия, которую желательно выразить в ЕА";
            fractionalDigits = 2;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eu = calculator.UpdateSingleValue(typeof(EUPartyAmount));
            float au = calculator.UpdateSingleValue(typeof(AUPartyAmount));
            float ma = calculator.UpdateSingleValue(typeof(MotionAmount));
            float mp = calculator.UpdateSingleValue(typeof(MotionPrice));
            float mac = calculator.UpdateSingleValue(typeof(MiningAUCoef));
            float am = calculator.UpdateSingleValue(typeof(AverageMining));

            float euFree = eu - ma * mp;
            float auFree = (1 - mac) * au;
            value = unroundValue = (auFree * am) / (auFree * am + euFree);

            return calculationReport;
        }
    }
}
