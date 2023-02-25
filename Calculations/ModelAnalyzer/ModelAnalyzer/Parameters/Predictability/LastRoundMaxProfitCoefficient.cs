using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Predictability
{
    class LastRoundMaxProfitCoefficient : FloatSingleParameter
    {
        private const string outRangeIssue = "Значение должно быть больше 2 и меньше 3";
        public LastRoundMaxProfitCoefficient()
        {
            type = ParameterType.Indicator;
            title = "Коэф. максимальной выгодности последнего хода фазы";
            details = "Отношение максимальной выгодности последнего хода фазы к средней выгодности";
            fractionalDigits = 2;
            tags.Add(ParameterTag.predictability);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float arp = RequestParmeter<AverageRoundProfit>(calculator).GetValue();
            float lrmp = RequestParmeter<LastRoundMaxProfit>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = lrmp / arp;

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            if (value < 2 || value > 3)
            {
                report.AddIssue(outRangeIssue);
            }

            return report;
        }
    }
}
