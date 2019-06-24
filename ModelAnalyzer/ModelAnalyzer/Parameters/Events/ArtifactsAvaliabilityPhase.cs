using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Events
{
    class ArtifactsAvaliabilityPhase : SingleParameter
    {
        const string phasesAmountIssue = "Не может быть больше, чем знаение \"{0}\": {1}";

        public ArtifactsAvaliabilityPhase()
        {
            type = ParameterType.In;
            title = "Фаза доступности артефактов";
            details = "Все карты имеющие артефакты, получат ограничение, не дающее использовать их раньше указанной фазы.";
            fractionalDigits = 0;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var pa = storage.SingleValue(typeof(PhasesAmount));

            if (pa != float.NaN)
                if (value >= pa)
                {
                    string title = storage.Parameter(typeof(PhasesAmount)).title;
                    string issue = string.Format(phasesAmountIssue, title, pa);
                    report.issues.Add(issue);
                }

            return report;
        }
    }
}
