using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Moving;

namespace ModelAnalyzer.Parameters.Items.Standard.KineticAccumulator
{
    class KA_Profit : FloatSingleParameter
    {
        public KA_Profit()
        {
            type = ParameterType.Inner;
            title = "Накопитель ТЗ: выгодность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ma = RequestParameter<MotionAmount>(calculator).GetValue();
            float ip = RequestParameter<KA_InversePower>(calculator).GetValue();

            value = unroundValue = ma / ip;

            return calculationReport;
        }
    }
}
