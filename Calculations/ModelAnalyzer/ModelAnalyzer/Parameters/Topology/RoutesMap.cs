using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Services.FieldAnalyzer;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Topology
{
    using PhaseRoutes = Dictionary<int, HashSet<FieldRoute>>;

    class RoutesMap : Parameter
    {
        const string valueStub = "Недоступно";

        internal PhaseRoutes phasesRoutes = null;

        public RoutesMap()
        {
            type = ParameterType.Inner;
            title = "Карта путей";
            details = "Хранит карту всех возможных путей между всеми парами узлов во всех конфигурациях поля (фазах). Карта генерируется в модуле FieldAnalyzer и используется другими параметрами из группы \"Топология\". Просмотр недоступен.";
            tags.Add(ParameterTag.topology);
        }

        internal override Parameter Copy()
        {
            var copy = base.Copy() as RoutesMap;

            if (phasesRoutes != null)
            {
                copy.phasesRoutes = new PhaseRoutes();
                foreach (var phaseRoutes in phasesRoutes)
                    copy.phasesRoutes[phaseRoutes.Key] = new HashSet<FieldRoute>(phaseRoutes.Value);
            }

            return copy;
        }

        internal override bool IsEqual(Parameter p)
        {
            if (!(p is RoutesMap))
                return false;

            var baseCheck = base.IsEqual(p);
            var fsp = p as RoutesMap;

            if (phasesRoutes == null || fsp.phasesRoutes == null)
                return phasesRoutes == null && fsp.phasesRoutes == null;

            bool routesCheck = true;
            foreach (var phase in phasesRoutes.Keys)
            {
                var routes = phasesRoutes[phase];
                var otherRoutes = fsp.phasesRoutes[phase];
                if (!routes.SequenceEqual(otherRoutes))
                    routesCheck = false;
            }

            return baseCheck && routesCheck;
        }

        public override void SetupByString(string str)
        {
            // Not possible. This parameter should be calculated.
        }

        public override string StringRepresentation()
        {
            return valueStub;
        }

        public int GetRoutesAmount ()
        {
            if (phasesRoutes == null)
                return 0;

            var routesAmount = 0;
            foreach (var routes in phasesRoutes.Values)
                routesAmount += routes.Count;

            return routesAmount;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            int pa = (int)RequestParameter<PhasesAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            phasesRoutes = new FieldAnalyzer(pa).PhasesRoutes();

            return calculationReport;
        }

        public override bool IsValueNull()
        {
            return phasesRoutes == null;
        }

        protected override void NullifyValue()
        {
            phasesRoutes = null;
        }

        internal override bool VerifyValue()
        {
            bool baseVerify = base.VerifyValue();

            return baseVerify && phasesRoutes != null;
        }
    }
}
