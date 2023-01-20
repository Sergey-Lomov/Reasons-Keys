using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.Services.FieldAnalyzer
{
    class Field
    {
        public const int nearesNodesAmount = 6;

        public HashSet<FieldPoint> points = new HashSet<FieldPoint>();

        public Field(HashSet<FieldPoint>  points)
        {
            this.points = points;
        }

        public List<FieldPoint> AvailableNearestFor(FieldPoint point)
        {
            var nearest = point.Nearest();
            return nearest.Where(p => points.Contains(p)).ToList();
        }

        public HashSet<FieldRoute> AllRoutes()
        {
            var routes = new HashSet<FieldRoute>();
            var globalHanledPoitns = new HashSet<FieldPoint>();

            foreach (var point in points)
            {
                var localRoutes = new HashSet<FieldRoute>();
                var localHandledPoints = new HashSet<FieldPoint>();
                var initialRoute = new FieldRoute(point, point, 0);

                localHandledPoints.Add(point);
                localRoutes.Add(initialRoute);

                var currentDistance = 0;
                while (localHandledPoints.Count < points.Count)
                {
                    if (point.x == -2 && point.y == 0 && point.z == 2)
                        Console.Write("Issue");

                    var longRoutes = localRoutes.Where(r => r.distance == currentDistance);
                    var edgePoints = longRoutes.Select(r => r.p2).ToList();

                    foreach (var edgePoint in edgePoints)
                    {
                        var nearest = edgePoint.Nearest();
                        foreach (var nearePoint in nearest)
                        {
                            var isNotHandled = !localHandledPoints.Contains(nearePoint);
                            var isAvailable = points.Contains(nearePoint);
                            if (isAvailable && isNotHandled)
                            {
                                localHandledPoints.Add(nearePoint);
                                var route = new FieldRoute(point, nearePoint, currentDistance + 1);
                                localRoutes.Add(route);
                            }
                        }
                    }

                    currentDistance++;
                }

                localRoutes.Remove(initialRoute);
                globalHanledPoitns.Add(point);
                routes.UnionWith(localRoutes);
            }

            return routes;
        }
    }
}
