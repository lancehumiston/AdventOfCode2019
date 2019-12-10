using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day10
{
    public class SpaceStation
    {
        public const char Asteroid = '#';

        public static (Point, int, List<Point>) GetBestSpaceStation(List<string> rows)
        {
            var asteroids = FindAsteroidCoordinates(rows);
            (Point, int, List<Point>) results = default;

            foreach (var asteroid in asteroids)
            {
                var asteroidCount = 0;
                var angleOfSights = new List<(bool, bool, double)>();

                var otherAsteroids = asteroids.Where(x => !x.Equals(asteroid)).ToList();
                foreach (var angleOfSight in otherAsteroids.Select(otherAsteroid => CalculateLineOfSight(otherAsteroid, asteroid)))
                {
                    if (!angleOfSights.Contains(angleOfSight))
                    {
                        asteroidCount++;
                    }
                    
                    angleOfSights.Add(angleOfSight);
                }

                if (asteroidCount < results.Item2)
                {
                    continue;
                }

                results.Item1 = asteroid;
                results.Item2 = asteroidCount;
                results.Item3 = otherAsteroids;
            }

            return results;
        }

        private static (bool, bool, double) CalculateLineOfSight(Point otherAsteroid, Point asteroid)
        {
            var hasGreaterLatitude = otherAsteroid.Y - asteroid.Y > 0;
            var hasGreaterLongitude = otherAsteroid.X - asteroid.X > 0;
            var angleToAsteroid = (double) (otherAsteroid.Y - asteroid.Y) / (otherAsteroid.X - asteroid.X);

            return (hasGreaterLatitude, hasGreaterLongitude, angleToAsteroid);
        }

        private static List<Point> FindAsteroidCoordinates(List<string> rows)
        {
            var asteroids = new List<Point>();

            for (var rowIdx = 0; rowIdx < rows.Count; rowIdx++)
            {
                var columns = rows[rowIdx];
                for (var columnIdx = 0; columnIdx < columns.Length; columnIdx++)
                {
                    if (rows[rowIdx][columnIdx] == Asteroid)
                    {
                        asteroids.Add(new Point(columnIdx, rowIdx));
                    }
                }
            }

            return asteroids;
        }
    }
}
