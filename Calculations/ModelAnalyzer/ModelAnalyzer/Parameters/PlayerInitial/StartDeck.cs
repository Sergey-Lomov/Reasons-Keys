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
                new List<EventRelation>() {EventRelation.BackReason(0), EventRelation.FrontBlocker(4) }, // Mining
                new List<EventRelation>() {EventRelation.BackReason(0)}, // Atack
                new List<EventRelation>() {EventRelation.BackBlocker(0)}, // Support
                new List<EventRelation>() {EventRelation.BackReason(0)}, // Not main key 1
                new List<EventRelation>() {EventRelation.BackBlocker(0)}, // Not main key 2
                new List<EventRelation>() {EventRelation.BackReason(0), EventRelation.BackBlocker(1)}, // Main key
            };

        private const int miningIndex = 0;
        private const int attackIndex = 1;
        private const int supportIndex = 2;
        private const int notMainKeyIndex = 3;
        private const int mainKeyIndex = 5;

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

            float aes = RequestParmeter<AverageEventStability>(calculator).GetValue();

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

        private List<EventCard> InitialEvents(Calculator calculator)
        {
            var initialEvents = new List<EventCard>();

            float mpa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();

            var miningEvent = MiningEvent(calculator);
            var attackEvent = AttackEvent(calculator);
            var supportEvent = SupportEvent(calculator);

            if (!calculationReport.IsSuccess)
                return initialEvents;

            for (int i = 0; i < mpa; i++)
            {
                var playerMining = new EventCard(miningEvent);
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

        private EventCard MiningEvent(Calculator calculator)
        {
            var micc = RequestParmeter<MiningInitialCardCoefficient>(calculator).GetValue();
            var am = RequestParmeter<AverageMining>(calculator).GetValue();
            var fr = (int)RequestParmeter<FieldRadius>(calculator).GetValue();
            var immr = (int)RequestParmeter<InitialMiningEventMaxRadius>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return null;

            var card = new EventCard();
            card.relations = relationsPrototypes[miningIndex];
            card.miningBonus = (int)Math.Round(micc * am, MidpointRounding.AwayFromZero);
            card.constraints.SetMaxRadius(immr, fr);
            card.comment = "Добывающее изначальное событие";

            return card;
        }

        private EventCard AttackEvent(Calculator calculator)
        {
            int fr = (int)RequestParmeter<FieldRadius>(calculator).GetValue();
            int iamr = (int)RequestParmeter<InitialAtackEventMaxRadius>(calculator).GetValue();

            var bp = new BranchPoint(BranchPoint.undefineBranch, -1);
            var bpList = new List<BranchPoint> { bp };
            var branchPoints = new BranchPointsSet(bpList, bpList);

            var card = new EventCard();
            card.branchPoints = branchPoints;
            card.relations = relationsPrototypes[attackIndex];
            card.constraints.SetMaxRadius(iamr, fr);
            card.comment = "Атакующая изначальная карта";

            return card;
        }

        private EventCard SupportEvent(Calculator calculator)
        {
            int fr = (int)RequestParmeter<FieldRadius>(calculator).GetValue();
            float asb = RequestParmeter<AverageStabilityBonus>(calculator).GetValue();
            int ismr = (int)RequestParmeter<InitialSupportEventMaxRadius>(calculator).GetValue();

            var bp = new BranchPoint(BranchPoint.undefineBranch, +1);
            var bpList = new List<BranchPoint> { bp };
            var branchPoints = new BranchPointsSet(bpList, bpList);

            var card = new EventCard();
            card.branchPoints = branchPoints;
            card.relations = relationsPrototypes[supportIndex];
            card.constraints.SetMaxRadius(ismr, fr);
            card.comment = "Поддерживающая изначальная карта";

            return card;
        }

        private void SetStabilityBonus(EventCard card, float stability, Calculator calculator, ParameterCalculationReport report)
        {
            var aripc = RequestParmeter<AverageRelationsImpactPerCount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return;

            var relationsImpact = aripc[card.backRelationsCount()];
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
