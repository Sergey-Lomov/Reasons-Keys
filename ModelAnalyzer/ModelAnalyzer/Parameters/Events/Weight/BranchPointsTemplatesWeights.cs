﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ModelAnalyzer.DataModels;

namespace ModelAnalyzer.Parameters.Events.Weight
{
    using PlayersCombinations = Dictionary<int, List<int[]>>;
    using TemplatesCombinations = Dictionary<BranchPointsTemplate, List<BranchPointsSet>>;
    using TemplatesWeights = Dictionary<BranchPointsTemplate, float>;

    class BranchPointsTemplatesWeights : ArrayParameter
    {
        private Dictionary<BranchPointsTemplate, (int, float)> debug = new Dictionary<BranchPointsTemplate, (int, float)>();

        private BranchPointsTemplate[] templates;
        private PlayersCombinations playersCombinations;
        private TemplatesCombinations templatesCombinations;
        private TemplatesWeights calculatedWeights = new TemplatesWeights();

        private class CalculationConfig
        {
            internal float prcc;
            internal float ppw;
            internal float bpw;
        }

        public BranchPointsTemplatesWeights()
        {
            type = ParameterType.Inner;
            title = "Вес шаблонов очков ветвей";
            details = "Вес задается в порядке: -1/-1, +1/+1, -1/0, 0/-1, +1/0, 0/+1, 0/0";
            fractionalDigits = 2;

            int[] p1 = { +1 };
            int[] m1 = { -1 };
            int[] zero = { };
            templates = new BranchPointsTemplate[] {
                new BranchPointsTemplate(zero, zero),
                new BranchPointsTemplate(p1, zero),
                new BranchPointsTemplate(zero, p1),
                new BranchPointsTemplate(m1, zero),
                new BranchPointsTemplate(zero, m1),
                new BranchPointsTemplate(p1, p1),
                new BranchPointsTemplate(m1, m1),
            };
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            int minpa = (int)calculator.UpdateSingleValue(typeof(MinPlayersAmount));
            int maxpa = (int)calculator.UpdateSingleValue(typeof(MaxPlayersAmount));

            var config = new CalculationConfig();
            config.prcc = calculator.UpdateSingleValue(typeof(PlayerRealisationControlCoefficient));
            config.ppw = calculator.UpdateSingleValue(typeof(PassivePlayerWeight));
            config.bpw = calculator.UpdateSingleValue(typeof(BranchPointWeight));

            SetupPlayersCombinatinos(minpa, maxpa);
            SetupTemplatesCombinatinos(maxpa);

            calculatedWeights.Clear();

            var weights = new List<float>(templates.Count());
            foreach (BranchPointsTemplate template in templates)
            {
                var weight = TemplateWeight(template, minpa, maxpa, config);
                weights.Add(weight);
            }

            weights.Reverse();

            unroundValues.Clear();
            values.Clear();
            values = unroundValues = weights;

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
          //  var size = storage.Parameter(typeof(SizeParamName));
         //   ValidateSize(size, report);
            return report;
        }

        private float TemplateWeight (BranchPointsTemplate template, int minpa, int maxpa, CalculationConfig config)
        {
            var weights = new List<float>();

            foreach (var set in templatesCombinations[template])
            {
                var setWeights = new List<float>();
                for (int pa = 6/*minpa*/; pa <= 6/*maxpa*/; pa++)
                {
                    foreach (var combination in playersCombinations[pa])
                    {
                        var weight = SetWeight(set, combination, config);
                        setWeights.Add(weight);
                    }
                }
                
                weights.AddRange(setWeights);
            }

            return weights.Count() > 0 ? weights.Average() : 0;
        }

        private float SetWeight(BranchPointsSet set, int[] availableBranches, CalculationConfig config)
        {
            if (set.ValidOnBranches(availableBranches))
            {
                var weight = ValidSetWeight(set, availableBranches, config);
                if (set.Template() == templates[6])
                {
                    if (!debug.Keys.Contains(set.Template()))
                    {
                        debug[set.Template()] = (1, weight);
                    }
                    else
                    {
                        var current = debug[set.Template()];
                        current.Item1++;
                        current.Item2 += weight;
                        debug[set.Template()] = current;
                    }

                    Debug.WriteLine("{0}", weight);
                }

                return weight;
            }
            else
            {
                var filteredSet = set.Filter(availableBranches);
                var weight = ValidSetWeight(filteredSet, availableBranches, config);
                if (set.Template() == templates[6])
                    if (!debug.Keys.Contains(filteredSet.Template()))
                    {
                        debug[filteredSet.Template()] = (1, weight);
                    }
                    else
                    {
                        var current = debug[filteredSet.Template()];
                        current.Item1++;
                        current.Item2 += weight;
                        debug[filteredSet.Template()] = current;
                    }   

                return weight;
            }
        }

