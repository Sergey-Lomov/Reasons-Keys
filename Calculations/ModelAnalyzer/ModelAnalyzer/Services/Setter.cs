using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAnalyzer.Services
{
    class Setter
    {
        public static void EvenDistributionSet<O, V>(
            List<O> objects,
            List<V> values,
            List<int> amounts,
            Action<O, V> setter)
        {
            // If amounts have only one amount set value to all objects. This is recursion exit point.
            /*if (amounts.Count() == 1)
            {
                objects.ForEach(o => setter(o, values.First()));
                return;
            }*/

            // Remoe zero amounts and values
            var zeroAmountIndexes = amounts.Select((a, i) => a == 0 ? i : int.MinValue).Where(i => i >= 0);
            var removeIdexes = zeroAmountIndexes.OrderByDescending(i => i).ToList();
            removeIdexes.ForEach(i => amounts.RemoveAt(i));
            removeIdexes.ForEach(i => values.RemoveAt(i));

            Func<float, int> rounded = v => (int)Math.Round(v, MidpointRounding.AwayFromZero);
            Func<int, int> amountForFrame = v => rounded((float)v / amounts.Min());
            var handled = 0;

            while (handled < objects.Count())
            {
                var frame = amounts.Select(a => amountForFrame(a)).ToList();
                amounts = amounts.Select((a, i) => a - frame[i]).ToList();

                var singleIndexes = frame.Select((a, i) => a == 1 ? i : int.MinValue).Where(i => i >= 0).ToList();
                var subFrame = frame.Where(a => a > 1).ToList();
                var subValues = new List<V>(values);
                singleIndexes.OrderByDescending(i => i).ToList().ForEach(i => subValues.RemoveAt(i));
                var subObjects = objects.GetRange(handled, subFrame.Sum());

                EvenDistributionSet(subObjects, subValues, subFrame, setter);
                handled += subObjects.Count();

                foreach (var index in singleIndexes)
                {
                    setter(objects[handled], values[index]);
                    handled++;
                }
            }
        }
    }
}
