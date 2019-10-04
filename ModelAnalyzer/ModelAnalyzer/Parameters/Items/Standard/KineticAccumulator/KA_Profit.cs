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

            float ma = calculator.UpdatedParameter<MotionAmount>().GetValue();
            float ip = calculator.UpdatedParameter<KA_InversePower>().GetValue();

            value = unroundValue = ma / ip;

            return calculationReport;
        }
    }
}
