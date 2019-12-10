using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Day5;

namespace Day7
{
    public class AmplificationCircuit
    {
        private (int, int) _Results;

        public int ComputeOptimalSignal(List<int> program, int amplifierCount, char[] phaseSettings)
        {
            const int lowerPhaseSettingBound = 0;
            var upperPhaseSettingBound = (int)Math.Pow(phaseSettings.Length, amplifierCount);

            var inputs = Enumerable.Range(lowerPhaseSettingBound, upperPhaseSettingBound)
                .Select(x => IntToStringFast(x, phaseSettings).PadLeft(amplifierCount, '0'))
                .Where(x => x.Distinct().Count() == x.Length).ToList();
            foreach (var input in inputs)
            {
                var outputSignal = 0;

                for (var amplifier = 0; amplifier < amplifierCount; amplifier++)
                {
                    Console.SetIn(new StringReader($"{input[amplifier]}\n{outputSignal}\n"));
                    using var writer = new StringWriter();
                    Console.SetOut(writer);

                    IntcodeTranspiler.Transpile(new List<int>(program));
                    outputSignal = int.Parse(writer.ToString());
                }

                if (outputSignal > _Results.Item2)
                {
                    _Results.Item1 = int.Parse(input);
                    _Results.Item2 = outputSignal;
                }
            }

            return _Results.Item2;
        }

        /// <summary>
        /// https://stackoverflow.com/a/923814
        /// An optimized method using an array as buffer instead of 
        /// string concatenation. This is faster for return values having 
        /// a length > 1.
        /// </summary>
        public static string IntToStringFast(int value, char[] baseChars)
        {
            // 32 is the worst cast buffer size for base 2 and int.MaxValue
            int i = 32;
            char[] buffer = new char[i];
            int targetBase = baseChars.Length;

            do
            {
                buffer[--i] = baseChars[value % targetBase];
                value = value / targetBase;
            }
            while (value > 0);

            char[] result = new char[32 - i];
            Array.Copy(buffer, i, result, 0, 32 - i);

            return new string(result);
        }
    }
}
