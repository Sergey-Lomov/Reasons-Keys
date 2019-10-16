using System;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.LachesisNeedle
{
    class LN_ConnectionsAmount : FloatSingleParameter
    {
        public LN_ConnectionsAmount()
        {
            type = ParameterType.Out;
            title = "ИЛ: кол-во связей";
            details = "Кол-во соединений которые можно установить с помощью Иглы Лахесис";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ocpr = RequestParmeter<LN_OneConnectionProfit>(calculator).GetValue();
            float eapr = RequestParmeter<EstimatedArtifactsProfit>(calculator).GetValue();

            unroundValue = eapr / ocpr;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