        private float ValidSetWeight (BranchPointsSet set, int[] availableBranches, CalculationConfig config)
        {
            var weights = new List<float>();

            foreach (int owner in availableBranches)
                weights.Add(SetWeightWithOwner(set, owner, availableBranches.Count(), config));

            return weights.Average();
        }

        private float SetWeightWithOwner(BranchPointsSet set, int owner, int pa, CalculationConfig config)
        {
            float successWeight = 0;
            float failedWeight = 0;

            foreach (BranchPoint bp in set.success)
            {
                if (bp.branch == owner)
                    successWeight += config.bpw * bp.point;
                else
                {
                    successWeight -= config.bpw / (pa - 1) * bp.point;
                    successWeight += config.ppw * bp.point;
                    failedWeight -= config.ppw * bp.point;
                }
            }

            foreach (BranchPoint bp in set.failed)
            {
                if (bp.branch == owner)
                    failedWeight += config.bpw * bp.point;
                else
                {
                    failedWeight -= config.bpw / (pa - 1) * bp.point;
                    failedWeight += config.ppw * bp.point;
                    successWeight -= config.ppw * bp.point;
                }
            }

            float minWeight = Math.Min(successWeight, failedWeight);
            float maxWeight = Math.Max(successWeight, failedWeight);
            float weight = minWeight * (1 - config.prcc) + maxWeight * config.prcc;

            return weight;
        }

        private void SetupPlayersCombinatinos(int minpa, int maxpa)
        {
            var combinations = new PlayersCombinations();
            for (int i = minpa; i <= maxpa; i++)
                combinations[i] = new List<int[]>();

            int maxMask = (1 << maxpa) - 1;
            int minMask = (1 << minpa) - 1;
            for (int mask = minMask; mask <= maxMask; mask++)
            {
                var combination = new List<int>();
                for (int iter = 0; iter < maxpa; iter++)
                    if ((mask & 1 << iter) != 0)
                        combination.Add(iter);

                if (combination.Count >= minpa)
                    combinations[combination.Count].Add(combination.ToArray());
            }

            playersCombinations = combinations;
        }

        private void SetupTemplatesCombinatinos(int maxpa)
        {
            var combinations = new TemplatesCombinations();

            var availableBranches = new List<int>();
            for (int i = 0; i < maxpa; i++)
                availableBranches.Add(i);

            foreach (BranchPointsTemplate template in templates)
            {
                int nodesAmount = template.success.Count() + template.failed.Count();
                var bracnhesCombinations = AllCombinations(nodesAmount, availableBranches);
                combinations[template] = BranchPointsSets(template, bracnhesCombinations);
            }

            templatesCombinations = combinations;
        }

        private List<int[]> AllCombinations (int numbersAmount, List<int> avaialbaleNumbers, bool noSameNumbers = true)
        {
            var combinations = new List<int[]>();
            if (numbersAmount == 0)
                return combinations;

            foreach (int i in avaialbaleNumbers)
            {
                var subNumbers = new List<int>(avaialbaleNumbers);
                if (noSameNumbers)
                    subNumbers.Remove(i);

                var subCombinations = AllCombinations(numbersAmount - 1, subNumbers);
                foreach (var subCombination in subCombinations)
                {
                    var combination = new List<int>();
                    combination.Add(i);
                    combination.AddRange(subCombination);
                    combinations.Add(combination.ToArray());
                }

                if (subCombinations.Count == 0)
                    combinations.Add(new int[] {i});
            }

            return combinations;
        }

        private List<BranchPointsSet> BranchPointsSets(BranchPointsTemplate template, List<int[]> branchesCombinations)
        {
            var sets = new List<BranchPointsSet>();
            foreach (var combination in branchesCombinations)
            {
                var success = new List<BranchPoint>();
                var failed = new List<BranchPoint>();

                for (int i = 0; i < combination.Length; i++)
                {
                    if (i < template.success.Count())
                    {
                        var point = template.success[i];
                        var branchPoint = new BranchPoint(combination[i], point);
                        success.Add(branchPoint);
                    }
                    else if (i - template.success.Count() < template.failed.Count())
                    {
                        var point = template.failed[i - template.success.Count()];
                        var branchPoint = new BranchPoint(combination[i], point);
                        failed.Add(branchPoint);
                    }
                }

                var set = new BranchPointsSet(success, failed);
                sets.Add(set);
            }
            return sets;
        }
    }
}
