using ModelAnalyzer.Parameters.Topology;

namespace ModelAnalyzer.Parameters.Mining
{
    class MiningAllocation : ArrayParameter
    {
        private readonly string arraySizeMessage = "Размер массива должен быть равен \"{0}\" + 1 (нулевой центральный): {1}.";

        public MiningAllocation()
        {
            type = ParameterType.Out;
            title = "Распределение добычи";
            details = "Распределение добычи ТЗ по радиусам поля";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float mi = calculator.UpdatedSingleValue(typeof(MiningIncrement));
            float fr = calculator.UpdatedSingleValue(typeof(FieldRadius));

            unroundValues.Clear();
            values.Clear();

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
            float fr = storage.SingleValue(typeof(FieldRadius));
            string title = storage.Parameter(typeof(FieldRadius)).title;
            var issue = string.Format(arraySizeMessage, title, fr + 1);
            ValidateSize(fr + 1, issue, report);

            return report;
        }
    }
}
