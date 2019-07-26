using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAnalyzer.Parameters.Events
{
    using BranchPiar = ValueTuple<int, int>;

    abstract class BranchPointsAllocation : Parameter
    {
        internal List<BranchPiar> values = new List<BranchPiar>();

        const string invalidStringMessage = "Невозможно перобразовать строку: \"{0}\" в \"{1}\"";
        const string pairsSeparator = " ";
        const string branchesSeparator = "-";

        public override void SetupByString(string str)
        {
            var subs = str.Split(pairsSeparator.ToCharArray());

            foreach (var sub in subs)
            {
                var items = sub.Split(branchesSeparator.ToCharArray());
                if (items.Count() < 2)
                    ThrowInvalidString(str);

                BranchPiar pair;
                if (!int.TryParse(items[0], out pair.Item1))
                    ThrowInvalidString(str);
                if (!int.TryParse(items[1], out pair.Item2))
                    ThrowInvalidString(str);
            }
        }

        public override string StringRepresentation()
        {
            var str = "";

            for (int i = 0; i < values.Count; i++)
            {
                var pair = values[i];
                string elementsSeparator = i < values.Count - 1 ? pairsSeparator : "";
                str += pair.Item1 + branchesSeparator + pair.Item2 + elementsSeparator;
            }

            return str;
        }

        public override string UnroundValueToString()
        {
            return ValueToString();
        }

        public override string ValueToString()
        {
            return StringRepresentation();
        }

        private void ThrowInvalidString(string str)
        {
            string issue = string.Format(invalidStringMessage, str, title);
            MAException e = new MAException(issue, this);
            throw e;
        }

        internal ParameterCalculationReport Calculate(Calculator calculator, bool prioritizeEven)
        {

            calculationReport = new ParameterCalculationReport(this);

            float pa = calculator.UpdatedSingleValue(typeof(MaxPlayersAmount));

            if (float.IsNaN(pa))
            {
                string title = calculator.ParameterTitle(typeof(MaxPlayersAmount));
                FailCalculationByInvalidIn(new string[] {title});
                return calculationReport;
            }

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
