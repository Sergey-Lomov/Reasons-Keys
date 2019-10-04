using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Services;
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
            title = "Стартовые события";
            details = "Изначальные и решающие события, с которыми игрок начинает игру";
            tags.Add(ParameterTag.playerInitial);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);
            deck.Clear();

            var initialEvents = InitialEvents(calculator);
            var keyEvents = KeyEvents(calculator);

            deck.AddRange(initialEvents);
            deck.AddRange(keyEvents);

            if (calculationReport.issues.Count > 0)
            {
                deck.Clear();
                return calculationReport;
            }

            UpdateDeckUsability(calculator);
            UpdateDeckWeight(calculator);

            return calculationReport;
        }

        private List<EventCard> InitialEvents(Calculator calculator)
        {
            var initialEvents = new List<EventCard>();

            float acew = calculator.UpdatedParameter<AverageContinuumEventWeight>().GetValue();
            float iewc = calculator.UpdatedParameter<InitialEventsWeightCoefficient>().GetValue();

            float estimatedWeight = acew * iewc;

            initialEvents.Add(LogisticsEvent(estimatedWeight, calculator));
            initialEvents.Add(MiningEvent(estimatedWeight, calculator));
            initialEvents.Add(StabilityEvent(estimatedWeight, calculator));

            return initialEvents;
        }

        private EventCard LogisticsEvent(float estimatedWeight, Calculator calculator)
        {
            var card = new EventCard();

            var mainDeck = calculator.UpdatedParameter<MainDeck>().deck;

            float licc = calculator.UpdatedParameter<LogisticsInitialCardCoefficient>().GetValue();
            float eip = calculator.UpdatedParameter<EventImpactPrice>().GetValue();
            float mbw = calculator.UpdatedParameter<MiningBonusWeight>().GetValue();

            List<float> sia = calculator.UpdatedParameter<StabilityIncrementAllocation>().GetValue();
            List<float> mba = calculator.UpdatedParameter<EventMiningBonusAllocation>().GetValue();

            // Check values
            var invalidTitles = new List<string>();

            if (float.IsNaN(eip))
                invalidTitles.Add(calculator.UpdatedParameter<EventImpactPrice>().title);

            if (mainDeck.Count() == 0)
                invalidTitles.Add(calculator.UpdatedParameter<MainDeck>().title);

            if (invalidTitles.Count > 0)
            {
                FailCalculationByInvalidIn(invalidTitles.ToArray());
                return null;
            }

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
            card.comment = "Логистичекое изначальное";

            return card;
        }

        private EventCard MiningEvent(float estimatedWeight, Calculator calculator)
        {
            var card = new EventCard();

            var mainDeck = calculator.UpdatedParameter<MainDeck>().deck;
            float micc = calculator.UpdatedParameter<MiningInitialCardCoefficient>().GetValue();
            float micu = calculator.UpdatedParameter<MinInitialCardUsability>().GetValue();
            float eip = calculator.UpdatedParameter<EventImpactPrice>().GetValue();
            float mbw = calculator.UpdatedParameter<MiningBonusWeight>().GetValue();

            // Check values
            var invalidTitles = new List<string>();

            if (float.IsNaN(eip))
                invalidTitles.Add(calculator.UpdatedParameter<EventImpactPrice>().title);

            if (mainDeck.Count() == 0)
                invalidTitles.Add(calculator.UpdatedParameter<MainDeck>().title);

            if (invalidTitles.Count > 0)
            {
                FailCalculationByInvalidIn(invalidTitles.ToArray());
                return null;
            }

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
            card.comment = "Добывающее изначальное";

            return card;
        }

        private EventCard StabilityEvent (float estimatedWeight, Calculator calculator)
        {
            var card = new EventCard();

            var mainDeck = calculator.UpdatedParameter<MainDeck>().deck;
            float sicc = calculator.UpdatedParameter<StabilityInitialCardCoefficient>().GetValue();
            float micu = calculator.UpdatedParameter<MinInitialCardUsability>().GetValue();
            float eip = calculator.UpdatedParameter<EventImpactPrice>().GetValue();
            float mbw = calculator.UpdatedParameter<MiningBonusWeight>().GetValue();

            // Check values
            var invalidTitles = new List<string>();

            if (float.IsNaN(eip))
                invalidTitles.Add(calculator.UpdatedParameter<EventImpactPrice>().title);

            if (mainDeck.Count() == 0)
                invalidTitles.Add(calculator.UpdatedParameter<MainDeck>().title);

            if (invalidTitles.Count > 0)
            {
                FailCalculationByInvalidIn(invalidTitles.ToArray());
                return null;
            }

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
            card.comment = "Стабилизирующее изначальное";

            return card;
        }

        private List<EventCard> KeyEvents(Calculator calculator)
        {
            var keyEvents = new List<EventCard>();

            float kea = calculator.UpdatedParameter<KeyEventsAmount>().GetValue();
            float kebp = calculator.UpdatedParameter<KeyEventsBranchPoints>().GetValue();
            float mkebp = calculator.UpdatedParameter<MainKeyEventBranchPoints>().GetValue();

            // Check values
            var invalidTitles = new List<string>();

            if (float.IsNaN(kea))
                invalidTitles.Add(calculator.UpdatedParameter<KeyEventsAmount>().title);

            if (float.IsNaN(kebp))
                invalidTitles.Add(calculator.UpdatedParameter<KeyEventsBranchPoints>().title);

            if (float.IsNaN(mkebp))
                invalidTitles.Add(calculator.UpdatedParameter<MainKeyEventBranchPoints>().title);

            if (invalidTitles.Count > 0)
            {
                FailCalculationByInvalidIn(invalidTitles.ToArray());
                return keyEvents;
            }

            keyEvents.Add(MainKeyEvent((int)mkebp));
            keyEvents.AddRange(NotMainKeyEvents((int)kea - 1, (int)kebp));

            return keyEvents;
        }

        private List<EventCard> NotMainKeyEvents(int amount, int kebp)
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

                var branchPoint = new BranchPoint(0, kebp);
                var success = new List<BranchPoint>();
                success.Add(branchPoint);
                var set = new BranchPointsSet(success, null);

                card.branchPoints = set;
                card.relations = withBlocker ? blockerRelations : reasonRelations;
                card.isKey = true;
                card.comment = "Решающее событие";
                keyEvents.Add(card);

                withBlocker = !withBlocker;
            }

            return keyEvents;
        }

        private EventCard MainKeyEvent (int mkebp)
        {
            var card = new EventCard();

            var branchPoint = new BranchPoint(0, mkebp);
            var success = new List<BranchPoint>();
            success.Add(branchPoint);
            var set = new BranchPointsSet(success, null);

            var relations = new List<EventRelation>();
            relations.Add(new EventRelation(RelationType.reason, RelationDirection.back, 0));
            relations.Add(new EventRelation(RelationType.reason, RelationDirection.back, 1));

            card.branchPoints = set;
            card.relations = relations;
            card.isKey = true;
            card.comment = "Основное решающее событие";

            return card;
        }
    }
}
