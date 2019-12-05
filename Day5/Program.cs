using System;
using System.IO;
using System.Linq;

namespace Day5
{
    public class Program
    {
        private const string DefaultFilePath = "input.txt";

        /// <summary>
        /// --- Day 5: Sunny with a Chance of Asteroids ---
        /// https://adventofcode.com/2019/day/5
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

            var program = File.ReadAllText(codeFilePath)
                .Split(',')
                .Select(int.Parse)
                .ToList();

            var diagnosticCode = IntcodeTranspiler.Transpile(program).First();

            Console.WriteLine($"diagnosticCode:{diagnosticCode}");
        }
    }
}
