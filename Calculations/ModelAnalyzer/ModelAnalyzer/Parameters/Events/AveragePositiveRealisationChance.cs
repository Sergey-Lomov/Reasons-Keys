using System;
using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.PlayerInitial;

namespace ModelAnalyzer.Parameters.Events
{
    class AveragePositiveRealisationChance : FloatArrayParameter
    {
        public AveragePositiveRealisationChance()
        {
            type = ParameterType.Indicator;
            title = "Cредний шанс на то, что событие произойдет";
            details = "Зависит от кол-ва игроков";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var minpa = (int)RequestParmeter<MinPlayersAmount>(calculator).GetValue();
            var maxpa = (int)RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            var cna = RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();
            var eca = calculator.UpdatedParameter<EventCreationAmount>().GetValue();
            var bea = RequestParmeter<BackEdgesAmount>(calculator).GetValue();
            var bric = RequestParmeter<BackRelationIgnoringChance>(calculator).GetValue();
            var tfra = RequestParmeter<FrontReasonsInEstimatedDeck>(calculator).GetValue();
            var tfba = RequestParmeter<FrontBlockersInEstimatedDeck>(calculator).GetValue();
            var mainDeck = RequestParmeter<MainDeckCore>(calculator).deck;
            var startDeck = RequestParmeter<StartDeck>(calculator).deck;

            if (!calculationReport.IsSuccess)
                return calculationReport;

            ClearValues();

            float ane = bea / cna;
            float edca(int pa) => mainDeck.Count() + startDeck.Count() * pa / maxpa;
            float calcAfra(int pa) => tfra[pa - minpa] / edca(pa) * pa * eca / bea * ane;
            float calcAfba(int pa) => tfba[pa - minpa] / edca(pa) * pa * eca / bea * ane;

            for (int pa = minpa; pa <= maxpa; pa++)
            {
                var afra = calcAfra(pa);
                var afba = calcAfba(pa);

                var dichotomiser = new Dichotomiser();
                dichotomiser.left = 1;
                dichotomiser.right = 0;
                dichotomiser.func = aprc => dihotomiserFunc(aprc, mainDeck, afra, afba, bric[pa - minpa]);
                var value = (float)dichotomiser.calculate();
                unroundValues.Add(value);
            }

            values = unroundValues;

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var minpa = (int)storage.Parameter<MinPlayersAmount>().GetValue();
            var maxpa = (int)storage.Parameter<MaxPlayersAmount>().GetValue();

            ValidateSize(maxpa - minpa + 1, report);
            return report;
        }

        private double dihotomiserFunc (double aprc, List<EventCard> deck, double afra, double afba, double bric)
        {
            double sum = 0;
            foreach (var card in deck)
                sum += EventCardsAnalizer.PositiveRealisationChance(card, aprc, afra, afba, bric);
            return sum / deck.Count() - aprc;
        }
    }
}
