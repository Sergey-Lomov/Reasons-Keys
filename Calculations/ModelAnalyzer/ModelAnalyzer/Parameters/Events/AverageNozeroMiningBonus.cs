﻿using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Topology;

namespace ModelAnalyzer.Parameters.Events
{
    class AverageNozeroMiningBonus : FloatSingleParameter
    {
        public AverageNozeroMiningBonus()
        {
            type = ParameterType.Inner;
            title = "Средний ненулевой бонус добычи ТЗ";
            details = "Среднее арифметическое бонуса добычи на всех картах конитнуума, имеющих бонусы добычи";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.mining);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            List<float> mba = RequestParameter<EventMiningBonusAllocation>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float nozeroSum = mba.Sum() - mba[0];
            float average = 0;
            for (int i = 0; i < mba.Count(); i++)
                average += i * mba[i] / nozeroSum;

            value = unroundValue = average;

            return calculationReport;
        }
    }
}
