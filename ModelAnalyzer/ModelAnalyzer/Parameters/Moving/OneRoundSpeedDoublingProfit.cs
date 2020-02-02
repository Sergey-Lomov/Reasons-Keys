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

            float sdr = RequestParmeter<SpeedDoublingRate>(calculator).GetValue();
            float am = RequestParmeter<AverageMining>(calculator).GetValue();
            float ra = RequestParmeter<RoundAmount>(calculator).GetValue();

            value = unroundValue = sdr * am / (ra / 2);

            return calculationReport;
        }
    }
}
