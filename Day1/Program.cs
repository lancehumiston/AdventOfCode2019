using System;
using System.IO;
using System.Linq;

namespace Day01
{
    /// <summary>
    /// --- Day 1: The Tyranny of the Rocket Equation ---
    /// https://adventofcode.com/2019/day/1
    /// </summary>
    public class Program
    {
        private const string DefaultFilePath = "modules.txt";

        public static void Main(string[] args)
        {
            if (args.Any(x => x.Equals("--help")))
            {
                Console.WriteLine($"Provide the path of the modules mass values file (default: `{DefaultFilePath}`)");
                Environment.Exit(Environment.ExitCode);
            }

            var modulesFilePath = args.FirstOrDefault(x => x.EndsWith(".txt")) ?? DefaultFilePath;

            var moduleMasses = File.ReadLines(modulesFilePath);

            var result = moduleMasses
                .Select(x => FuelCalculator.CalculateForModule(int.Parse(x)))
                .Sum();

            Console.WriteLine(result);
        }
    }
}
