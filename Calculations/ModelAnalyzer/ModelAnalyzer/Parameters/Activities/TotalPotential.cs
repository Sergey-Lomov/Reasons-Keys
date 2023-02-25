using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Activities
{
    class TotalPotential : FloatSingleParameter
    {
        public TotalPotential()
        {
            type = ParameterType.Inner;
            title = "Полный потенциал";
            details = "Полный потенциал игрока за всю партию. Другими словами это кол-во ТЗ, которое мог бы собрать игрок, если бы всю игру только собирал ТЗ со средней добычей.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float md = RequestParameter<AUPartyAmount>(calculator).GetValue();
            float m = RequestParameter<AverageMining>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = md * m;

            return calculationReport;
        }
    }
}
