using ModelAnalyzer.Parameters.Moving;

namespace ModelAnalyzer.Parameters.Items.Standard.KineticAccumulator
{
    class KA_Profit : SingleParameter
    {
        public KA_Profit()
        {
            type = ParameterType.Inner;
            title = "Накопитель ТЗ: выгодность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ma = calculator.UpdatedSingleValue(typeof(MotionAmount));
            float ip = calculator.UpdatedSingleValue(typeof(KA_InversePower));

            value = unroundValue = ma / ip;

            return calculationReport;
        }
    }
}
