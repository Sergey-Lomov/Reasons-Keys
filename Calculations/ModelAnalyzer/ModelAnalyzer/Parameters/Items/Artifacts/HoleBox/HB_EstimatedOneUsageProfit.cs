using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.HoleBox
{
    class HB_EstimatedOneUsageProfit : FloatSingleParameter
    {
        public HB_EstimatedOneUsageProfit()
        {
            type = ParameterType.Inner;
            title = "ДК: расчетная выгодность одного использования";
            details = "Выгодность использования ДК рассчитывается с допущением, что игрок будет использовать артефакт таким образом, что полученные ТЗ возвращать уже не придется. Поэтому по сути расчетная выгодность применения просто произведение максимальной транзакции на коэф. выгодности чистого ТЗ.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float hbmt = RequestParameter<HB_MaxTransaction>(calculator).GetValue();
            float peuc = RequestParameter<PureEUProfitCoefficient>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = hbmt * peuc;

            return calculationReport;
        }
    }
}
