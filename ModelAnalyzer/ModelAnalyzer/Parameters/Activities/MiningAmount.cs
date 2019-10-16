using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Activities
{
    class MiningAmount : FloatSingleParameter
    {
        public MiningAmount()
        {
            type = ParameterType.Inner;
            title = "Стандартное кол-во актов добычи ТЗ";
            details = "Кол-во раз, которые игрок должен добывать ТЗ при стандартной добче и стандартном расходе ТЗ";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.mining);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float au = RequestParmeter<AUPartyAmount>(calculator).GetValue();
            float mc = RequestParmeter<MiningAUCoef>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = au * mc;

            return calculationReport;
        }
    }
}
