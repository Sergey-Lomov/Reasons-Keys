using ModelAnalyzer.Services;

namespace $rootnamespace$
{
    class $safeitemname$ : CalculationModule
    {
        public $safeitemname$()
        {
            title = "";
        }

        internal override ModuleCalculationReport Execute(Calculator calculator)
        {
            var report = new ModuleCalculationReport(this);

            var p = RequestParmeter<ParamName>(calculator, report).GetValue();

            if (!report.IsSuccess)
                return report;

    return report;
        }
    }
}
