using System;
using System.IO;
using System.Linq;

namespace Day06
{
    public class Program
    {
        private const string DefaultFilePath = "input.txt";

        /// <summary>
        /// --- Day 6: Universal Orbit Map ---
        /// https://adventofcode.com/2019/day/6
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            if (args.Any(x => x.Equals("--help")))
            {
                Console.WriteLine($"Provide the path of the map of local orbits .txt file (default: `{DefaultFilePath}`)");
                Environment.Exit(Environment.ExitCode);
            }

            var codeFilePath = args.FirstOrDefault(x => x.EndsWith(".txt")) ?? DefaultFilePath;

            var orbitPatterns = File.ReadAllLines(codeFilePath).ToList();

            Console.WriteLine($"Orbit count: {SolarSystem.CalculateOrbitCount(orbitPatterns)}");

            Console.WriteLine($"Minimum orbit transfers: {SolarSystem.CalculatePlanetDistance(orbitPatterns, "YOU", "SAN")}");
        }
    }
}
