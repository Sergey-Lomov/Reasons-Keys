using ModelAnalyzer.Services;

using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.PlayerInitial;
using System.Collections.Generic;
using System.Linq;
using ModelAnalyzer.DataModels;

namespace ModelAnalyzer.Parameters.Events
{
    class AverageRelationsImpactPower : FloatSingleParameter
    {
        public AverageRelationsImpactPower()
        {
            type = ParameterType.Inner;
            title = "Среднее воздействие связей событий";
            details = "Отображает силу воздействия, которую в среднем оказывают связи события";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var minpa = RequestParameter<MinPlayersAmount>(calculator).GetValue();
            var maxpa = RequestParameter<MaxPlayersAmount>(calculator).GetValue();
            var aripc = RequestParameter<AverageRelationsImpactPerCount>(calculator).GetValue(); // Constants from documentation
            var rtu = RequestParameter<RelationTemplatesUsage>(calculator).GetNoZero();
            var kea = (int)RequestParameter<KeyEventsAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float sds = StartDeck.initialEventsAmount + kea;
            int brsca1 = (int)(kea % 2 == 0 ? sds : sds - 1) * (int)maxpa;
            int brsca2 = (kea % 2 == 0 ? 0 : 1) * (int)maxpa;
            // 5 is max amount of back relations (based on field topology), but array shouls start from 0 back relations.
            int[] brsca = new int[6] { 0, brsca1, brsca2, 0, 0, 0 };

            int brcca(int n) => rtu.Keys.Where(t => t.BackAmount() == n).Select(t => rtu[t].cardsCount).Sum();
            int maxBackRelationsCount = rtu.Keys.Select(t => t.BackAmount()).Max();
            float cds = rtu.Select(kvp => kvp.Value.cardsCount).Sum();
            float acriCore(float n) => brcca((int)n) * aripc[(int)n];
            float acri = MathAdditional.Sum(0, maxBackRelationsCount, acriCore) / cds;
            float asriCore(float n) => brsca[(int)n] * aripc[(int)n];
            float asri = MathAdditional.Sum(0, maxBackRelationsCount, asriCore) / sds;
            float asds = sds * (minpa + maxpa) / 2f / maxpa;

            value = unroundValue = (acri * cds + asri * asds) / (cds + asds);

            return calculationReport;
        }
    }
}
