using System.Collections.Generic;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.PlayerInitial;
using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Topology
{
    class FrontBlockersRelativeMap : NodesImpactMap
    {
        public FrontBlockersRelativeMap()
        {
            type = ParameterType.Indicator;
            title = "Карта блокираторов вперед (относительная)";
            details = "На этой карте указано отношение кол-во способов добавить блокиратор для каждого узла к кол-ву карт, которые имеют блокираторы вперед. Учитываются все события (континуума + изначальная колода). При этом использование одного и того же события из разных соседних узлов считается за два различных способа. Но использование одного и того же события, в одном и том же узле считается одним способом, даже если у события есть два блокиратора вперед и его можно выложить в узел 2мя способами (поворотами).";
            fractionalDigits = 2;
            tags.Add(ParameterTag.topology);

            handleDirections.Add(RelationDirection.front);
            handleTypes.Add(RelationType.blocker);
            useRelative = true;
        }

        internal override List<EventCard> Deck(Calculator calculator)
        {
            var continuumDeck = calculator.UpdatedParameter<MainDeck>().deck;
            var initialDeck = calculator.UpdatedParameter<StartDeck>().deck;
            var result = new List<EventCard>(continuumDeck);
            result.AddRange(initialDeck);
            return result;
        }
    }
}
