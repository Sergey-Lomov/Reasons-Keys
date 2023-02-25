using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Items;
using ModelAnalyzer.Parameters.Items.Standard.BaseWeapon;
using ModelAnalyzer.Parameters.Items.Standard.RelationsImprover;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Predictability
{
    class AverageRoundProfit : FloatSingleParameter
    {
        public AverageRoundProfit()
        {
            type = ParameterType.Inner;
            title = "Средняя выгодность хода";
            details = "Выгода, которую в среднем приносит игроку один ход";
            fractionalDigits = 2;
            tags.Add(ParameterTag.predictability);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float aarp = RequestParmeter<AverageActrionsRoundProfit>(calculator).GetValue();
            float ra = RequestParmeter<RoundAmount>(calculator).GetValue();
            float ipc = RequestParmeter<ItemPriceCoefficient>(calculator).GetValue();
            float eap = RequestParmeter<EstimatedArtifactsProfit>(calculator).GetValue();
            float bwp = RequestParmeter<BW_Profit>(calculator).GetValue();
            float rispc = RequestParmeter<RI_StandardPurchaseCount>(calculator).GetValue();
            float rip = RequestParmeter<RI_Profit>(calculator).GetValue();
            float aaa = RequestParmeter<ArtifactsActingAmount>(calculator).GetValue();
            float maxpa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            float minpa = RequestParmeter<MinPlayersAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float iupp = (1 - ipc) * (bwp + rip * rispc);
            float aupp = aaa / (maxpa + minpa) * 2 * eap;

            value = unroundValue = (iupp + aupp) / ra + aarp;

            return calculationReport;
        }
    }
}
