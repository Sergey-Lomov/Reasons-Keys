using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Items;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Predictability
{
    class AverageActrionsRoundProfit : FloatSingleParameter
    {
        public AverageActrionsRoundProfit()
        {
            type = ParameterType.Inner;
            title = "Средняя выгодность совершения действий за ход";
            details = "Под действияфми подразумевается движение, организация событий, оказание воздействия и добыча ТЗ";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.predictability);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float tp = RequestParmeter<TotalPotential>(calculator).GetValue();
            float mauc = RequestParmeter<MiningAUCoef>(calculator).GetValue();
            float peuc = RequestParmeter<PureEUProfitCoefficient>(calculator).GetValue();
            float ra = RequestParmeter<RoundAmount>(calculator).GetValue();


            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = tp * (1 - mauc + mauc * peuc) / ra;

            return calculationReport;
        }
    }
}
