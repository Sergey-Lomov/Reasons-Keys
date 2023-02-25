using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Items.Artifacts;

namespace ModelAnalyzer.Parameters.Items.Standard
{
    class EstimatedBaseItemRoundProfit : FloatSingleParameter
    {
        public EstimatedBaseItemRoundProfit()
        {
            type = ParameterType.Inner;
            title = "Расчетная выгодность базовых предметов за ход";
            details = "Выгодность одного использования для базовых предметов намного ниже чем для артефактов, поэтому удобно подстраивать базовые предметы под артефакты а не наоборот.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float aarp = RequestParameter<AverageArtifactRoundProfit>(calculator).GetValue();
            float apc = RequestParameter<ArtifactsProfitCoefficient>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = aarp / apc ;

            return calculationReport;
        }
    }
}
