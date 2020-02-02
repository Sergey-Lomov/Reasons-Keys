using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Topology;

namespace ModelAnalyzer.Parameters.Mining
{
    class MiningAllocation : FloatArrayParameter
    {
        private readonly string arraySizeMessage = "Размер массива должен быть равен \"{0}\" + 1 (нулевой центральный): {1}.";

        public MiningAllocation()
        {
            type = ParameterType.Out;
            title = "Распределение добычи";
            details = "Распределение добычи ТЗ по радиусам поля";
            fractionalDigits = 0;
            tags.Add(ParameterTag.mining);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float mi = RequestParmeter<MiningIncrement>(calculator).GetValue();
            float fr = RequestParmeter<FieldRadius>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;
            ClearValues();

            for (int i = 0; i <= fr; i++)
            {
                unroundValues.Add(mi * i);
                var roundValue = (float)System.Math.Round(mi * i);
                values.Add(roundValue);
            }

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            float fr = storage.Parameter<FieldRadius>().GetValue();
            string title = storage.Parameter<FieldRadius>().title;
            var issue = string.Format(arraySizeMessage, title, fr + 1);
            ValidateSize(fr + 1, issue, report);

            return report;
        }
    }
}
