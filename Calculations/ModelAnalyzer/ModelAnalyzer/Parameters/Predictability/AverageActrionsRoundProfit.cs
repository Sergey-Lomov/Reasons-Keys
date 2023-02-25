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

            float tp = RequestParameter<TotalPotential>(calculator).GetValue();
            float mauc = RequestParameter<MiningAUCoef>(calculator).GetValue();
            float peuc = RequestParameter<PureEUProfitCoefficient>(calculator).GetValue();
            float ra = RequestParameter<RoundAmount>(calculator).GetValue();


            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = tp * (1 - mauc + mauc * peuc) / ra;

            return calculationReport;
        }
    }
}
