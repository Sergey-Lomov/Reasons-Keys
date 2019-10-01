﻿using System;

namespace ModelAnalyzer.Parameters.Items.Artifacts.LachesisNeedle
{
    class LN_ConnectionsAmount : SingleParameter
    {
        public LN_ConnectionsAmount()
        {
            type = ParameterType.Out;
            title = "ИЛ: кол-во связей";
            details = "Кол-во соединений которые можно установить с помощью Иглы Лахесис";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ocpr = calculator.UpdatedSingleValue(typeof(LN_OneConnectionProfit));
            float eapr = calculator.UpdatedSingleValue(typeof(EstimatedArtifactsProfit));

            unroundValue = eapr / ocpr;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
