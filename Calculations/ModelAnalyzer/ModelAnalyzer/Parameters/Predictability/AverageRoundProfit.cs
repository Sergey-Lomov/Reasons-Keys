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

            float aarp = RequestParameter<AverageActrionsRoundProfit>(calculator).GetValue();
            float ra = RequestParameter<RoundAmount>(calculator).GetValue();
            float ipc = RequestParameter<ItemPriceCoefficient>(calculator).GetValue();
            float eap = RequestParameter<EstimatedArtifactsProfit>(calculator).GetValue();
            float bwp = RequestParameter<BW_Profit>(calculator).GetValue();
            float rispc = RequestParameter<RI_StandardPurchaseCount>(calculator).GetValue();
            float rip = RequestParameter<RI_Profit>(calculator).GetValue();
            float aaa = RequestParameter<ArtifactsActingAmount>(calculator).GetValue();
            float maxpa = RequestParameter<MaxPlayersAmount>(calculator).GetValue();
            float minpa = RequestParameter<MinPlayersAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float iupp = (1 - ipc) * (bwp + rip * rispc);
            float aupp = aaa / (maxpa + minpa) * 2 * eap;

            value = unroundValue = (iupp + aupp) / ra + aarp;

            return calculationReport;
        }
    }
}
