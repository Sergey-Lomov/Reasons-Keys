using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Predictability
{
    class RoundMaxProfitCoefficient : FloatSingleParameter
    {
        private const string outRangeIssue = "Значение должно быть больше 3 и меньше 4.5";

        public RoundMaxProfitCoefficient()
        {
            type = ParameterType.Indicator;
            title = "Коэф. максимальной выгодности хода";
            details = "Отношение максимальной выгодности обычного хода к средней выгодности";
            fractionalDigits = 2;
            tags.Add(ParameterTag.predictability);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float arp = RequestParmeter<AverageRoundProfit>(calculator).GetValue();
            float rmp = RequestParmeter<RoundMaxProfit>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = rmp / arp;

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            if (value < 3 || value > 4.5)
            {
                report.AddIssue(outRangeIssue);
            }

            return report;
        }
    }
}
