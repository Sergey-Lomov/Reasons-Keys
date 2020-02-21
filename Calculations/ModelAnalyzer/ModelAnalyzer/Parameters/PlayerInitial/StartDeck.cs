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

            var miningEvent = MiningEvent(calculator);
            var attackEvent = AtackEvent(calculator);
            var supportEvent = SupportEvent(calculator);

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
            int fr = (int)RequestParmeter<FieldRadius>(calculator).GetValue();
            int immr = (int)RequestParmeter<InitialMiningEventMaxRadius>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return null;

            var frontBlocker = new EventRelation(RelationType.blocker, RelationDirection.front, 4);
            var relations = new List<EventRelation> { frontBlocker };

            card.relations = relations;
            card.stabilityBonus = 0;
            card.miningBonus = (int)Math.Round(micc * am, MidpointRounding.AwayFromZero);
            card.constraints.SetMaxRadius(immr, fr);
            card.comment = "Добывающее изначальное событие";

            return card;
        }

        private EventCard AtackEvent(Calculator calculator)
        {
            int fr = (int)RequestParmeter<FieldRadius>(calculator).GetValue();
            float asb = RequestParmeter<AverageStabilityBonus>(calculator).GetValue();
            int iamr = (int)RequestParmeter<InitialAtackEventMaxRadius>(calculator).GetValue();

            var card = AverageStabilityWithUndefineBranchesEvent(asb, -1, "Атакующая изначальная карта");
            card.constraints.SetMaxRadius(iamr, fr);

            return card;
        }

        private EventCard SupportEvent(Calculator calculator)
        {
            int fr = (int)RequestParmeter<FieldRadius>(calculator).GetValue();
            float asb = RequestParmeter<AverageStabilityBonus>(calculator).GetValue();
            int ismr = (int)RequestParmeter<InitialSupportEventMaxRadius>(calculator).GetValue();

            var card = AverageStabilityWithUndefineBranchesEvent(asb, +1, "Поддерживающая изначальная карта");
            card.constraints.SetMaxRadius(ismr, fr);

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

            bool withBlocker = false;
            var blockerRelations = new List<EventRelation>();
            blockerRelations.Add(new EventRelation(RelationType.blocker, RelationDirection.back, 1));
            var reasonRelations = new List<EventRelation>();
            reasonRelations.Add(new EventRelation(RelationType.reason, RelationDirection.back, 1));

            for (int i = 0; i < amount; i++)
            {
                var card = KeyEvent(kebp, minRadius, owner);

                card.relations = withBlocker ? blockerRelations : reasonRelations;
                card.comment = "Решающее событие";
                keyEvents.Add(card);

                withBlocker = !withBlocker;
            }

            return keyEvents;
        }

        private EventCard MainKeyEvent (int mkebp, int minRadius, int owner)
        {
            var card = KeyEvent(mkebp, minRadius, owner);

            var relations = new List<EventRelation>();
            relations.Add(new EventRelation(RelationType.reason, RelationDirection.back, 0));
            relations.Add(new EventRelation(RelationType.reason, RelationDirection.back, 1));

            card.relations = relations;
            card.comment = "Основное решающее событие";

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
