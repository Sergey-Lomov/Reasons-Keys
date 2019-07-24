using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Activities
{
    class TotalPotential : SingleParameter
    {
        public TotalPotential()
        {
            type = ParameterType.Inner;
            title = "Полный потенциал";
            details = "Полный потенциал игрока за всю партию. Другими словами это кол-во ТЗ, которое мог бы собрать игрок, если бы всю игру только собирал ТЗ со средней добычей.";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float md = calculator.UpdatedSingleValue(typeof(AUPartyAmount));
            float m = calculator.UpdatedSingleValue(typeof(AverageMining));

            value = unroundValue = md * m;

            return calculationReport;
        }
    }
}
