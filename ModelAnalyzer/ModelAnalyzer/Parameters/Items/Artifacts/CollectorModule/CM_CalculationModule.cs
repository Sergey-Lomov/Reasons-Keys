using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Services.FieldAnalyzer;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Timing;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.PlayerInitial;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule
{
    class CM_CalculationModule : CalculationModule
    {
        internal int power;
        internal float profit;

        private readonly float artifactGainCoefficient = 0.0f;
        private readonly int maxPower = 6;

        public CM_CalculationModule()
        {
            title = "МК: модуль расчетов";
        }

        internal override ModuleCalculationReport Execute(Calculator calculator)
        {
            var report = new ModuleCalculationReport(this);

            var phasesAmount = (int)calculator.UpdatedParameter<PhasesAmount>().GetValue();
            var phasesDuration = calculator.UpdatedParameter<PhasesDuration>().GetValue();
            var minPlayersAmount = (int)calculator.UpdatedParameter<MinPlayersAmount>().GetValue();
            var maxPlayersAmount = (int)calculator.UpdatedParameter<MaxPlayersAmount>().GetValue();
            var roundAmount = calculator.UpdatedParameter<RoundAmount>().GetValue();
            var continuumNodesAmount = calculator.UpdatedParameter<ContinuumNodesAmount>().GetValue();
            var miningAUCoeff = calculator.UpdatedParameter<MiningAUCoef>().GetValue();
            var auPartyAmount = calculator.UpdatedParameter<AUPartyAmount>().GetValue();
            var eventCreationAmount = calculator.UpdatedParameter<EventCreationAmount>().GetValue();
            var estimatedArtifactProfit = calculator.UpdatedParameter<EstimatedArtifactsProfit>().GetValue();
            var pureEUCoeff = calculator.UpdatedParameter<PureEUProfitCoefficient>().GetValue();

            var nodes = calculator.UpdatedParameter<NodesNearestAmount>();
            var mainDeck = calculator.UpdatedParameter<MainDeck>();
            var startDeck = calculator.UpdatedParameter<StartDeck>();

            List<float> relativeDurations = new List<float>(phasesDuration);
            int currentPhase = 0;
            for (int round = 0; round < roundAmount * artifactGainCoefficient; round++)
            {
                relativeDurations[currentPhase]--;
                if (relativeDurations[currentPhase] == 0)
                    currentPhase++;
            }

            List<float> phaseChance = new List<float>();
            foreach (var duration in relativeDurations)
                phaseChance.Add(duration / relativeDurations.Sum());

            float averagePlayersAmount = (minPlayersAmount + maxPlayersAmount) / 2.0f;
            var bonusAmount = new Dictionary<int, float>();
            foreach (var card in mainDeck.deck)
                if (bonusAmount.ContainsKey(card.miningBonus))
                    bonusAmount[card.miningBonus]++;
                else
                    bonusAmount[card.miningBonus] = 1;

            foreach (var card in startDeck.deck)
                if (bonusAmount.ContainsKey(card.miningBonus))
                    bonusAmount[card.miningBonus] += averagePlayersAmount;
                else
                    bonusAmount[card.miningBonus] = averagePlayersAmount;

            var nearestCombinations = new Dictionary<int, List<List<int>>>();
            for (int i = 0; i <= maxPower; i++)
                nearestCombinations[i] = AllCombinations(i, bonusAmount.Count());

            var fieldAnalyzer = new FieldAnalyzer(phasesAmount);

            var powerProfit = new Dictionary<int, float>();
            for (int i = 0; i <= maxPower; i++)
                powerProfit[i] = 0;

            var phasesEventChances = new Dictionary<int, float>();
            float phaseStart = 0;
            for (int i = 0; i < phasesAmount; i++)
            {
                var phaseEnd = phaseStart + phasesDuration[i];
                float phaseStartEventsAmount = averagePlayersAmount * eventCreationAmount * phaseStart / roundAmount;
                float phaseEndEventsAmount = averagePlayersAmount * eventCreationAmount * phaseEnd / roundAmount;
                phasesEventChances[i] = (phaseStartEventsAmount + phaseEndEventsAmount) / continuumNodesAmount / 2;

                phaseStart = phaseEnd + 1;
            }

            foreach (var nodeData in nodes.GetValue())
            {
                var phase = nodeData.phase;
                var phaseNodesAmount = fieldAnalyzer.phasesFields[phase].points.Count();
                var nodesChance = nodeData.nodesAmount / (float)phaseNodesAmount;
                var nodeArchetypeChance = nodesChance * phaseChance[phase];
                var eventChance = phasesEventChances[phase];

                for (int i = 0; i <= nodeData.nearestAmount; i++)
                    foreach (var combination in nearestCombinations[i])
                    {
                        float combinationChance = CombinationChance(combination, bonusAmount, nodeData.nearestAmount, eventChance);
                        float chance = nodeArchetypeChance * combinationChance;
                        var profits = CombinationProfits(combination);

                        foreach (var power in profits.Keys)
                            powerProfit[power] += profits[power] * chance;
                     }
            }

            float estimatedUsage = auPartyAmount * miningAUCoeff * (1 - artifactGainCoefficient);
            float eoupr = estimatedArtifactProfit / (estimatedUsage * pureEUCoeff);

            foreach (var currentPower in powerProfit.Keys)
                if (Math.Abs(powerProfit[currentPower] - eoupr) < Math.Abs(powerProfit[power] - eoupr))
                    power = currentPower;

            profit = powerProfit[power] * estimatedUsage * pureEUCoeff;

            return report;
        }

        private List<List<int>> AllCombinations(int nodesAmount, int maxBonus)
        {
            var combinatinos = new List<List<int>>();
            combinatinos.Add(new List<int>());

            for (int i = 0; i < nodesAmount; i++)
            {
                var newCombinations = new List<List<int>>();
                foreach (var combination in combinatinos)
                    for (int bonus = 0; bonus < maxBonus; bonus++)
                    {
                        var newCombination = new List<int>(combination);
                        newCombination.Add(bonus);
                        newCombinations.Add(newCombination);
                    }

                combinatinos = newCombinations;
            }

            return combinatinos;
        }

        private float CombinationChance(List<int> combination, Dictionary<int, float> bonusAmount, int nearestAmount, float eventChance)
        {
            var eventFactorial = MathAdditional.Factorial(combination.Count());
            var emptyFactorial = MathAdditional.Factorial(nearestAmount - combination.Count());
            var nearestFactorial = MathAdditional.Factorial(nearestAmount);

            var eventsChance = Math.Pow(eventChance, combination.Count());
            var emptyNodesChance = Math.Pow(1 - eventChance, nearestAmount - combination.Count());
            var combinationsChance = nearestFactorial / (eventFactorial * emptyFactorial); //Means empty/event combinations not bonuses combination
            var amountChance = eventsChance * emptyNodesChance * combinationsChance;

            float bonusesChance = 1;
            var availableBonuses = new Dictionary<int, float>(bonusAmount);
            for (int i = 0; i < combination.Count(); i++)
            {
                var bonus = combination[i];
                var bonusChance = availableBonuses[bonus] / availableBonuses.Values.Sum();
                bonusChance = bonusChance > 0 ? bonusChance : 0;
                bonusesChance *= bonusChance;
                availableBonuses[bonus]--;
            }

            return bonusesChance * (float)amountChance;
        }

        private Dictionary<int, int> CombinationProfits(List<int> combination)
        {
            var availableBonuses = new List<int>(combination);
            var profits = new Dictionary<int, int>();
            var accumulation = 0;

            for (int i = 1; i <= combination.Count; i++)
            {
                accumulation += availableBonuses.Max();
                profits[i] = accumulation;
                availableBonuses.Remove(availableBonuses.Max());
            }

            for (int i = combination.Count() + 1; i <= maxPower; i++)
                profits[i] = accumulation;

            return profits;
        }
    }
}
