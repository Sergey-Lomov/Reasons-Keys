using System;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CoagulationGenerator
{
    class CG_Profit : FloatSingleParameter
    {
        private const string missedEstimationIssue = "Выгодность генератора свертывания более чем на 20% отклоняется от оценочной выгондосит артефактов";

        public CG_Profit()
        {
            type = ParameterType.Inner;
            title = "ГС: выгодность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float oupr = calculator.UpdatedParameter<CG_OneUsageProfit>().GetValue();
            float ca = calculator.UpdatedParameter<CG_ChargesAmount>().GetValue();

            value = unroundValue = oupr * ca;

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
