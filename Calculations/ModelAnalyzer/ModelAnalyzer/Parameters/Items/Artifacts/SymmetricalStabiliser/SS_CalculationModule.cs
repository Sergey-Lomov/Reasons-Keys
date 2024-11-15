﻿using System;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Items.Artifacts.SymmetricalStabiliser
{
    class SS_CalculationModule : CalculationModule
    {
        internal float impactPower;
        internal float secondaryImpactPower;
        internal float symmetricalNodesAmount;

        public SS_CalculationModule()
        {
            title = "CC: модуль подбора";
        }

        internal override ModuleCalculationReport Execute(Calculator calculator)
        {
            calculationReport = new ModuleCalculationReport(this);

            var eca = RequestParmeter<EventCreationAmount>(calculator).GetValue();
            var ua = RequestParmeter<SS_UsageAmount>(calculator).GetValue();
            var cna = RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();
            var eapr = RequestParmeter<EstimatedArtifactsProfit>(calculator).GetValue();
            var eifp = RequestParmeter<EventImpactPrice>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            var eoupr = eapr / ua;

            var available_sna = new int[] {1,2,5};
            var max_sp = (int)Math.Floor(eoupr / eifp - eca / cna);
            var max_ssp = (int)Math.Floor(eoupr / eifp - eca / cna);

            float best_oupr = 0;
            int best_sna = 0;
            int best_sp = 0;
            int best_ssp = 0;

            foreach (var sna in available_sna)
                for (int sp = 1; sp <= max_sp; sp++)
                    for (int ssp = 1; ssp <= max_ssp && ssp <= sp; ssp++)
                    {
                        var current_oupr = (sp + (eca / cna * sna * ssp)) * eifp;
                        if (Math.Abs(eoupr - best_oupr) > Math.Abs(eoupr - current_oupr))
                        {
                            best_oupr = current_oupr;
                            best_sna = sna;
                            best_sp = sp;
                            best_ssp = ssp;
                        }
                    }

            symmetricalNodesAmount = best_sna;
            impactPower = best_sp;
            secondaryImpactPower = best_ssp;

            return calculationReport;
        }
    }
}
