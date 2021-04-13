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

            float minpa = RequestParmeter<MinPlayersAmount>(calculator).GetValue();
            float maxpa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            var aripc = RequestParmeter<AverageRelationsImpactPerCount>(calculator).GetValue(); // Constants from documentation
            var rtu = RequestParmeter<RelationTemplatesUsage>(calculator).GetNoZero();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            int brcca(int n) => rtu.Keys.Where(t => t.backAmount() == n).Select(t => rtu[t].cardsCount).Sum();
            int backAmount(List<EventRelation> relations) => relations.Where(r => r.direction == RelationDirection.back).Count();
            int brsca(int n) => StartDeck.relationsPrototypes.FindAll(rs => backAmount(rs) == n).Count * (int)maxpa;

            int maxBackRelationsCount = rtu.Keys.Select(t => t.backAmount()).Max();

            float cds = rtu.Select(kvp => kvp.Value.cardsCount).Sum();
            float sds = StartDeck.relationsPrototypes.Count * maxpa;
            float acriCore(float n) => brcca((int)n) * aripc[(int)n];
            float acri = MathAdditional.sum(0, maxBackRelationsCount, acriCore) / cds;
            float asriCore(float n) => brsca((int)n) * aripc[(int)n];
            float asri = MathAdditional.sum(0, maxBackRelationsCount, asriCore) / sds;
            float asds = sds * (minpa + maxpa) / 2f / maxpa;

            value = unroundValue = (acri * cds + asri * asds) / (cds + asds);

            return calculationReport;
        }
    }
}
