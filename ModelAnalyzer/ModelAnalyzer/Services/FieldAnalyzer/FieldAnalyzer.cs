using System.Collections.Generic;

namespace ModelAnalyzer.Services.FieldAnalyzer
{
    class FieldAnalyzer
    {
        internal Dictionary<int, Field> phasesFields = new Dictionary<int, Field>();

        internal FieldAnalyzer(int phasesCount)
        {
            var factory = new FieldFabric();
            for (int phase = 0; phase < phasesCount; phase++)
                phasesFields[phase] = factory.field(phasesCount - 1, phase);
        }

        internal Dictionary<int, HashSet<FieldRoute>> phasesRoutes ()
        {
            var phasesRoutes = new Dictionary<int, HashSet<FieldRoute>>();

            foreach (var phaseField in phasesFields)
                phasesRoutes[phaseField.Key] = phaseField.Value.AllRoutes();

            return phasesRoutes;
        }
    }
}
