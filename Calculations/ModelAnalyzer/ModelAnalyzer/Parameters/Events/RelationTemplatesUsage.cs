using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters.Timing;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Services;
using ModelAnalyzer.Services.FieldAnalyzer;

namespace ModelAnalyzer.Parameters.Events
{
    class RelationTemplatesUsage : Parameter
    {
        private const string nullStub = "-";
        private const string notAllTemplatesIssue = "Кол-во шаблонов для которых расчитано кол-во карт (включая 0 карт) меньше чем общее возможное кол-во шаблонов";

        private Dictionary<EventRelationsTemplate, RelationsTemplateUsageInfo> counts = null;

        public RelationTemplatesUsage()
        {
            type = ParameterType.Inner;
            title = "Использование шаблонов расположения";
            details = "Этот параметр отражает для каждого шаблона то, как он используется в игре: кол-во карт с этим шаблоном и его применимость";
            tags.Add(ParameterTag.events);
        }

        public override bool isValueNull()
        {
            return counts == null;
        }

        public override void SetupByString(string str)
        {
            // Not possible. This parameter should be calculated.
        }

        public override string StringRepresentation()
        {
            if (isValueNull()) return nullStub;
            return GetNoZero().Count().ToString() + " шаблонов";
        }

        protected override void NullifyValue()
        {
            counts = null;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var na = (int)RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();
            var pd = RequestParmeter<PhasesDuration>(calculator).GetValue();
            var fr = (int)RequestParmeter<FieldRadius>(calculator).GetValue();
            var rna = RequestParmeter<RoundNodesAmount>(calculator).GetValue();
            var fec = RequestParmeter<FrontEventsCoef>(calculator).GetValue();
            var mrtu = RequestParmeter<MinRelationsTemplateUsability>(calculator).GetValue();
            var emr = (int)RequestParmeter<EventMaxRelations>(calculator).GetValue();
            var mbr = (int)RequestParmeter<MinBackRelations>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            var fieldAnalyzer = new FieldAnalyzer(phasesCount: pd.Count);
            var intPd = pd.Select(v => (int)v).ToList();
            fieldAnalyzer.templateUsabilityPrecalculations(intPd, fr);
            float usability(EventRelationsTemplate t) => fieldAnalyzer.templateUsability(t, rna);

            var allDirectionsTemplates = EventRelationsTemplate.allTemplates(Field.nearesNodesAmount);
            var templatesUsability = allDirectionsTemplates.ToDictionary(t => t, t => usability(t));
            var filteredTemplates = templatesUsability.Where(kvp => kvp.Value >= mrtu).ToList();

            bool minRelValid(EventRelationsTemplate t) => t.directionsAmount() > 0;
            bool maxRelValid(EventRelationsTemplate t) => t.directionsAmount() <= emr;
            bool minBackValid(EventRelationsTemplate t) => t.backAmount() >= mbr;
            bool validate(EventRelationsTemplate t) => minRelValid(t) && maxRelValid(t) && minBackValid(t);

            filteredTemplates = filteredTemplates.Where(kvp => validate(kvp.Key)).ToList();
            var templates_2d = filteredTemplates.Where(p => p.Key.containsFront()).ToDictionary(p => p.Key, p => p.Value);
            var templates_ob = filteredTemplates.Where(p => !p.Key.containsFront()).ToDictionary(p => p.Key, p => p.Value);
            var cardsAmount_2d = (int)Math.Round(na * fec, MidpointRounding.AwayFromZero);
            int cardsAmount_ob = na - cardsAmount_2d;

            counts = new Dictionary<EventRelationsTemplate, RelationsTemplateUsageInfo>();
            CalculateForAmounts(calculator, cardsAmount_2d, templates_2d);
            CalculateForAmounts(calculator, cardsAmount_ob, templates_ob);

            var missedTemplates = allDirectionsTemplates.Where(t => !counts.ContainsKey(t));
            foreach (var template in missedTemplates)
            {
                var info = new RelationsTemplateUsageInfo();
                info.cardsCount = 0;
                info.usability = usability(template);
                counts[template] = info;
            }

            return calculationReport;
        }

        private void CalculateForAmounts(Calculator calculator, int cardsAmount, Dictionary<EventRelationsTemplate, float> templatesUsabilities)
        {
            var cards = new List<EventCard>();
            var ordered = templatesUsabilities.OrderByDescending(kvp => kvp.Value);
            var templates = ordered.Select(kvp => kvp.Key).ToList();
            var usabilities = ordered.Select(kvp => kvp.Value).ToList();
            var groupsAmounts = MathAdditional.AmountsForAllocation(cardsAmount, usabilities, calculationReport);

            for (int groupIter = 0; groupIter < groupsAmounts.Count(); groupIter++)
            {
                var template = templates[groupIter];
                var info = new RelationsTemplateUsageInfo();
                info.cardsCount = groupsAmounts[groupIter];
                info.usability = usabilities[groupIter];
                counts[template] = info;
            }
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);

            var allTemplates = EventRelationsTemplate.allTemplates(Field.nearesNodesAmount);
            if (counts.Count < allTemplates.Count)
                report.AddIssue(notAllTemplatesIssue);

            return report;
        }

        internal Dictionary<EventRelationsTemplate, RelationsTemplateUsageInfo> GetValue()
        {
            return isValueNull() ? new Dictionary<EventRelationsTemplate, RelationsTemplateUsageInfo>() : counts;
        }

        internal Dictionary<EventRelationsTemplate, RelationsTemplateUsageInfo> GetNoZero()
        {
            if (isValueNull()) return new Dictionary<EventRelationsTemplate, RelationsTemplateUsageInfo>();
            return counts.Where(kvp => kvp.Value.cardsCount != 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
