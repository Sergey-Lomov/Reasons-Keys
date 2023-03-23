using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.DataModels;
using System.Collections.Generic;
using System;
using System.Linq;
using ModelAnalyzer.Parameters.General;

namespace ModelAnalyzer.Parameters.Activities.EventsRestoring
{
    class EventsRestoringModule : CalculationModule
    {
        const string inconsistentSizeIssue = "При расчетах различные комбинации участвующих в игре игроков дали разное кол-во карт в раздаче";
        const string minimalLuckyIssue = "Как минимум для одной комбинации игроков не удалось подобрать кол-во карт, удовлетворяющее минимальному шансу успешной раздачи";
        const int maxStackSize = 6;

        private enum CardType { positive, neutral, negative }

        internal struct StackDescription
        {
            internal int size;
            internal float luckyChance;
            internal float unluckyChance;

            internal StackDescription(int size, float luckyChance, float unluckyChance)
            {
                this.size = size;
                this.luckyChance = luckyChance;
                this.unluckyChance = unluckyChance;
            }
        }

        internal List<StackDescription> stacks = new List<StackDescription>();

        public EventsRestoringModule()
        {
            title = "Модуль расчетов раздачи событий";
        }

        internal override ModuleCalculationReport Execute(Calculator calculator)
        {
            calculationReport = new ModuleCalculationReport(this);

            var eusc = RequestParmeter<EstimatedUnluckyStackChance>(calculator).GetValue();
            var mlsc = RequestParmeter<MinimalLuckyStackChance>(calculator).GetValue();
            var minpa = (int)RequestParmeter<MinPlayersAmount>(calculator).GetValue();
            var maxpa = (int)RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            var deck = RequestParmeter<MainDeck>(calculator);

            if (!calculationReport.IsSuccess)
                return calculationReport;

            for (int playersAmount = minpa; playersAmount <= maxpa; playersAmount++)
            {
                var stack = StackFor(playersAmount, maxpa, deck.deck, eusc, mlsc, calculationReport);
                stacks.Add(stack);
            }

            return calculationReport;
        }

        private StackDescription StackFor(int playersAmount, int maxPlayersAmount, List<EventCard> deck, float eusc, float mlsc, OperationReport report)
        {
            var result = new StackDescription();
            var allPlayers = Enumerable.Range(0, maxPlayersAmount).ToList();
            var playersCombinations = Combinations.Build(allPlayers, playersAmount);
            var stacks = new List<StackDescription>();
            foreach (IEnumerable<int> combination in playersCombinations)
            {
                var stack = StackFor(combination.ToArray(), deck, eusc, mlsc, report);
                stacks.Add(stack);
            }

            /*result.size = stacks.First().size;
            if (stacks.Where(s => s.size != result.size).Count() > 0)
            {
                report.AddIssue(inconsistentSizeIssue);
            }*/
            var size = (int)Math.Round(stacks.Select(s => s.size).Average(), MidpointRounding.AwayFromZero);
            if (size > 0)
            {
                return StackFor(playersCombinations, deck, size);
            } else
            {
                return stacks.First();
            }
        }

        private List<StackDescription> StacksFor(int[] availablePlayers, List<EventCard> deck)
        {
            var player = availablePlayers.First(); //Calculates only for first player, because all player have simmetrically cards concepts
            var negativeCards = deck.Where(c => TypeOf(c, player, availablePlayers) == CardType.negative).Count();
            var positiveCards = deck.Where(c => TypeOf(c, player, availablePlayers) == CardType.positive).Count();
            var totalCards = (float)deck.Count();
            var nonPositiveCards = totalCards - positiveCards;

            var stacks = new List<StackDescription>(maxStackSize);
            for (int size = 1; size <= maxStackSize; size++)
            {
                float negativeChance = 1;
                float nonPositiveChance = 1;
                for (int i = 0; i < size; i++)
                {
                    negativeChance *= (negativeCards - i) / (totalCards - i);
                    nonPositiveChance *= (nonPositiveCards - i) / (totalCards - i);
                }
                var stack = new StackDescription(size, 1 - nonPositiveChance, negativeChance);
                stacks.Add(stack);
            }

            return stacks;
        }

        private StackDescription StackFor(int[] availablePlayers, List<EventCard> deck, float eusc, float mlsc, OperationReport report)
        {
            var stacks = StacksFor(availablePlayers, deck);
            var validStacks = stacks.Where(s => s.luckyChance >= mlsc).ToList();
            if (validStacks.Count() == 0)
            {
                report.AddIssue(minimalLuckyIssue);
                return new StackDescription();
            }

            var bestStack = validStacks.OrderBy(s => Math.Abs(s.unluckyChance - eusc)).First();

            return bestStack;
        }

        private StackDescription StackFor(System.Collections.IEnumerable playersCombinations, List<EventCard> deck, int size)
        {
            var stacks = new List<StackDescription>();
            foreach (IEnumerable<int> combination in playersCombinations)
            {
                var combinationStacks = StacksFor(combination.ToArray(), deck);
                var filtered = combinationStacks.Where(s => s.size == size);
                stacks.Add(filtered.First());
            }

            var result = new StackDescription();
            result.size = size;
            result.unluckyChance = stacks.Select(s => s.unluckyChance).Average();
            result.luckyChance = stacks.Select(s => s.luckyChance).Average();
            return result;
        }

        private CardType TypeOf(EventCard card, int player, int[] availablePlayers)
        {
            var positivePlayers = card.branchPoints.All().Where(bp => bp.point > 0).Select(bp => bp.branch);
            var negativePlayers = card.branchPoints.All().Where(bp => bp.point < 0).Select(bp => bp.branch);
            var availablePositiveCount = positivePlayers.Where(p => availablePlayers.Contains(p)).Count();
            var availableNegativeCount = negativePlayers.Where(p => availablePlayers.Contains(p)).Count();
            var playerPositiveCount = positivePlayers.Where(b => b == player).Count();
            var playerNegativeCount = negativePlayers.Where(b => b == player).Count();
            // TODO: Remove test code and test enum value
            if ((availableNegativeCount >= 2 && playerNegativeCount == 0) // Guaranted -1 to enemy                                                             
                || playerPositiveCount > 0 // Chance to get +1 to player
                || (card.provideArtifact && (availablePositiveCount <= 2 || playerPositiveCount >= 0))) // Provide artifact and not get guaranted +1 to enemy   
                return CardType.positive;
            else if (((availablePositiveCount >= 2 && playerPositiveCount == 0) // Guaranted +1 to enemy
                || playerNegativeCount > 0) // Chance to get -1 to player
                && !card.provideArtifact) // Card with artifact is not negative anyway
                return CardType.negative;
            else
                return CardType.neutral;
        }
    }
}
