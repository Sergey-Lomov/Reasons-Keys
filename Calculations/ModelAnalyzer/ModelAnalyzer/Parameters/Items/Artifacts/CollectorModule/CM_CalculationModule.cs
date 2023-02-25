using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Services.FieldAnalyzer;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule
{
    class CM_CalculationModule : CalculationModule
    {
        internal float usageProfit;
        internal int usageAmount;
        internal float profit;
        internal float maxLimit;
        internal float minLimit;
        
        public CM_CalculationModule()
        {
            title = "МК: модуль расчетов";
        }

        internal override ModuleCalculationReport Execute(Calculator calculator)
        {
            calculationReport = new ModuleCalculationReport(this);

            var ud = RequestParmeter<CM_UsageDifficulty>(calculator).GetValue();
            var anmb = RequestParmeter<AverageNozeroMiningBonus>(calculator).GetValue();
            var eapr = RequestParmeter<EstimatedArtifactsProfit>(calculator).GetValue();
            var peuc = RequestParmeter<PureEUProfitCoefficient>(calculator).GetValue();
            var maxlc = RequestParmeter<CM_MaxLimitCoef>(calculator).GetValue();
            var minlc = RequestParmeter<CM_MinLimitCoef>(calculator).GetValue();
            var adc = RequestParmeter<ArtifactsDischargeCompensation>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            usageProfit = ud * anmb * peuc;
            usageAmount = (int)Math.Round(eapr / usageProfit, MidpointRounding.AwayFromZero);
            profit = usageProfit * usageAmount;
            maxLimit = ud * anmb * maxlc;
            minLimit = adc / usageAmount * minlc;

            return calculationReport;
        }
    }
}
