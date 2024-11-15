﻿using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class StartDeck : DeckParameter
    {
        internal static int initialEventsAmount = 3;
        internal static List<EventRelation> backReason0 = new List<EventRelation> {EventRelation.BackReason(0)};
        internal static List<EventRelation> backBlocker0 = new List<EventRelation> {EventRelation.BackBlocker(0)};

        private const int logisticIndex = 0;
        private const int attackIndex = 1;
        private const int supportIndex = 2;
        private const int notMainKeyIndex = 3;
        private const int mainKeyIndex = 4;

        private const string logisticComment = "Логистическое изначальное событие";
        private const string attackComment = "Атакующее изначальное событие";
        private const string supportComment = "Поддерживающее изначальное событие";

        private readonly List<int> miningBonuses = new List<int>();

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

            UpdateDeckUsability(calculator);
            UpdateDeckWeight(calculator);

            return calculationReport;
        }

        private void SetupMiningBonuses(Calculator calculator)
        {
            var amb = RequestParameter<AverageMiningBonus>(calculator).GetValue();
            var anmb = RequestParameter<AverageNozeroMiningBonus>(calculator).GetValue();
            var kea = (int)RequestParameter<KeyEventsAmount>(calculator).GetValue();
            var mainDeck = RequestParameter<MainDeck>(calculator).deck;

            if (!calculationReport.IsSuccess)
                return;

            var size = initialEventsAmount + kea;

            // Found bonuses combination
            var availableBonuses = mainDeck.Select(c => c.miningBonus).Distinct().ToList();
            List<List<int>> combinations = MathAdditional.Combinations(availableBonuses, size);
            double deviation(List<int> c) => Math.Abs(c.Average() - amb);
            double noZeroAverage(List<int> c) => MathAdditional.Average(c.Where(b => b != 0));
            double noZeroDeviation(List<int> c) => Math.Abs(noZeroAverage(c) - anmb);
            double totalDeviation(List<int> c) => deviation(c) + noZeroDeviation(c);
            var bonuses = combinations.OrderBy(c => totalDeviation(c)).First();
            bonuses = bonuses.OrderByDescending(b => b).ToList();

            // Separate combination to two subcombinations
            var initialBonuses = new List<int>();
            var keyBonuses = new List<int>();
            for (int i = 0; i < size; i++)
                if ((i % 2 == 0 && keyBonuses.Count < kea) || initialBonuses.Count == initialEventsAmount)
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

            float mpa = RequestParameter<MaxPlayersAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return initialEvents;

            for (int i = 0; i < mpa; i++)
            {
                initialEvents.Add(LogisticEvent(calculator, i));
                initialEvents.Add(AttackEvent(calculator, i));
                initialEvents.Add(SupportEvent(calculator, i));
            }

            return initialEvents;
        }

        private EventCard LogisticEvent(Calculator calculator, int owner)
        {
            var fr = (int)RequestParameter<FieldRadius>(calculator).GetValue();
            var liemr = (int)RequestParameter<LogisticInitialEventMaxRadius>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return null;

            var card = new EventCard();
            card.Relations = new List<EventRelation> {EventRelation.Undefined(0), EventRelation.Undefined(1), EventRelation.Undefined(2), EventRelation.Undefined(3), EventRelation.Undefined(4), EventRelation.Undefined(5)};
            card.miningBonus = miningBonuses[logisticIndex];
            card.constraints.SetMaxRadius(liemr, fr);
            card.name = "P" + (owner + 1) + "_" + logisticIndex;
            card.comment = logisticComment;

            return card;
        }

        private EventCard AttackEvent(Calculator calculator, int owner)
        {
            int fr = (int)RequestParameter<FieldRadius>(calculator).GetValue();
            int iamr = (int)RequestParameter<AtackInitialEventMaxRadius>(calculator).GetValue();

            var bp = new BranchPoint(BranchPoint.undefineBranch, -1);
            var bpList = new List<BranchPoint> { bp };
            var branchPoints = new BranchPointsSet(bpList, bpList);

            var card = new EventCard();
            card.branchPoints = branchPoints;
            card.Relations = owner % 2 == 0 ? backBlocker0 : backReason0;
            card.miningBonus = miningBonuses[attackIndex];
            card.constraints.SetMaxRadius(iamr, fr);
            card.name = "P" + (owner + 1) + "_" + attackIndex;
            card.comment = attackComment;

            return card;
        }

        private EventCard SupportEvent(Calculator calculator, int owner)
        {
            int fr = (int)RequestParameter<FieldRadius>(calculator).GetValue();
            int ismr = (int)RequestParameter<SupportInitialEventMaxRadius>(calculator).GetValue();

            var bp = new BranchPoint(BranchPoint.undefineBranch, +1);
            var bpList = new List<BranchPoint> { bp };
            var branchPoints = new BranchPointsSet(bpList, bpList);

            var card = new EventCard();
            card.branchPoints = branchPoints;
            card.Relations = owner % 2 == 0 ? backReason0 : backBlocker0;
            card.miningBonus = miningBonuses[supportIndex];
            card.constraints.SetMaxRadius(ismr, fr);
            card.name = "P" + (owner + 1) + "_" + supportIndex;
            card.comment = supportComment;

            return card;
        }

        private List<EventCard> KeyEvents(Calculator calculator)
        {
            var keyEvents = new List<EventCard>();

            int mpa = (int)RequestParameter<MaxPlayersAmount>(calculator).GetValue();
            int kea = (int)RequestParameter<KeyEventsAmount>(calculator).GetValue();
            int kebp = (int)RequestParameter<KeyEventsBranchPoints>(calculator).GetValue();
            int mkebp = (int)RequestParameter<MainKeyEventBranchPoints>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return null;

            for (int i = 0; i < mpa; i++)
            {
                var mainEvent = MainKeyEvent(mkebp, i);
                var notMainEvents = NotMainKeyEvents(kea - 1, kebp, i);
                keyEvents.Add(mainEvent);
                keyEvents.AddRange(notMainEvents);
            }

            return keyEvents;
        }

        private List<EventCard> NotMainKeyEvents(int amount, int kebp, int owner)
        {
            var keyEvents = new List<EventCard>(amount);

            var bpsOnSuccess = owner % 2 != 0;
            for (int i = 0; i < amount; i++)
            {
                var card = KeyEvent(kebp, owner, bpsOnSuccess);

                card.Relations = owner % 2 == 0 ? backReason0 : backBlocker0;
                card.miningBonus = miningBonuses[notMainKeyIndex + i];
                card.comment = "Решающее событие";
                card.name = "P" + (owner + 1) + "_" + (notMainKeyIndex + i);
                keyEvents.Add(card);

                bpsOnSuccess = !bpsOnSuccess;
            }

            return keyEvents;
        }

        private EventCard MainKeyEvent (int mkebp, int owner)
        {
            var card = KeyEvent(mkebp, owner, owner % 2 == 0);

            card.miningBonus = miningBonuses[mainKeyIndex];
            card.Relations = owner % 2 == 0 ? backReason0 : backBlocker0;
            card.comment = "Главное решающее событие";
            card.name = "P" + (owner + 1) + "_" + mainKeyIndex;

            return card;
        }

        private EventCard KeyEvent (int points, int owner, bool bpsOnSuccess)
        {
            var card = new EventCard();

            var branchPoint = new BranchPoint(owner, points);
            var bps = new List<BranchPoint>() { branchPoint };
            var set = bpsOnSuccess ? new BranchPointsSet(bps, null) : new BranchPointsSet(null, bps);

            card.branchPoints = set;
            card.isKey = true;

            return card;
        }
    }
}
