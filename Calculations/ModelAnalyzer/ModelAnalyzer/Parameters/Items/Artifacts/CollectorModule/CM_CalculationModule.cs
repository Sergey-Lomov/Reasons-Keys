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
            var report = new ModuleCalculationReport(this);

            var ud = RequestParmeter<CM_UsageDifficulty>(calculator, report).GetValue();
            var amb = RequestParmeter<AverageMiningBonus>(calculator, report).GetValue();
            var eapr = RequestParmeter<EstimatedArtifactsProfit>(calculator, report).GetValue();
            var peuc = RequestParmeter<PureEUProfitCoefficient>(calculator, report).GetValue();
            var maxlc = RequestParmeter<CM_MaxLimitCoef>(calculator, report).GetValue();
            var minlc = RequestParmeter<CM_MinLimitCoef>(calculator, report).GetValue();
            var adc = RequestParmeter<ArtifactsDischargeCompensation>(calculator, report).GetValue();

            if (!report.IsSuccess)
                return report;

            float eupr = ud * amb * peuc;
            usageAmount = (int)Math.Round(eapr / eupr, MidpointRounding.AwayFromZero);
            profit = eupr * usageAmount;
            maxLimit = ud * amb * maxlc;
            minLimit = adc / usageAmount * minlc;

            return report;
        }
    }
}
