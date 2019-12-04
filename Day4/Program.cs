using System;

namespace Day4
{
    public class Program
    {
        /// <summary>
        /// --- Day 4: Secure Container ---
        /// https://adventofcode.com/2019/day/4
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var bounds = (108457, 562041);
            Console.WriteLine($"Count:{Cryptography.CalculatePasswordCombinationCount(bounds.Item1, bounds.Item2)}");
        }
    }
}
