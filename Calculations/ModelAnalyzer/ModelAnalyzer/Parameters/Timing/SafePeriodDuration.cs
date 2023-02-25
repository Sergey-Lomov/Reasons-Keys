using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.PlayerInitial;
using ModelAnalyzer.Parameters.Items.Standard.BaseWeapon;

namespace ModelAnalyzer.Parameters.Timing
{
    class SafePeriodDuration : FloatSingleParameter
    {
        private const string notFoundIssue = "Период безопасности не определен в рамках максимального кол-ва раундов";

        public SafePeriodDuration()
        {
            type = ParameterType.Out;
            title = "Длительность периода безопасности";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.timing);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var fta = RequestParameter<FirstRoundAttackers>(calculator).GetValue();
            var am = RequestParameter<AverageMining>(calculator).GetValue();
            var auma = RequestParameter<AUMoveAmount>(calculator).GetValue();
            var bwd = RequestParameter<BW_Damage>(calculator).GetValue();
            var ieu = RequestParameter<InitialEU>(calculator).GetValue();
            var ra = RequestParameter<RoundAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            for (int round = 0; round <= ra; round++)
            {
                // In documentation formula use round + 1, but indexing in code start from 0 not from 1 like in documentation
                var weaponLevel = System.Math.Min(round, bwd.Count - 1);
                var victimEU = ieu + auma * round * am;
                var atackersDamage = fta * bwd[weaponLevel];
                if (victimEU > atackersDamage)
                {
                    value = unroundValue = round;
                    break;
                }
            }
            
            if (float.IsNaN(value))
            {
                calculationReport.AddIssue(notFoundIssue);
            }

            return calculationReport;
        }
    }
}
