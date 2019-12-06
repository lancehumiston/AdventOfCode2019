using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    public static class SolarSystem
    {
        public static int CalculateOrbitCount(List<string> orbitPatterns)
        {
            var planets = BuildMapOfTheUniverse(orbitPatterns);

            // Traverse from the orbiters that have no orbiters to the center of the universe
            return planets.Values.Sum(orbiter => CountHopsToCenter(planets, orbiter));
        }

        public static int CountHopsToCenter(Dictionary<string, string> planets, string edgePlanet)
        {
            if (!planets.ContainsKey(edgePlanet))
            {
                return 1;
            }

            return CountHopsToCenter(planets, planets[edgePlanet]) + 1;
        }

        /// <summary>
        /// Creates a dictionary of {A, B} where A orbits B<paramref name="orbitPatterns"/>
        /// </summary>
        /// <param name="orbitPatterns">Collection of orbit patterns in the form of "A)B" where B orbits A</param>
        /// <returns></returns>
        public static Dictionary<string, string> BuildMapOfTheUniverse(List<string> orbitPatterns)
        {
            var edgeOrbits = new Dictionary<string, string>();

            foreach (var planets in orbitPatterns.Select(orbitPattern => orbitPattern.Split(')')))
            {
                edgeOrbits[planets[1]] = planets[0];
            }

            return edgeOrbits;
        }
    }
}
