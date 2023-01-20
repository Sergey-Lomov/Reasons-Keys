using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.Services.FieldAnalyzer
{
    class FieldFabric
    {
        public Field Field(int outerRadius, int innerRadius)
        {
            var points = PointsForRarius(outerRadius);
            var excludePoints = PointsForRarius(innerRadius - 1);
            points.RemoveWhere(p => excludePoints.Contains(p));

            return new Field(points);
        }

        private HashSet<FieldPoint> PointsForRarius (int radius)
        {
            var points = new HashSet<FieldPoint>();

            for (int radiusIter = 0; radiusIter <= radius; radiusIter++)
                for (int x = -radiusIter; x <= radiusIter; x++)
                    for (int y = -radiusIter; y <= radiusIter; y++)
                        for (int z = -radiusIter; z <= radiusIter; z++)
                            if (x + y + z == 0)
                                points.Add(new FieldPoint(x, y, z));

            return points;
        }
    }
}
