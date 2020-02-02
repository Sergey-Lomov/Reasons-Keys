using System;

namespace ModelAnalyzer.Services.FieldAnalyzer
{
    class FieldRoute : IEquatable<FieldRoute>
    {
        public FieldPoint p1, p2;
        public int distance = 0;

        public FieldRoute(FieldPoint p1, FieldPoint p2, int distance)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.distance = distance;
        }

        public bool Equals(FieldRoute other)
        {
            return p1 == other.p1
                && p2 == other.p2
                && distance == other.distance;
        }
    }
}
