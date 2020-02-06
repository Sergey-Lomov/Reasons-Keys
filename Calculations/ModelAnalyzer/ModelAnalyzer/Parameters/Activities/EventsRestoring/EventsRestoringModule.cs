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

        internal struct StackDescrition
        {
            internal int size;
            internal float luckyChance;
            internal float unluckyChance;

            internal StackDescrition(int size, float luckyChance, float unluckyChance)
            {
                this.size = size;
                this.luckyChance = luckyChance;
                this.unluckyChance = unluckyChance;
            }
        }

        internal List<StackDescrition> stacks = new List<StackDescrition>();

        public EventsRestoringModule()
        {
            title = "Модуль расчектов раздачи событий";
        }

        internal override ModuleCalculationReport Execute(Calculator calculator)
        {
            var report = new ModuleCalculationReport(this);

            var eusc = RequestParmeter<EstimatedUnluckyStackChance>(calculator, report).GetValue();
            var mlsc = RequestParmeter<MinimalLuckyStackChance>(calculator, report).GetValue();
            var minpa = (int)RequestParmeter<MinPlayersAmount>(calculator, report).GetValue();
            var maxpa = (int)RequestParmeter<MaxPlayersAmount>(calculator, report).GetValue();
            var deck = RequestParmeter<MainDeck>(calculator, report);

            var sizes = new List<int>();
            var unluckyChances = new List<float>();
            var luckyChances = new List<float>();

            for (int playersAmount = minpa; playersAmount <= maxpa; playersAmount++)
            {
                var stack = StackFor(playersAmount, maxpa, deck.deck, eusc, mlsc, report);
                stacks.Add(stack);
            }

            if (!report.IsSuccess)
                return report;

            return report;
        }

        private StackDescrition StackFor(int playersAmount, int maxPlayersAmount, List<EventCard> deck, float eusc, float mlsc, OperationReport report)
        {
            var result = new StackDescrition();
            var allPlayers = Enumerable.Range(0, maxPlayersAmount).ToList();
            var playersCombinations = Combinations.Build(allPlayers, playersAmount);
            var stacks = new List<StackDescrition>();
            foreach (IEnumerable<int> combination in playersCombinations)
            {
                var stack = StackFor(combination.ToArray(), deck, eusc, mlsc, report);
                stacks.Add(stack);
            }

            result.size = stacks.First().size;
            if (stacks.Where(s => s.size != result.size).Count() > 0)
            {
                report.AddIssue(inconsistentSizeIssue);
            }
            result.unluckyChance = stacks.Select(s => s.unluckyChance).Average();
            result.luckyChance = stacks.Select(s => s.luckyChance).Average();

            return result;
        }

        private StackDescrition StackFor(int[] availablePlayers, List<EventCard> deck, float eusc, float mlsc, OperationReport report)
        {
            var player = availablePlayers.First(); //Calculates only for first player, because all player have simmetrically cards concepts
            var negativeCards = deck.Where(c => TypeOf(c, player, availablePlayers) == CardType.negative).Count();
            var positiveCards = deck.Where(c => TypeOf(c, player, availablePlayers) == CardType.positive).Count();
            var totalCards = (float)deck.Count();
            var nonPositiveCards = totalCards - positiveCards;

            var stacks = new List<StackDescrition>(maxStackSize);
            for (int size = 1; size <= maxStackSize; size++)
            {
                float negativeChance = 1;
                float nonPositiveChance = 1;
                for (int i = 0; i < size; i++)
                {
                    negativeChance *= (negativeCards - i) / (totalCards - i);
                    nonPositiveChance *= (nonPositiveCards - i) / (totalCards - i);
                }
                var stack = new StackDescrition(size, 1 - nonPositiveChance, negativeChance);
                stacks.Add(stack);
            }
            var validStacks = stacks.Where(s => s.luckyChance >= mlsc);
            if (validStacks.Count() == 0)
            {
                report.AddIssue(minimalLuckyIssue);
                return new StackDescrition();
            }

            var bestStack = validStacks.OrderBy(s => Math.Abs(s.unluckyChance - eusc)).First();

            return bestStack;
        }

        private CardType TypeOf(EventCard card, int player, int[] availablePlayers)
        {
            var positivePlayers = card.branchPoints.all().Where(bp => bp.point > 0).Select(bp => bp.branch);
            var negativePlayers = card.branchPoints.all().Where(bp => bp.point < 0).Select(bp => bp.branch);
            var availablePositiveCount = positivePlayers.Where(p => availablePlayers.Contains(p)).Count();
            var availableNegativeCount = negativePlayers.Where(p => availablePlayers.Contains(p)).Count();
            var playerPositiveCount = positivePlayers.Where(b => b == player).Count();
            var playerNegativeCount = negativePlayers.Where(b => b == player).Count();
            if ((availableNegativeCount >= 2 && playerNegativeCount == 0) || playerPositiveCount > 0)
                return CardType.positive;
            else if ((availablePositiveCount >= 2 && playerPositiveCount == 0) || playerNegativeCount > 0)
                return CardType.negative;
            else
                return CardType.neutral;
        }
    }
}
