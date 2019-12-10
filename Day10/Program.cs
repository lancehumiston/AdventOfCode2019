using System;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        private const string DefaultFilePath = "input.txt";

        /// <summary>
        /// --- Day 10: Monitoring Station ---
        /// https://adventofcode.com/2019/day/10
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            if (args.Any(x => x.Equals("--help")))
            {
                Console.WriteLine($"Provide the path to the asteroid map input .txt file (default: `{DefaultFilePath}`)");
                Environment.Exit(Environment.ExitCode);
            }

            var mapFilePath = args.FirstOrDefault(x => x.EndsWith(".txt")) ?? DefaultFilePath;

            var asteroidData = File.ReadAllLines(mapFilePath).ToList();

            var spaceStationLocationResults = SpaceStation.GetBestSpaceStation(asteroidData);

            Console.WriteLine(spaceStationLocationResults);
        }
    }
}
