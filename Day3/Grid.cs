using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day3
{
    public static class Grid
    {
        public static List<Point> PlotPoints(List<string> vectors)
        {
            var startLocation = new Point(0, 0);
            var points = new List<Point> {startLocation};

            // Draw a new line of points for each vector and appending to the existing points
            foreach (var line in vectors.Select(vector => Draw(points.Last(), vector)))
            {
                points.AddRange(line);
            }

            return points;
        }

        private static List<Point> Draw(Point startLocation, string vector)
        {
            // The first character is a direction
            var direction = vector.First();
            // The rest of the characters indicate a numeric magnitude
            var magnitude = int.Parse(vector.Remove(0,1));
            var points = new List<Point>{startLocation};

            var moveFunc = direction switch
            {
                'U' => _MoveUp,
                'D' => _MoveDown,
                'L' => _MoveLeft,
                'R' => _MoveRight,
                _ => throw new ArgumentException($"Invalid direction: {direction}")
            };

            for (var i = 0; i < magnitude; i++)
            {
                points.Add(moveFunc(points.Last()));
            }

            // The startLocation already exists in the path, exclude it from the return list of points
            points.RemoveAt(0);
            return points;
        }

        private static readonly Func<Point, Point> _MoveUp = startLocation => new Point(startLocation.X, ++startLocation.Y);

        private static readonly Func<Point, Point> _MoveDown = startLocation => new Point(startLocation.X, --startLocation.Y);

        private static readonly Func<Point, Point> _MoveLeft = startLocation => new Point(--startLocation.X, startLocation.Y);

        private static readonly Func<Point, Point> _MoveRight = startLocation => new Point(++startLocation.X, startLocation.Y);

        /// <summary>
        /// https://stackoverflow.com/a/1676684
        /// Finds all the unique intersection points of multiple lists of points
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static List<Point> FindIntersectionPoints(List<List<Point>> paths)
        {
            return paths
                .Skip(1)
                .Aggregate(new HashSet<Point>(paths.First()), (hashSet, list) =>
                {
                    hashSet.IntersectWith(list);
                    return hashSet;
                }).ToList();
        }

        /// <summary>
        /// Finds the smallest number of collective steps that wires took before intersecting
        /// </summary>
        /// <param name="paths"></param>
        /// <param name="intersectionPoints"></param>
        /// <returns></returns>
        public static int FindLowestStepCountToIntersection(List<List<Point>> paths, List<Point> intersectionPoints)
        {
            return intersectionPoints
                .Select(intersectionPoint => paths.Select(x => x.IndexOf(intersectionPoint)).Sum())
                .OrderBy(x => x)
                .Skip(1) // Skip the origin point
                .First();
        }

        /// <summary>
        /// Finds the smallest calculated distance from (0,0) using https://en.wikipedia.org/wiki/Taxicab_geometry and
        /// ignoring any (0,0) points.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static int FindClosestManhattanDistance(List<Point> points)
        {
            return points
                .Select(point => Math.Abs(point.X) + Math.Abs(point.Y))
                .OrderBy(x => x)
                .Skip(1) // Skip the origin point
                .First();
        }
    }
}
