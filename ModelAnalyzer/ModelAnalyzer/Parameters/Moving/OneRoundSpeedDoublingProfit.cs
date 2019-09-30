using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Moving
{
    class OneRoundSpeedDoublingProfit : SingleParameter
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

            float sdr = calculator.UpdatedSingleValue(typeof(SpeedDoublingRate));
            float am = calculator.UpdatedSingleValue(typeof(AverageMining));
            float ra = calculator.UpdatedSingleValue(typeof(RoundAmount));

            value = unroundValue = sdr * am / (ra / 2);

            return calculationReport;
        }
    }
}
