using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Topology
{
    class AverageDistance : FloatSingleParameter
    {
        private readonly string arrayIssueFormat = "Длина массивов \"{0}\" и \"{1}\" не совпадает.";

        public AverageDistance()
        {
            type = ParameterType.Inner;
            title = "Среднее расстояние";
            details = "Вычисляется на основе средних расстояний фаз и длительности фаз.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.topology);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            List<float> pd = RequestParmeter<AveragePhasesDistance>(calculator).GetValue();
            List<float> pw = RequestParmeter<PhasesWeight>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            if (pd.Count != pw.Count)
            {
                string title1 = RequestParmeter<AveragePhasesDistance>(calculator).title;
                string title2 = RequestParmeter<PhasesWeight>(calculator).title;
                string message = string.Format(arrayIssueFormat, title1, title2);

                calculationReport.Failed(message);
                return calculationReport;
            }

            float sum = 0;
            for (int i = 0; i < pd.Count; i++)
                sum += pd[i] * pw[i];

            value = unroundValue = sum / pw.Sum();
            return calculationReport;
        }
    }
}
