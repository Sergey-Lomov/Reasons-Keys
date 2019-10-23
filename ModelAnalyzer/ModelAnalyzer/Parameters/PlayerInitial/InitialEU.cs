using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Moving;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Items.Standard.BaseWeapon;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class InitialEU : FloatSingleParameter
    {
        public InitialEU()
        {
            type = ParameterType.Out;
            title = "Начальный запас ТЗ";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
            tags.Add(ParameterTag.mining);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float r = RequestParmeter<FieldRadius>(calculator).GetValue();
            float mp = RequestParmeter<MotionPrice>(calculator).GetValue();
            float ecp_eu = RequestParmeter<EventCreationPriceEU>(calculator).GetValue();
            float cmb = RequestParmeter<CenterMiningBonus>(calculator).GetValue();

            List<float> bwfp = RequestParmeter<BW_FullPrice>(calculator).GetValue();
            List<float> bwsp = RequestParmeter<BW_ShotPrice>(calculator).GetValue();
            List<float> bwd = RequestParmeter<BW_Damage>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float goToEdge = r * mp + 1;
            float oneShotSurvive = bwd[0] + 1;
            float buyWeaponAndShoot = bwfp[0] + bwsp[0] + 1 - cmb * 2;
            float prepare3Events = ecp_eu * 3 + 1 - cmb * 2;

            unroundValue = new List<float>{goToEdge, oneShotSurvive, buyWeaponAndShoot, prepare3Events}.Max();
            value = (float)Math.Ceiling(unroundValue);

            return calculationReport;
        }
    }
}
