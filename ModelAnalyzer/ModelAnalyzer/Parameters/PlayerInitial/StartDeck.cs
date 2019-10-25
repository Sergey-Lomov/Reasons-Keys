using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.Events.Weight;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class StartDeck : DeckParameter
    {
        internal const int InitialEventsAmount = 3;

        private const string logisticsCardIssueMessage = "Невозможно сгенерировать логистическую изначальную карту, которая бы совмещала занчения параметров \"Коэф. логистической изначальной карты\" и \"Коэф. веса изначальных событий\" в рамках погрешности 10%";
        private const string miningCardIssueMessage = "Невозможно сгенерировать добывающую изначальную карту, которая бы совмещала занчения параметров \"Коэф. добывающей изначальной карты\" и \"Коэф. веса изначальных событий\" в рамках погрешности 10%";
        private const string stabilityCardIssueMessage = "Невозможно сгенерировать стабилизирующую изначальную карту, которая бы совмещала занчения параметров \"Коэф. стабилизирующей изначальной карты\" и \"Коэф. веса изначальных событий\" в рамках погрешности 10%";
        private const string emptyFilterIssueMessage = "Несуществует карт континуума с необходимой стабильностью, определяемой параметром \"Коэф. минимальной применимости изначальных событий\"";

        public StartDeck()
        {
            type = ParameterType.Out;
            title = "Колода стартовых событий";
            details = "Изначальные и решающие события, с которыми игрок начинает игру";
            tags.Add(ParameterTag.playerInitial);
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var initialEvents = InitialEvents(calculator);
            var keyEvents = KeyEvents(calculator);

            if (!calculationReport.IsSuccess)
                return calculationReport;

            deck = new List<EventCard>(initialEvents.Count() + keyEvents.Count());
            deck.AddRange(initialEvents);
            deck.AddRange(keyEvents);

            UpdateDeckUsability(calculator);
            UpdateDeckWeight(calculator);
            UpdateDeckConstraints(calculator);

            return calculationReport;
        }

        private List<EventCard> InitialEvents(Calculator calculator)
        {
            var initialEvents = new List<EventCard>();

            float mpa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            float acew = RequestParmeter<AverageContinuumEventWeight>(calculator).GetValue();
            float iewc = RequestParmeter<InitialEventsWeightCoefficient>(calculator).GetValue();

            float estimatedWeight = acew * iewc;

            var logisticEvent = LogisticsEvent(estimatedWeight, calculator);
            var miningEvent = MiningEvent(estimatedWeight, calculator);
            var stabilityEvent = StabilityEvent(estimatedWeight, calculator);

            for (int i = 0; i < mpa; i++)
            {
                initialEvents.Add(new EventCard(logisticEvent));
                initialEvents.Add(new EventCard(miningEvent));
                initialEvents.Add(new EventCard(stabilityEvent));
            }

            return initialEvents;
        }

        private EventCard LogisticsEvent(float estimatedWeight, Calculator calculator)
        {
            var card = new EventCard();

            var mainDeck = RequestParmeter<MainDeck>(calculator).deck;

            float licc = RequestParmeter<LogisticsInitialCardCoefficient>(calculator).GetValue();
            float eip = RequestParmeter<EventImpactPrice>(calculator).GetValue();
            float mbw = RequestParmeter<MiningBonusWeight>(calculator).GetValue();

            List<float> sia = RequestParmeter<StabilityIncrementAllocation>(calculator).GetValue();
            List<float> mba = RequestParmeter<EventMiningBonusAllocation>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return null;

            // Calculation
            Func<EventCard, float> select = c => EventCardsAnalizer.RelationsWeight(c.relations, calculator);
            var relationsWeights = mainDeck.Select(select);
            var average = relationsWeights.Average();
            var max = relationsWeights.Max();
            var min = relationsWeights.Min();

            float estimatedRelationsWeight;
            if (licc > 0)
                estimatedRelationsWeight = average + (max - average) * licc;
            else
                estimatedRelationsWeight = average + (min - average) * licc;

            Func<EventCard, float> order = c => Math.Abs(EventCardsAnalizer.RelationsWeight(c.relations, calculator) - estimatedRelationsWeight);
            var relations = mainDeck.OrderBy(order).First().relations;
            var realRelationsWeight = EventCardsAnalizer.RelationsWeight(relations, calculator);

            var weightLeft = estimatedWeight - realRelationsWeight;
            int stability = (int)Math.Round(weightLeft / eip, MidpointRounding.AwayFromZero);
            int maxStability = sia.Count - 1;
            stability = stability > 0 ? stability : 0;
            stability = stability < maxStability ? stability : maxStability;
            weightLeft -= stability * eip;

            int miningBonus = (int)Math.Round(weightLeft / mbw, MidpointRounding.AwayFromZero);
            int maxMiningBonus = mba.Count - 1;
            miningBonus = miningBonus > 0 ? miningBonus : 0;
            miningBonus = miningBonus < maxMiningBonus ? miningBonus : maxMiningBonus;
            weightLeft -= miningBonus * eip;

            if (Math.Abs(weightLeft) / estimatedWeight > 0.1)
                calculationReport.AddIssue(logisticsCardIssueMessage);

            card.relations = relations;
            card.stabilityIncrement = stability;
            card.miningBonus = miningBonus;
            card.comment = "Логистичекое изначальное событие";

            return card;
        }

        private EventCard MiningEvent(float estimatedWeight, Calculator calculator)
        {
            var card = new EventCard();

            var mainDeck = RequestParmeter<MainDeck>(calculator).deck;
            float micc = RequestParmeter<MiningInitialCardCoefficient>(calculator).GetValue();
            float micu = RequestParmeter<MinInitialCardUsability>(calculator).GetValue();
            float eip = RequestParmeter<EventImpactPrice>(calculator).GetValue();
            float mbw = RequestParmeter<MiningBonusWeight>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return null;

            //Calculation
            var miningBonuses = mainDeck.Select(s => s.miningBonus);
            float average = (float)miningBonuses.Average();
            float max = miningBonuses.Max();
            float min = miningBonuses.Min();

            float unroundMiningBonus;
            if (micc > 0)
                unroundMiningBonus = average + (max - average) * micc;
            else
                unroundMiningBonus = average + (min - average) * micc;
            int miningBonus = (int)Math.Round(unroundMiningBonus, MidpointRounding.AwayFromZero);

            var averageStability = mainDeck.Select(s => s.stabilityIncrement).Average();
            int stability = (int)Math.Round(averageStability, MidpointRounding.AwayFromZero);
            var weightLeft = estimatedWeight - stability * eip - miningBonus * mbw;

            var filteredDeck = mainDeck.Where(c => c.usability >= micu);
            if (filteredDeck.Count() == 0)
            {
                calculationReport.AddIssue(emptyFilterIssueMessage);
                card.comment = "Сбой";
                return card;
            }

            Func<EventCard, float> order = c => Math.Abs(EventCardsAnalizer.RelationsWeight(c.relations, calculator) - weightLeft);
            var relations = filteredDeck.OrderBy(order).First().relations;
            weightLeft -= EventCardsAnalizer.RelationsWeight(relations, calculator);

            if (Math.Abs(weightLeft) / estimatedWeight > 0.1)
                calculationReport.AddIssue(miningCardIssueMessage);

            card.relations = relations;
            card.stabilityIncrement = stability;
            card.miningBonus = miningBonus;
            card.comment = "Добывающее изначальное событие";

            return card;
        }

        private EventCard StabilityEvent (float estimatedWeight, Calculator calculator)
        {
            var card = new EventCard();

            var mainDeck = RequestParmeter<MainDeck>(calculator).deck;
            float sicc = RequestParmeter<StabilityInitialCardCoefficient>(calculator).GetValue();
            float micu = RequestParmeter<MinInitialCardUsability>(calculator).GetValue();
            float eip = RequestParmeter<EventImpactPrice>(calculator).GetValue();
            float mbw = RequestParmeter<MiningBonusWeight>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return null;

            //Calculation

            var stabilityIncrements = mainDeck.Select(s => s.stabilityIncrement);
            float average = (float)stabilityIncrements.Average();
            float max = stabilityIncrements.Max();
            float min = stabilityIncrements.Min();

            float unroundStability;
            if (sicc > 0)
                unroundStability = average + (max - average) * sicc;
            else
                unroundStability = average + (min - average) * sicc;
            int stability = (int)Math.Round(unroundStability, MidpointRounding.AwayFromZero);

            var averageMiningBonus = mainDeck.Select(s => s.miningBonus).Average();
            int miningBonus = (int)Math.Round(averageMiningBonus, MidpointRounding.AwayFromZero);
            var weightLeft = estimatedWeight - stability * eip - miningBonus * mbw;
        
            var filteredDeck = mainDeck.Where(c => c.usability >= micu);
            if (filteredDeck.Count() == 0)
            {
                calculationReport.AddIssue(emptyFilterIssueMessage);
                card.comment = "Сбой";
                return card;
            }

            Func<EventCard, float> order = c => Math.Abs(EventCardsAnalizer.RelationsWeight(c.relations, calculator) - weightLeft);
            var relations = filteredDeck.OrderBy(order).First().relations;
            weightLeft -= EventCardsAnalizer.RelationsWeight(relations, calculator);

            if (Math.Abs(weightLeft) / estimatedWeight > 0.1)
                calculationReport.AddIssue(stabilityCardIssueMessage);

            card.relations = relations;
            card.stabilityIncrement = stability;
            card.miningBonus = miningBonus;
            card.comment = "Стабилизирующее изначальное событие";

            return card;
        }

        private List<EventCard> KeyEvents(Calculator calculator)
        {
            var keyEvents = new List<EventCard>();

            float mpa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            float kea = RequestParmeter<KeyEventsAmount>(calculator).GetValue();
            float kebp = RequestParmeter<KeyEventsBranchPoints>(calculator).GetValue();
            float mkebp = RequestParmeter<MainKeyEventBranchPoints>(calculator).GetValue();
            float kclc = RequestParmeter<KeyChainLenghtCoefficient>(calculator).GetValue();
            float asi = RequestParmeter<AverageStabilityIncrement>(calculator).GetValue();
            float ueca = RequestParmeter<UnkeyEventCreationAmount>(calculator).GetValue();

            float kesc = kclc * ueca * asi;
            float mkesc = kesc * mkebp / kebp;

            if (!calculationReport.IsSuccess)
                return null;

            for (int i = 0; i < mpa; i++)
            {
                var mainEvent = MainKeyEvent((int)mkebp, (int)mkesc, i);
                var notMainEvents = NotMainKeyEvents((int)kea - 1, (int)kebp, (int)kesc, i);
                keyEvents.Add(mainEvent);
                keyEvents.AddRange(notMainEvents);
            }

            return keyEvents;
        }

        private List<EventCard> NotMainKeyEvents(int amount, int kebp, int kesc, int owner)
        {
            var keyEvents = new List<EventCard>(amount);

            bool withBlocker = false;
            var blockerRelations = new List<EventRelation>();
            blockerRelations.Add(new EventRelation(RelationType.blocker, RelationDirection.back, 1));
            var reasonRelations = new List<EventRelation>();
            reasonRelations.Add(new EventRelation(RelationType.reason, RelationDirection.back, 1));

            for (int i = 0; i < amount; i++)
            {
                var card = new EventCard();

                var branchPoint = new BranchPoint(owner, kebp);
                var success = new List<BranchPoint>();
                success.Add(branchPoint);
                var set = new BranchPointsSet(success, null);

                card.branchPoints = set;
                card.relations = withBlocker ? blockerRelations : reasonRelations;
                card.minStabilityConstraint = kesc;
                card.isKey = true;
                card.comment = "Решающее событие";
                keyEvents.Add(card);

                withBlocker = !withBlocker;
            }

            return keyEvents;
        }

        private EventCard MainKeyEvent (int mkebp, int mkesc, int owner)
        {
            var card = new EventCard();

            var branchPoint = new BranchPoint(owner, mkebp);
            var success = new List<BranchPoint>();
            success.Add(branchPoint);
            var set = new BranchPointsSet(success, null);

            var relations = new List<EventRelation>();
            relations.Add(new EventRelation(RelationType.reason, RelationDirection.back, 0));
            relations.Add(new EventRelation(RelationType.reason, RelationDirection.back, 1));

            card.branchPoints = set;
            card.relations = relations;
            card.minStabilityConstraint = mkesc;
            card.isKey = true;
            card.comment = "Основное решающее событие";

            return card;
        }
    }
}
