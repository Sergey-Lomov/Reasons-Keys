using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Moving
{
    class OneRoundSpeedDoublingProfit : FloatSingleParameter
    {
        public OneRoundSpeedDoublingProfit()
        {
            type = ParameterType.Inner;
            title = "Выгодность разового удвоения скорости";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.moving);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float sdr = calculator.UpdatedParameter<SpeedDoublingRate>().GetValue();
            float am = calculator.UpdatedParameter<AverageMining>().GetValue();
            float ra = calculator.UpdatedParameter<RoundAmount>().GetValue();

            value = unroundValue = sdr * am / (ra / 2);

            return calculationReport;
        }
    }
}
