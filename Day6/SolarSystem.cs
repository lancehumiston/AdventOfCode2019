using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    public static class SolarSystem
    {
        /// <summary>
        /// Calculates the number of direct and indirect orbits in the solar system
        /// </summary>
        /// <param name="orbitPatterns"></param>
        /// <returns></returns>
        public static int CalculateOrbitCount(List<string> orbitPatterns)
        {
            var planets = BuildMapOfTheUniverse(orbitPatterns);

            // Traverse from the orbiters that have no orbiters to the center of the universe
            return planets.Values.Sum(orbiter => CountHopsToCenter(planets, orbiter));
        }

        /// <summary>
        /// Recursively traverses the <paramref name="planets"/> until a value is found that does not exist as a key
        /// for another value meaning that it does not orbit anything (i.e. the COM is found)
        /// </summary>
        /// <param name="planets"></param>
        /// <param name="edgePlanet"></param>
        /// <returns>The number of planet hops</returns>
        public static int CountHopsToCenter(Dictionary<string, string> planets, string edgePlanet)
        {
            return GetHopsToCenter(planets, edgePlanet).Count;
        }

        /// <summary>
        /// Recursively traverses the <paramref name="planets"/> until a value is found that does not exist as a key
        /// for another value meaning that it does not orbit anything (i.e. the COM is found)
        /// </summary>
        /// <param name="planets"></param>
        /// <param name="edgePlanet"></param>
        /// <returns>The planets hops</returns>
        public static List<string> GetHopsToCenter(Dictionary<string, string> planets, string edgePlanet)
        {
            if (!planets.ContainsKey(edgePlanet))
            {
                return new List<string> {edgePlanet};
            }

            return new List<string>(GetHopsToCenter(planets, planets[edgePlanet]))
                .Prepend(edgePlanet)
                .ToList();
        }

        /// <summary>
        /// Compute the distance between <paramref name="sourcePlanet"/> and <paramref name="destinationPlanet"/> that
        /// exist in <paramref name="orbitPatterns"/>
        /// </summary>
        /// <param name="orbitPatterns"></param>
        /// <param name="sourcePlanet"></param>
        /// <param name="destinationPlanet"></param>
        /// <returns></returns>
        public static int CalculatePlanetDistance(List<string> orbitPatterns, string sourcePlanet, string destinationPlanet)
        {
            var planets = BuildMapOfTheUniverse(orbitPatterns);

            var hops = new HashSet<string>(GetHopsToCenter(planets, sourcePlanet));
            hops.SymmetricExceptWith(GetHopsToCenter(planets, destinationPlanet));

            // The initial orbit transfer for both planets should be removed from the total count
            return hops.Count - 2;
        }

        /// <summary>
        /// Creates a dictionary of planets {A, B} where A orbits B<paramref name="orbitPatterns"/>
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
