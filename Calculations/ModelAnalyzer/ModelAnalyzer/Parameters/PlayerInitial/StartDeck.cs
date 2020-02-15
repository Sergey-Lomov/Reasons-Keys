using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class StartDeck : DeckParameter
    {
        internal const int InitialEventsAmount = 3;

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
            float asb = RequestParmeter<AverageStabilityBonus>(calculator).GetValue();

            var miningEvent = MiningEvent(calculator);
            var attackEvent = AverageStabilityWithUndefineBranchesEvent(asb, -1, "Атакующая изначальная карта");
            var supportEvent = AverageStabilityWithUndefineBranchesEvent(asb, +1, "Поддерживающая изначальная карта");

            if (!calculationReport.IsSuccess)
                return initialEvents;

            for (int i = 0; i < mpa; i++)
            {
                initialEvents.Add(new EventCard(miningEvent));
                initialEvents.Add(new EventCard(attackEvent));
                initialEvents.Add(new EventCard(supportEvent));
            }

            return initialEvents;
        }

        private EventCard MiningEvent(Calculator calculator)
        {
            var card = new EventCard();

            float micc = RequestParmeter<MiningInitialCardCoefficient>(calculator).GetValue();
            float am = RequestParmeter<AverageMining>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return null;

            var frontBlocker = new EventRelation(RelationType.blocker, RelationDirection.front, 4);
            var relations = new List<EventRelation> { frontBlocker };

            card.relations = relations;
            card.stabilityBonus = 0;
            card.miningBonus = (int)Math.Round(micc * am, MidpointRounding.AwayFromZero);
            card.comment = "Добывающее изначальное событие";

            return card;
        }

        private EventCard AverageStabilityWithUndefineBranchesEvent(float asb, int points, string comment)
        {
            var card = new EventCard();
            var backReason = new EventRelation(RelationType.reason, RelationDirection.back, 0);
            var relations = new List<EventRelation> { backReason };

            var undefine = new BranchPoint(BranchPoint.undefineBranch, points);
            var undefineList = new List<BranchPoint> { undefine };
            var branchPoints = new BranchPointsSet(undefineList, undefineList);

            card.relations = relations;
            card.branchPoints = branchPoints;
            card.stabilityBonus = (int)Math.Round(asb, MidpointRounding.AwayFromZero);
            card.miningBonus = 0;
            card.comment = comment;

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
            float asi = RequestParmeter<AverageStabilityBonus>(calculator).GetValue();
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
