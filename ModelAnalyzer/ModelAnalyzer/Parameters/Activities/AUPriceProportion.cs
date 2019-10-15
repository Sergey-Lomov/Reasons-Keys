using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Timing;
using ModelAnalyzer.Parameters.Moving;

namespace ModelAnalyzer.Parameters.Activities
{
    class AUPriceProportion : FloatSingleParameter
    {
        public AUPriceProportion()
        {
            type = ParameterType.Inner;
            title = "Пропорция стоимости ЕА";
            details = "Та часть полной стоимости какого либо предмета иил действия, которую желательно выразить в ЕА";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eu = calculator.UpdatedParameter<EUPartyAmount>().GetValue();
            float au = calculator.UpdatedParameter<AUPartyAmount>().GetValue();
            float mota = calculator.UpdatedParameter<MotionAmount>().GetValue();
            float mp = calculator.UpdatedParameter<MotionPrice>().GetValue();
            float mina = calculator.UpdatedParameter<MiningAmount>().GetValue();
            float am = calculator.UpdatedParameter<AverageMining>().GetValue();

            float euFree = eu - mota * mp;
            float auFree = au - mina;
            value = unroundValue = auFree * am / (auFree * am + euFree);

            return calculationReport;
        }
    }
}
