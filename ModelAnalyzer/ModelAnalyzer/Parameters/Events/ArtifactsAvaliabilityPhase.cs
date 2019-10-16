using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Events
{
    class ArtifactsAvaliabilityPhase : FloatSingleParameter
    {
        const string phasesAmountIssue = "Не может быть больше, чем знаение \"{0}\": {1}";

        public ArtifactsAvaliabilityPhase()
        {
            type = ParameterType.In;
            title = "Фаза доступности артефактов";
            details = "Все карты имеющие артефакты, получат ограничение, не дающее использовать их раньше указанной фазы.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            float pa = storage.Parameter<PhasesAmount>().GetValue();

            if (pa != float.NaN)
                if (value >= pa)
                {
                    string title = storage.Parameter<PhasesAmount>().title;
                    string issue = string.Format(phasesAmountIssue, title, pa);
                    report.AddIssue(issue);
                }

            return report;
        }
    }
}
