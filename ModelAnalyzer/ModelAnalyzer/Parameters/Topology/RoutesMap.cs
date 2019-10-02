using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Services.FieldAnalyzer;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Topology
{
    class RoutesMap : Parameter
    {
        const string valueStub = "Недоступно";

        internal Dictionary<int, HashSet<FieldRoute>> phasesRoutes;

        public RoutesMap()
        {
            type = ParameterType.Inner;
            title = "Карта путей";
            details = "Хранит карту всех возможных путей между всеми парами узлов во всех конфигурациях поля (фазах). Карта генерируется в модуле FieldAnalyzer и используется другими параметрами из группы \"Топология\". Просмотр недоступен.";
            tags.Add(ParameterTag.topology);
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
            var routesAmount = 0;
            foreach (var routes in phasesRoutes.Values)
                routesAmount += routes.Count;

            return routesAmount;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            int pa = (int)calculator.UpdatedSingleValue(typeof(PhasesAmount));
            phasesRoutes = new FieldAnalyzer(pa).phasesRoutes();

            return calculationReport;
        }
    }
}
