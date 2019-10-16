using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Moving;
using ModelAnalyzer.Parameters.Topology;

namespace ModelAnalyzer.Parameters.Items.Standard.SpeedBooster
{
    class SB_Power : FloatArrayParameter
    {
        private const string zeroBoostIssue = "Одна или несколько ступеней ускорителя не дают прироста скорости";
        private const string roundingIssue = "Значительная потеря точности при округлении";

        public SB_Power()
        {
            type = ParameterType.Out;
            title = "Ускоритель: мощность";
            details = "Величина, на которую увеличивают скорость игрока, ступени ускорителя. ПРи округлении используетс специализированный подход, перераспределяющий погрешность - это предотвращает ситуации, когда предыдущая ступень дает больший прирост скоросит чем следующая";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
            tags.Add(ParameterTag.moving);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float isp = RequestParmeter<InitialSpeed>(calculator).GetValue();
            float mspc = RequestParmeter<SB_MaxSpeedCoef>(calculator).GetValue();
            float ad = RequestParmeter<AverageDistance>(calculator).GetValue();
            float ua = RequestParmeter<SB_UpgradesAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValues = new List<float>();
            values = new List<float>();

            float msp = ad * mspc;
            float step = (msp - isp) / ua;
            float roundedStep = (float)Math.Floor(step);
            float roundingLeft = 0;
            for (int i = 1; i <= (int)ua; i++)
            {
                unroundValues.Add(step);
                roundingLeft += step - roundedStep;
                if (roundingLeft < 0.5)
                {
                    values.Add(roundedStep);
                } else
                {
                    roundingLeft -= 1;
                    values.Add(roundedStep + 1);
                }
            }

            values.Sort();

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var size = storage.Parameter<SB_UpgradesAmount>();
            ValidateSize(size, report);

            if (values != null)
            {
                if (values.Contains(0))
                    report.AddIssue(zeroBoostIssue);

                var roundingIssues = validator.ValidateRounding(unroundValues.Sum(), values.Sum());
            }

            return report;
        }
    }
}
