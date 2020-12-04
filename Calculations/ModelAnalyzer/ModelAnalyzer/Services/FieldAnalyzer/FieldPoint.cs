using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAnalyzer.Services.FieldAnalyzer
{
    class FieldPoint
    {
        private const int hashMult = 13;
        private const int hashInit = 17;

        public int x, y, z;

        public FieldPoint(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public HashSet<FieldPoint> nearest()
        {
            var set = new HashSet<FieldPoint>();
            set.Add(new FieldPoint(x + 1, y - 1, z));
            set.Add(new FieldPoint(x + 1, y, z - 1));
            set.Add(new FieldPoint(x - 1, y + 1, z));
            set.Add(new FieldPoint(x - 1, y, z + 1));
            set.Add(new FieldPoint(x, y - 1, z + 1));
            set.Add(new FieldPoint(x, y + 1, z - 1));
            return set;
        }

        public static bool operator ==(FieldPoint p1, FieldPoint p2)
        {
            return p1.x == p2.x && p1.y == p2.y && p1.z == p2.z;
        }

        public static bool operator !=(FieldPoint p1, FieldPoint p2)
        {
            return !(p1 == p2);
        }

        public override bool Equals(object obj)
        {
            if (obj is FieldPoint)
                return this == (obj as FieldPoint);

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = hashInit;
                hash = hash * hashMult + x.GetHashCode();
                hash = hash * hashMult + y.GetHashCode();
                hash = hash * hashMult + z.GetHashCode();
                return hash;
            }
        }

        public int radius()
        {
            return new List<int> { x, y, z }.Select(v => Math.Abs(v)).Max();
        }
    }
}
