using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class StartDeck : DeckParameter
    {
        internal const int InitialEventsAmount = 3;
        internal static List<List<EventRelation>> relationsPrototypes = new List<List<EventRelation>>() {
                new List<EventRelation>() {EventRelation.Undefined(0), EventRelation.Undefined(1), EventRelation.Undefined(2), EventRelation.Undefined(3), EventRelation.Undefined(4), EventRelation.Undefined(5)}, // Logistic
                new List<EventRelation>() {EventRelation.BackBlocker(0)}, // Atack
                new List<EventRelation>() {EventRelation.BackReason(0)}, // Support
                new List<EventRelation>() {EventRelation.BackReason(0), EventRelation.BackBlocker(1)}, // Main key
                new List<EventRelation>() {EventRelation.BackReason(0)}, // Not main key 1
                new List<EventRelation>() {EventRelation.BackBlocker(0)}, // Not main key 2
            };

        private const int logisticIndex = 0;
        private const int attackIndex = 1;
        private const int supportIndex = 2;
        private const int mainKeyIndex = 3;
        private const int notMainKeyIndex = 4;

        private const string logisticComment = "Логистическое изначальное событие";
        private const string attackComment = "Атакующее изначальное событие";
        private const string supportComment = "Поддерживающее изначальное событие";

        private List<int> miningBonuses = new List<int>();

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

            float aes = RequestParmeter<AverageEventStability>(calculator).GetValue();
            SetupMiningBonuses(calculator);

            if (!calculationReport.IsSuccess)
                return calculationReport;

            var initialEvents = InitialEvents(calculator);
            var keyEvents = KeyEvents(calculator);

            if (!calculationReport.IsSuccess)
                return calculationReport;

            deck = new List<EventCard>(initialEvents.Count() + keyEvents.Count());
            deck.AddRange(initialEvents);
            deck.AddRange(keyEvents);

            deck.ForEach(c => SetStabilityBonus(c, aes, calculator, calculationReport));

            UpdateDeckUsability(calculator);
            UpdateDeckWeight(calculator);
            UpdateDeckConstraints(calculator);

            return calculationReport;
        }

        private void SetupMiningBonuses(Calculator calculator)
        {
            var amb = RequestParmeter<AverageMiningBonus>(calculator).GetValue();
            var mainDeck = RequestParmeter<MainDeck>(calculator).deck;

            if (!calculationReport.IsSuccess)
                return;

            var size = relationsPrototypes.Count;

            // Found bonuses combination
            var availableBonuses = mainDeck.Select(c => c.miningBonus).Distinct().ToList();
            List<List<int>> combinations = MathAdditional.combinations(availableBonuses, size);
            double combinationDeviation(List<int> c) => Math.Abs(c.Average() - amb);
            var bonuses = combinations.OrderBy(c => combinationDeviation(c)).First();
            bonuses = bonuses.OrderByDescending(b => b).ToList();

            // Separate combination to two subcombinations
            var initialBonuses = new List<int>();
            var keyBonuses = new List<int>();
            for (int i = 0; i < size; i++)
                if (i % 2 == 0)
                    keyBonuses.Add(bonuses[i]);
                else
                    initialBonuses.Add(bonuses[i]);

            // Fill initial events bonuses
            double bonusDeviation(int value, List<int> values) => Math.Abs(value - values.Average());
            initialBonuses = initialBonuses.OrderByDescending(b => bonusDeviation(b, initialBonuses)).ToList();
            for (int i = 0; i < initialBonuses.Count; i++)
                miningBonuses.Add(initialBonuses[i]);

            // Fill key events bonuses
            keyBonuses = keyBonuses.OrderByDescending(b => bonusDeviation(b, keyBonuses)).ToList();
            for (int i = 0; i < keyBonuses.Count; i++)
                miningBonuses.Add(keyBonuses[i]);
        }

        private List<EventCard> InitialEvents(Calculator calculator)
        {
            var initialEvents = new List<EventCard>();

            float mpa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();

            var logisticEvent = LogisticEvent(calculator);
            var attackEvent = AttackEvent(calculator);
            var supportEvent = SupportEvent(calculator);

            if (!calculationReport.IsSuccess)
                return initialEvents;

            for (int i = 0; i < mpa; i++)
            {
                var playerMining = new EventCard(logisticEvent);
                playerMining.name = "P" + (i + 1) + "_1";
                initialEvents.Add(playerMining);

                var playerAttack = new EventCard(attackEvent);
                playerAttack.name = "P" + (i + 1) + "_2";
                initialEvents.Add(new EventCard(playerAttack));

                var playerSupport = new EventCard(supportEvent);
                playerSupport.name = "P" + (i + 1) + "_3";
                initialEvents.Add(new EventCard(playerSupport));
            }

            return initialEvents;
        }

        private EventCard LogisticEvent(Calculator calculator)
        {
            var fr = (int)RequestParmeter<FieldRadius>(calculator).GetValue();
            var liemr = (int)RequestParmeter<LogisticInitialEventMaxRadius>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return null;

            var card = new EventCard();
            card.relations = relationsPrototypes[logisticIndex];
            card.miningBonus = miningBonuses[logisticIndex];
            card.constraints.SetMaxRadius(liemr, fr);
            card.comment = logisticComment;

            return card;
        }

        private EventCard AttackEvent(Calculator calculator)
        {
            int fr = (int)RequestParmeter<FieldRadius>(calculator).GetValue();
            int iamr = (int)RequestParmeter<AtackInitialEventMaxRadius>(calculator).GetValue();

            var bp = new BranchPoint(BranchPoint.undefineBranch, -1);
            var bpList = new List<BranchPoint> { bp };
            var branchPoints = new BranchPointsSet(bpList, bpList);

            var card = new EventCard();
            card.branchPoints = branchPoints;
            card.relations = relationsPrototypes[attackIndex];
            card.miningBonus = miningBonuses[attackIndex];
            card.constraints.SetMaxRadius(iamr, fr);
            card.comment = attackComment;

            return card;
        }

        private EventCard SupportEvent(Calculator calculator)
        {
            int fr = (int)RequestParmeter<FieldRadius>(calculator).GetValue();
            float asb = RequestParmeter<AverageStabilityBonus>(calculator).GetValue();
            int ismr = (int)RequestParmeter<SupportInitialEventMaxRadius>(calculator).GetValue();

            var bp = new BranchPoint(BranchPoint.undefineBranch, +1);
            var bpList = new List<BranchPoint> { bp };
            var branchPoints = new BranchPointsSet(bpList, bpList);

            var card = new EventCard();
            card.branchPoints = branchPoints;
            card.relations = relationsPrototypes[supportIndex];
            card.miningBonus = miningBonuses[supportIndex];
            card.constraints.SetMaxRadius(ismr, fr);
            card.comment = supportComment;

            return card;
        }

        private void SetStabilityBonus(EventCard card, float stability, Calculator calculator, ParameterCalculationReport report)
        {
            var aripc = RequestParmeter<AverageRelationsImpactPerCount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return;

            int backCount = card.comment == logisticComment ? 1 : card.backRelationsCount();

            var relationsImpact = aripc[backCount];
            var stabilityBonus = (int)Math.Round(stability - relationsImpact, MidpointRounding.AwayFromZero);
            stabilityBonus = Math.Max(stabilityBonus, 0);
            card.stabilityBonus = stabilityBonus;
        }

        private List<EventCard> KeyEvents(Calculator calculator)
        {
            var keyEvents = new List<EventCard>();

            int mpa = (int)RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            int kea = (int)RequestParmeter<KeyEventsAmount>(calculator).GetValue();
            int kebp = (int)RequestParmeter<KeyEventsBranchPoints>(calculator).GetValue();
            int mkebp = (int)RequestParmeter<MainKeyEventBranchPoints>(calculator).GetValue();
            int mkemr = (int)RequestParmeter<MainKeyEventMinRadius>(calculator).GetValue();
            int nmkemr = (int)RequestParmeter<NotMainKeyEventMinRadius>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return null;

            for (int i = 0; i < mpa; i++)
            {
                var mainEvent = MainKeyEvent(mkebp, mkemr, i);
                var notMainEvents = NotMainKeyEvents(kea - 1, kebp, nmkemr, i);
                keyEvents.Add(mainEvent);
                keyEvents.AddRange(notMainEvents);
            }

            return keyEvents;
        }

        private List<EventCard> NotMainKeyEvents(int amount, int kebp, int minRadius, int owner)
        {
            var keyEvents = new List<EventCard>(amount);


            for (int i = 0; i < amount; i++)
            {
                var card = KeyEvent(kebp, minRadius, owner);

                card.relations = relationsPrototypes[notMainKeyIndex + i];
                card.miningBonus = miningBonuses[notMainKeyIndex + i];
                card.comment = "Решающее событие";
                card.name = "P" + (owner + 1) + "_" + (5 + i);
                keyEvents.Add(card);
            }

            return keyEvents;
        }

        private EventCard MainKeyEvent (int mkebp, int minRadius, int owner)
        {
            var card = KeyEvent(mkebp, minRadius, owner);

            card.relations = relationsPrototypes[mainKeyIndex];
            card.miningBonus = miningBonuses[mainKeyIndex];
            card.comment = "Основное решающее событие";
            card.name = "P" + (owner + 1) + "_4";

            return card;
        }

        private EventCard KeyEvent (int points, int minRadius, int owner)
        {
            var card = new EventCard();

            var branchPoint = new BranchPoint(owner, points);
            var success = new List<BranchPoint>();
            success.Add(branchPoint);
            var set = new BranchPointsSet(success, null);

            card.branchPoints = set;
            card.isKey = true;
            card.constraints.SetMinRadius(minRadius);

            return card;
        }
    }
}
