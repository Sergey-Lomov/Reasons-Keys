using System;
using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;

namespace ModelAnalyzer.Parameters.Events
{
    abstract class BranchPointsAllocation : PairsArrayParameter
    {

        public BranchPointsAllocation ()
        {
            tags.Add(ParameterTag.events);
        }

        internal ParameterCalculationReport Calculate(Calculator calculator, bool prioritizeEven)
        {
            calculationReport = new ParameterCalculationReport(this);

            float pa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            values = new List<ValueTuple<int, int>>();

            float combinationsAmount = 0;
            for (int i = 1; i < pa; i++)
                combinationsAmount += i;

            float unroundLimit = combinationsAmount / pa;
            int[] limits = new int[(int)pa];

            for (int i = 0; i < pa; i++)
            {
                if (prioritizeEven)
                    limits[i] = i % 2 == 0 ? (int)Math.Ceiling(unroundLimit) : (int)Math.Floor(unroundLimit);
                else
                    limits[i] = i % 2 != 0 ? (int)Math.Ceiling(unroundLimit) : (int)Math.Floor(unroundLimit);
            }

            for (int first = 0; first < pa; first++)
            {
                int usage = 0;
                for (int second = 0; second < pa; second++)
                {
                    if (first == second || usage == limits[first])
                        continue;

                    bool existSame = values.Exists(p => p.Item1 == first && p.Item2 == second);
                    bool existOpposite = values.Exists(p => p.Item1 == second && p.Item2 == first);

                    if (existSame || existOpposite)
                        continue;

                    usage++;
                    values.Add((first, second));
                }
            }

            return calculationReport;
        }
    }
}
