using System;
using System.IO;
using System.Linq;
using Day05;

namespace Day07
{
    public class Program
    {
        private const string DefaultFilePath = "input.txt";

        /// <summary>
        /// --- Day 7: Amplification Circuit ---
        /// https://adventofcode.com/2019/day/7
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            if (args.Any(x => x.Equals("--help")))
            {
                Console.WriteLine($"Provide the path to the amplifier controller software .txt file (default: `{DefaultFilePath}`)");
                Environment.Exit(Environment.ExitCode);
            }

            var codeFilePath = args.FirstOrDefault(x => x.EndsWith(".txt")) ?? DefaultFilePath;

            var program = File.ReadAllText(codeFilePath)
                .Split(',')
                .Select(int.Parse)
                .ToList();

            var result = new AmplificationCircuit().ComputeOptimalSignal(program, 5, new[] { '0', '1', '2', '3', '4' });
            Console.WriteLine(result);
        }
    }
}
