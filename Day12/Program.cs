using System;
using System.IO;
using System.Linq;

namespace Day12
{
    public class Program
    {
        private const string DefaultFilePath = "input.txt";

        /// <summary>
        /// --- Day 12: The N-Body Problem ---
        /// https://adventofcode.com/2019/day/12
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            if (args.Any(x => x.Equals("--help")))
            {
                Console.WriteLine($"Provide the path to the position of moons .txt file (default: `{DefaultFilePath}`)");
                Environment.Exit(Environment.ExitCode);
            }

            var moonCoordinateInput = args.FirstOrDefault(x => x.EndsWith(".txt")) ?? DefaultFilePath;

            var moonCoordinates = File.ReadAllLines(moonCoordinateInput)
                .Select(MoonMap.ParseCoordinate)
                .ToList();

            var newCoordinates = MoonMap.SimulateSteps(moonCoordinates, 1000);

            Console.WriteLine($"Total energy: {MoonMap.CalculateEnergy(newCoordinates)}");
        }
    }
}
