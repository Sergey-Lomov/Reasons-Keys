using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.Parameters.Events.Weight
{
    class AverageContinuumEventWeight : SingleParameter
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

            var deck = calculator.UpdatedParameter<MainDeck>().deck;

            if (deck.Count != 0)
                value = unroundValue = deck.Select(c => c.weight).Average();
            else
                value = unroundValue = 0;

            return calculationReport;
        }
    }
}
