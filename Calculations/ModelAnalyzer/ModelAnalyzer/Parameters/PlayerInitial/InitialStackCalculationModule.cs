using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class InitialStackCalculationModule : CalculationModule
    {
        internal int initialStackSize;
        internal float realInitialStackArtifactChance;

        private static readonly int[] availableValues = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public InitialStackCalculationModule()
        {
            title = "Изначальная раздача: модуль подбора";
        }

        internal override ModuleCalculationReport Execute(Calculator calculator)
        {
            calculationReport = new ModuleCalculationReport(this);

            var ar = RequestParmeter<ArtifactsRarity>(calculator).GetValue();
            var cna = (int)RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();
            var isac = RequestParmeter<InitialStackArtifactChance>(calculator).GetValue();
            var deck = RequestParmeter<MainDeck>(calculator).deck;

            if (!calculationReport.IsSuccess)
                return calculationReport;

            // Use real value instead fromula provided by mechanica doc for prevent rounding issues
            var nacea = deck.FindAll(e => !e.provideArtifact).Count;

            double tsca(int iss) => MathAdditional.Miltyply(cna - iss + 1, cna);
            double nasca(int iss) => MathAdditional.Miltyply(nacea - iss + 1, nacea);
            double isnac(int iss) => nasca(iss) / tsca(iss);
            double cisac(int iss) => 1 - isnac(iss);
            double distance(int iss) => System.Math.Abs(isac - cisac(iss));

            var sortedValues = availableValues.OrderBy(value => distance(value));
            initialStackSize = sortedValues.First();
            realInitialStackArtifactChance = (float)cisac(initialStackSize);

            return calculationReport;
        }
    }
}
