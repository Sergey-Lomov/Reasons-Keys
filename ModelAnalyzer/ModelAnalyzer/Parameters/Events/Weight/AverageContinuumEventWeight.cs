using System.Linq;

using ModelAnalyzer.Services;
namespace ModelAnalyzer.Parameters.Events.Weight
{
    class AverageContinuumEventWeight : FloatSingleParameter
    {
        public AverageContinuumEventWeight()
        {
            type = ParameterType.Inner;
            title = "Средний вес событий континуума";
            details = "";
            fractionalDigits = 2;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var deck = RequestParmeter<MainDeck>(calculator).deck;

            if (!calculationReport.IsSuccess)
                return calculationReport;

            if (deck.Count != 0)
                value = unroundValue = deck.Select(c => c.weight).Average();
            else
                value = unroundValue = 0;

            return calculationReport;
        }
    }
}
