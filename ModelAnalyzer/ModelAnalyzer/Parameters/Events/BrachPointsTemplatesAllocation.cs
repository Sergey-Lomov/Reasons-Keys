using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.DataModels;

namespace ModelAnalyzer.Parameters.Events
{
    class BrachPointsTemplatesAllocation : FloatArrayParameter
    {
        const int BrachPointsTeplatesAmount = 7;
        public List<BranchPointsTemplate> templates;

        public BrachPointsTemplatesAllocation()
        {
            type = ParameterType.In;
            title = "Распределение шаблонов очков ветвей";
            details = "Шаблоном очков ветвей считается комбинация, такая как -1/0 или +1/+1. Распределение задается в порядке: -1/-1, +1/+1, -1/0, 0/-1, +1/0, 0/+1, 0/0";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);

            int[] p1 = { +1 };
            int[] m1 = { -1 };
            int[] zero = { };

            var templatesArr = new BranchPointsTemplate[] {
                new BranchPointsTemplate(m1, m1),
                new BranchPointsTemplate(p1, p1),
                new BranchPointsTemplate(zero, m1),
                new BranchPointsTemplate(m1, zero),
                new BranchPointsTemplate(zero, p1),
                new BranchPointsTemplate(p1, zero),
                new BranchPointsTemplate(zero, zero)
            };
            templates = new List<BranchPointsTemplate>(templatesArr);
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            ValidateSize(BrachPointsTeplatesAmount, report);
            return report;
        }
    }
}
