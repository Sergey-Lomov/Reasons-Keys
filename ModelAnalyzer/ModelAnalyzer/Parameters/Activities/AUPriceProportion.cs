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

            float eu = RequestParmeter<EUPartyAmount>(calculator).GetValue();
            float au = RequestParmeter<AUPartyAmount>(calculator).GetValue();
            float mota = RequestParmeter<MotionAmount>(calculator).GetValue();
            float mp = RequestParmeter<MotionPrice>(calculator).GetValue();
            float mina = RequestParmeter<MiningAmount>(calculator).GetValue();
            float am = RequestParmeter<AverageMining>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float euFree = eu - mota * mp;
            float auFree = au - mina;
            value = unroundValue = auFree * am / (auFree * am + euFree);

            return calculationReport;
        }
    }
}
