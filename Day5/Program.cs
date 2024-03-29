﻿using System;
using System.IO;
using System.Linq;

namespace Day05
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
                Console.WriteLine($"Provide the path to the diagnostic program .txt file (default: `{DefaultFilePath}`)");
                Environment.Exit(Environment.ExitCode);
            }

            var codeFilePath = args.FirstOrDefault(x => x.EndsWith(".txt")) ?? DefaultFilePath;

            var program = File.ReadAllText(codeFilePath)
                .Split(',')
                .Select(int.Parse)
                .ToList();

            IntcodeTranspiler.Transpile(program);
        }
    }
}
