using System;
using System.IO;
using System.Linq;

namespace Day3
{
    public class Program
    {
        private const string DefaultFilePath = "input.txt";

        /// <summary>
        /// --- Day 3: Crossed Wires ---
        /// https://adventofcode.com/2019/day/3
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            if (args.Any(x => x.Equals("--help")))
            {
                Console.WriteLine($"Provide the path of the modules mass values file (default: `{DefaultFilePath}`)");
                Environment.Exit(Environment.ExitCode);
            }

            var codeFilePath = args.FirstOrDefault(x => x.EndsWith(".txt")) ?? DefaultFilePath;

            var wireVectorPaths = File.ReadAllLines(codeFilePath);

            var wireCoordinatePaths = wireVectorPaths
                .Select(wirePath => Grid.PlotPoints(wirePath.Split(',').ToList()))
                .ToList();

            var intersectionPoints = Grid.FindIntersectionPoints(wireCoordinatePaths);

            var closestIntersectionDistanceToOrigin = Grid.FindClosestManhattanDistance(intersectionPoints);

            Console.WriteLine($"closest intersection: {closestIntersectionDistanceToOrigin}");

            var lowestStepCountToIntersection = Grid.FindLowestStepCountToIntersection(wireCoordinatePaths, intersectionPoints);

            Console.WriteLine($"lowest step count to intersection: {lowestStepCountToIntersection}");
        }
    }
}
