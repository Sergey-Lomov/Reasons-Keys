using System;
using System.Collections.Generic;

namespace ModelAnalyzer.Services
{
    // This class implements stack for handling timings. Only last added timing calculated at each moment
    class TimingsStack
    {
        private readonly Stack<Timing> stack = new Stack<Timing>();

        private class Timing
        {
            private readonly DateTime start = DateTime.Now;
            private double idle = 0;

            public void AppendIdle(double idle)
            {
                this.idle += idle;
            }

            public double DurationTillNow()
            {
                return (DateTime.Now - start).TotalMilliseconds - idle;
            }
        }

        public void StartNewTiming()
        {
            stack.Push(new Timing());
        }

        // Returns finished timing duration
        public double FinishCurrentTiming()
        {
            var lastTiming = stack.Pop();
            var duration = lastTiming.DurationTillNow();

            foreach (var timing in stack)
            {
                timing.AppendIdle(duration);
            }

            return duration;
        }
    }
}
