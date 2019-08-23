using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAnalyzer.Services.FieldAnalyzer
{
    class FieldRoute
    {
        public FieldPoint p1, p2;
        public int distance = 0;

        public FieldRoute(FieldPoint p1, FieldPoint p2, int distance)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.distance = distance;
        }
    }
}
