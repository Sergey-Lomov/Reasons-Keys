using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class RealInitialStackArtifactChance : FloatSingleParameter
    {
        private const float maxDeviation = 0.1f;
        private const string deviationIssue = "Реальный шанс получения артефакта из изнчальной раздачи ортличается от заданого входящим параметром более чем на {0}%";

        public RealInitialStackArtifactChance()
        {
            type = ParameterType.Indicator;
            title = "Реальный шанс артефакта в изначальной раздаче";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.playerInitial);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var cm = calculator.UpdatedModule<InitialStackCalculationModule>();
            value = unroundValue = cm.realInitialStackArtifactChance;

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var isac = storage.Parameter<InitialStackArtifactChance>().GetValue();
            var deviation = System.Math.Abs((isac / unroundValue) - 1);

            if (deviation > maxDeviation)
            {
                var perrcentage = (int)(maxDeviation * 100);
                var issue = string.Format(deviationIssue, perrcentage);
                report.AddIssue(issue);
            }
            
            return report;
        }
    }
}
