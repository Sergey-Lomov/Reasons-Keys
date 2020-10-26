using System.Linq;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Events
{
    class MaxContinuumBPDeviation : FloatSingleParameter
    {
        private const float criticalValue = 0.05f;
        private const string bigValueIssue = "Отклонение слишком велико, нужно увеличить параметр \"Кол-во итераций при балансировке очков ветвей\"";

        public MaxContinuumBPDeviation()
        {
            type = ParameterType.Indicator;
            title = "Максимальное отклонение очков ветви в континууме";
            details = "Этот параметр отражает максимальное относительное отклонение среди значений параметра “Среднее кол-во очков ветви в континууме” от среднего арифметического этих значений. Например если “Максимальное отклонение очков ветви в континууме” имеет значение 0.15, это означает, что колода позволяет одной из ветвей получать очки на 15% легче, чем это дается среднестатистической ветви.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.branchPoints);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var acbp = RequestParmeter<AverageContinuumBP>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = acbp.Deviation() / acbp.Average();

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);

            if (unroundValue > criticalValue)
                report.AddIssue(bigValueIssue);

            return report;
        }
    }
}
