using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using ModelAnalyzer.Parameters;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.UI.Factories
{
    class EditFormsFactory
    {
        private Dictionary<Type, Type> editFormsTypes = new Dictionary<Type, Type>();

        public EditFormsFactory ()
        {
            editFormsTypes.Add(typeof(FloatArrayParameter), typeof(FloatArrayEditForm));
            editFormsTypes.Add(typeof(FloatSingleParameter), typeof(FloatSingleEditForm));
            editFormsTypes.Add(typeof(RoutesMap), typeof(UnavailableEditingForm));
            editFormsTypes.Add(typeof(BranchPointsAllocation), typeof(UnavailableEditingForm));
            editFormsTypes.Add(typeof(DeckParameter), typeof(UnavailableEditingForm));
        }

        public ParameterEditForm EditFormForParameter(Parameter p)
        {
            var formTypes = editFormsTypes.Where(pt => pt.Key.IsAssignableFrom(p.GetType()));
            if (formTypes.Count() == 0)
                return null;

            var formType = formTypes.First().Value;
            if (formType.IsSubclassOf(typeof(ParameterEditForm)))
            {
                var form = (ParameterEditForm)Activator.CreateInstance(formType);
                form.SetParameter(p);
                return form;
            }

            return null;
        }
    }
}
