using System.Linq;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Services;
using ModelAnalyzer.Services.FieldAnalyzer;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.PlayerInitial;

namespace ModelAnalyzer.Parameters.Events
{
    class BackRelationIgnoringChance : FloatArrayParameter
    {
        public BackRelationIgnoringChance()
        {
            type = ParameterType.Inner;
            title = "Шанс игнорирования связи назад";
            details = "Вероятность того, что связь назад будет проигнорирована по какому-либо правилу";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            int minpa = (int)calculator.UpdatedParameter<MinPlayersAmount>().GetValue();
            int maxpa = (int)calculator.UpdatedParameter<MaxPlayersAmount>().GetValue();
            float cna = calculator.UpdatedParameter<ContinuumNodesAmount>().GetValue();
            float eca = calculator.UpdatedParameter<EventCreationAmount>().GetValue();
            float keca = calculator.UpdatedParameter<KeyEventCreationAmount>().GetValue();
            var abr = calculator.UpdatedParameter<NodesAvailableBackRelations>().GetValue();
            var tfra = calculator.UpdatedParameter<FrontReasonsInEstimatedDeck>().GetValue();
            var tfba = calculator.UpdatedParameter<FrontBlockersInEstimatedDeck>().GetValue();
            var mainDeck = calculator.UpdatedParameter<MainDeckCore>();
            var startDeck = calculator.UpdatedParameter<StartDeck>();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            ClearValues();

            bool isBack(EventRelation r) => r.direction == RelationDirection.back;
            int backCount(EventCard c) => c.relations.Where(r => isBack(r)).Count();
            var brc = mainDeck.deck.Select(c => backCount(c)).Sum();
            var brs = startDeck.deck.Select(c => backCount(c)).Sum();
            var nfbrc = mainDeck.deck.Select(c => backCount(c) - 1).Where(i => i >= 0).Sum();
            var nfbrs = startDeck.deck.Select(c => backCount(c) - 1).Where(i => i >= 0).Sum();

            float totalRelationsSlots = Field.nearesNodesAmount * cna;
            double sumAlpha(float i) => i * abr[(int)i];
            float backRelationsSlots = (float)MathAdditional.sum(0, Field.nearesNodesAmount, sumAlpha);

            float bra(int pa) => brc + brs * (float)pa / maxpa;
            float nfbra(int pa) => nfbrc + nfbrs * (float)pa / maxpa;
            float tfr(int pa) => tfra[pa - minpa] + tfba[pa - minpa];
            float frc(int pa) => tfr(pa) / (totalRelationsSlots - backRelationsSlots);
            float enc(int pa) => (1 - pa * eca / cna) * (nfbra(pa) / bra(pa));
            float kec(int pa) => pa * keca / cna;

            for (int pa = minpa; pa <= maxpa; pa++)
            {
                float bric = frc(pa) + enc(pa) + kec(pa);
                unroundValues.Add(bric);
            }

            values = unroundValues;

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var minpa = (int)storage.Parameter<MinPlayersAmount>().GetValue();
            var maxpa = (int)storage.Parameter<MaxPlayersAmount>().GetValue();

            ValidateSize(maxpa - minpa + 1, report);
            return report;
        }
    }
}
