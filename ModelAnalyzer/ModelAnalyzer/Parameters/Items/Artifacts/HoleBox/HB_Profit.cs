using System;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Items.Artifacts.HoleBox
{
    class HB_Profit : FloatSingleParameter
    {
        private const string missedEstimationIssue = "Выгодность дыры в коробке более чем на 20% отклоняется от оценочной выгондосит артефактов";
        private const string emptyArrayIssue = "Параметр {0} не содержит значений";

        public HB_Profit()
        {
            type = ParameterType.Inner;
            title = "ДК: выгодность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float peuprc = calculator.UpdatedSingleValue(typeof(PureEUProfitCoefficient));
            float ra = calculator.UpdatedSingleValue(typeof(RoundAmount));
            float cpd = calculator.UpdatedSingleValue(typeof(HB_CollapsePreparationDuration));
            float ocac = calculator.UpdatedSingleValue(typeof(HB_OwnerCollapseAbsorbCoefficient));
            float[] tl = calculator.UpdatedArrayValue(typeof(HB_TensionLimits));

            if (tl.Count() == 0)
            {
                var title = calculator.ParameterTitle(typeof(HB_TensionLimits));
                var mesasge = string.Format(emptyArrayIssue, title);
                calculationReport.Failed(mesasge);
                value = unroundValue = 0;
                return calculationReport;
            }

            float maeu = tl.Last();

            value = unroundValue = maeu * (peuprc + (1 - cpd / ra) * (1 - ocac) / 2);

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            float eapr = storage.Parameter<EstimatedArtifactsProfit>().GetValue();

            if (Math.Abs(1 - value / eapr) > 0.2)
                report.issues.Add(missedEstimationIssue);

            return report;
        }
    }
}
