using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Activities;
using System;

namespace ModelAnalyzer.Parameters.Events
{
    class ChainStabilityLimit : FloatSingleParameter
    {
        public ChainStabilityLimit()
        {
            type = ParameterType.Out;
            title = "Предел цепной стабильности";
            details = "Это параметр задает предел, превысив который цепная стабильность опускается до 1.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float mfbas = RequestParmeter<MinigForBalanceAverageStability>(calculator).GetValue();
            float eip = RequestParmeter<EventImpactPrice>(calculator).GetValue();
            float am = RequestParmeter<AverageMining>(calculator).GetValue();
            float abs = RequestParmeter<AverageStabilityIncrement>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = 2 * (mfbas * am / eip - abs) - 1;
            value = (float)Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
