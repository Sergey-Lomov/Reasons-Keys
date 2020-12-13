using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.Services.FieldAnalyzer
{
    class FieldPoint
    {
        private const int hashMult = 13;
        private const int hashInit = 17;

        public int x, y, z;
        public int timestamp;

        public static FieldPoint center = new FieldPoint(0, 0, 0);

        public FieldPoint(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            timestamp = timestampFor(x, y, z);
        }

        public FieldPoint(FieldPoint source, FieldDirection direction)
        {
            x = source.x + direction.x;
            y = source.y + direction.y;
            z = source.z + direction.z;
            timestamp = timestampFor(x, y, z);
        }

        private static int timestampFor(int x, int y, int z)
        {
            var radius = FieldPoint.radius(x, y, z);
            if (radius == 0)
                return 0;

            var preRadiuses = Enumerable.Range(1, radius - 1);
            var preRadiusTime = preRadiuses.Select(i => i * Field.nearesNodesAmount).Sum();
            return preRadiusTime + inRadiusTime(x, y, z, radius) + 1;
        }

        private static int inRadiusTime(int x, int y, int z, int radius)
        {
            var inRadiusTime = 0;
            var cx = 0;
            var cy = radius;
            var cz = -radius;

            if (cx == x && cy == y && cz == z)
                return 0;

            while (cy > 0)
            {
                cx += 1; cy -= 1;
                inRadiusTime++;
                if (cx == x && cy == y && cz == z)
                    return inRadiusTime;
            }

            while (cz < 0)
            {
                cy -= 1; cz += 1;
                inRadiusTime++;
                if (cx == x && cy == y && cz == z)
                    return inRadiusTime;
            }

            while (cx > 0)
            {
                cx -= 1; cz += 1;
                inRadiusTime++;
                if (cx == x && cy == y && cz == z)
                    return inRadiusTime;
            }

            while (cy < 0)
            {
                cx -= 1; cy += 1;
                inRadiusTime++;
                if (cx == x && cy == y && cz == z)
                    return inRadiusTime;
            }

            while (cz > 0)
            {
                cy += 1; cz -= 1;
                inRadiusTime++;
                if (cx == x && cy == y && cz == z)
                    return inRadiusTime;
            }

            while (cx < 0)
            {
                cx += 1; cz -= 1;
                inRadiusTime++;
                if (cx == x && cy == y && cz == z)
                    return inRadiusTime;
            }

            return inRadiusTime;
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
            return radius(x, y, z);
        }

        private static int radius(int x, int y, int z)
        {
            return new List<int> { x, y, z }.Select(v => Math.Abs(v)).Max();
        }
    }
}
