using System;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts
{
    abstract class ArtifactProfit : FloatSingleParameter
    {
        private const string missedEstimationIssue = "Более чем на 20% отклоняется от оценочной выгондосит артефактов";

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
