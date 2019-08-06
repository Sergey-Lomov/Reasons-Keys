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

            MainDeck mainDeck = calculator.UpdatedParameter<MainDeck>();
            value = unroundValue = mainDeck.deck.Select(c => c.weight).Average();

            return calculationReport;
        }
    }
}
