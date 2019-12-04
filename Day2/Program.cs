using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    public class Program
    {
        private const string DefaultFilePath = "input.txt";
        private const int TargetValue = 19690720;
        private static readonly Tuple<int,int> _InclusiveBounds = new Tuple<int, int>(0, 99);

        /// <summary>
        /// --- Day 2: 1202 Program Alarm ---
        /// https://adventofcode.com/2019/day/2
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

            // TODO: revisit brute force approach
            for (var i = _InclusiveBounds.Item1; i <= _InclusiveBounds.Item2; i++)
            {
                for (var j = _InclusiveBounds.Item1; j <= _InclusiveBounds.Item2; j++)
                {
                    var programCopy = program.ToList();

                    FixBug(programCopy, i, j);

                    try
                    {
                        var result = IntcodeTranspiler.Transpile(programCopy).First();

                        if (result.Equals(TargetValue))
                        {
                            Console.WriteLine($"noun:{i}");
                            Console.WriteLine($"verb:{j}");
                            Console.WriteLine($"{100 * i + j}=100*noun+verb");

                            Environment.Exit(Environment.ExitCode);
                        }
                    }
                    catch (ArgumentOutOfRangeException) { }
                    catch (OverflowException) { }
                }
            }
        }

        private static void FixBug(List<int> sequence, int noun, int verb)
        {
            sequence[1] = noun;
            sequence[2] = verb;
        }
    }
}
