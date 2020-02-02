using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Services.FieldAnalyzer;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Topology
{
    class NearestNodesData
    {
        internal int phase;
        internal int nearestAmount;
        internal int nodesAmount;

        internal NearestNodesData(int _phase, int _nearestAmount, int _nodesAmount)
        {
            phase = _phase;
            nearestAmount = _nearestAmount;
            nodesAmount = _nodesAmount;
        }
    }

    class NodesNearestAmount : Parameter
    {
        private readonly string stringRepresentationStub = "stub";
        private readonly int maxNearestAmount = 6;

        private List<NearestNodesData> value = null;

        public NodesNearestAmount()
        {
            type = ParameterType.Inner;
            title = "Кол-во узлов имеющих n соседних узлов";
            details = "Хранит кол-ва узлов с различным кол-вом соседних узлов в различных фазах";
            tags.Add(ParameterTag.topology);
        }

        public List<NearestNodesData> GetValue()
        {
            return value;
        }

        public override void SetupByString(string str)
        {
            // Should be recalculated every app restart
        }

        public override string StringRepresentation()
        {
            return stringRepresentationStub;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);
            value = new List<NearestNodesData>();

            int pa = (int)RequestParmeter<PhasesAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            var fieldAnalyzer = new FieldAnalyzer(pa);

            for (int phase = 0; phase < pa; phase++)
            {
                var phaseData = new Dictionary<int, NearestNodesData>();
                for (int nearestAmount = 1; nearestAmount <= maxNearestAmount; nearestAmount++)
                {
                    var data = new NearestNodesData(phase, nearestAmount, 0);
                    phaseData[nearestAmount] = data;
                }

                Field field = fieldAnalyzer.phasesFields[phase];
                foreach (var node in field.points)
                {
                    var nearestAmount = field.AvailableNearestFor(node).Count();
                    phaseData[nearestAmount].nodesAmount++;
                }

                value.AddRange(phaseData.Values);
            }

            return calculationReport;
        }

        protected override void NullifyValue()
        {
            value = null;
        }

        internal override bool VerifyValue()
        {
            bool baseVerify = base.VerifyValue();

            return baseVerify && value != null;
        }
    }
}
