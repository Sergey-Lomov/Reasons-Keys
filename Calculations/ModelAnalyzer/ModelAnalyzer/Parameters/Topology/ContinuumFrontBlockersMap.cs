using System.Collections.Generic;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Topology
{
    class ContinuumFrontBlockersMap : NodesImpactMap
    {

        public ContinuumFrontBlockersMap()
        {
            type = ParameterType.Indicator;
            title = "Карта блокираторов вперед (континуум)";
            details = "На этой карте указано кол-во способов добавить блокиратор для каждого узла. Учитываются только события континуума. При этом использование одного и того же события из разных соседних узлов считается за два различных способа. Но использование одного и того же события, в одном и том же узле считается одним способом, даже если у события есть два блокиратора вперед и его можно выложить в узел 2мя способами (поворотами).";
            fractionalDigits = 2;
            tags.Add(ParameterTag.topology);

            handleDirections.Add(RelationDirection.front);
            handleTypes.Add(RelationType.blocker);
        }

        internal override List<EventCard> Deck(Calculator calculator)
        {
            return calculator.UpdatedParameter<MainDeck>().deck;
        }
    }
}
