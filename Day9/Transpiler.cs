using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    public static class DoubleListExtensions
    {
        public static double SafeGetElementAt(this List<double> list, int index)
        {
            if (index >= list.Count)
            {
                list.AddRange(Enumerable.Range(0, list.Count - index + 1).Select(Convert.ToDouble));
            }

            return list.ElementAt(index);
        }
    }

    public static class IntcodeTranspiler
    {
        private const int BaseCase = 99;
        private const int FirstParameterIndexOffset = 1;
        private const int SecondParameterIndexOffset = 2;
        private const int ResultIndexOffset = 3;
        private static int _RelativeBase = 0;

        /// <summary>
        /// An doublecode program is a list of doubleegers separated by commas (like 1,0,0,3,99). To run one, start by
        /// looking at the first doubleeger (called position 0). Here, you will find an opcode - either 1, 2, or 99.
        /// The opcode indicates what to do; for example, 99 means that the program is finished and should immediately
        /// halt. Encountering an unknown opcode means something went wrong.
        /// 
        /// Opcode 1 adds together numbers read from two positions and stores the result in a third position. The three
        /// doubleegers immediately after the opcode tell you these three positions - the first two indicate the positions
        /// from which you should read the input values, and the third indicates the position at which the output should
        /// be stored.
        /// 
        /// Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them. Again,
        /// the three doubleegers after the opcode indicate where the inputs and outputs are, not their values.
        ///
        /// Opcode 3 takes a single doubleeger as input and saves it to the address given by its only parameter. For
        /// example, the instruction 3,50 would take an input value and store it at address 50.
        ///
        /// Opcode 4 outputs the value of its only parameter. For example, the instruction 4,50 would output the value
        /// at address 50.
        ///
        /// Opcode 5 is jump-if-true: if the first parameter is non-zero, it sets the instruction podoubleer to the value
        /// from the second parameter. Otherwise, it does nothing.
        ///
        /// Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction podoubleer to the value
        /// from the second parameter. Otherwise, it does nothing.
        ///
        /// Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the
        /// position given by the third parameter. Otherwise, it stores 0.
        ///
        /// Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position
        /// given by the third parameter. Otherwise, it stores 0.
        ///
        /// Opcode 9 adjusts the relative base by the value of its only parameter. The relative base increases (or
        /// decreases, if the value is negative) by the value of the parameter.
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static List<double> Transpile(List<double> sequence)
        {
            for (var offset = 0; (int)sequence[offset] != BaseCase; offset += (int)ProcessInstruction(sequence, offset))
            {
            }

            return sequence;
        }

        private static double ProcessInstruction(List<double> sequence, int offset)
        {
            var instruction = $"{sequence[offset]}".PadLeft(4, '0');
            var opcode = (Opcode)double.Parse(instruction[new Range(2, 4)]);
            var firstParameterMode = (ParameterMode)double.Parse(instruction[new Range(1, 2)]);
            var secondParameterMode = (ParameterMode)double.Parse(instruction[new Range(0, 1)]);

            switch (opcode)
            {
                case Opcode.Add:
                    sequence[(int)sequence[offset + ResultIndexOffset]] = checked(GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset) + GetParameterValue(secondParameterMode, sequence, offset + SecondParameterIndexOffset));

                    return 4;
                case Opcode.Multiply:
                    sequence[(int)sequence[offset + ResultIndexOffset]] = checked(GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset) * GetParameterValue(secondParameterMode, sequence, offset + SecondParameterIndexOffset));

                    return 4;
                case Opcode.Input:
                    var input = Console.ReadLine() ?? throw new ArgumentNullException();
                    sequence[(int)sequence[offset + FirstParameterIndexOffset]] = double.Parse(input);

                    return 2;
                case Opcode.Output:
                    var output = GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset);
                    Console.WriteLine(output);

                    return 2;
                case Opcode.JumpIfNotZero:
                    if ((int)GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset) != default)
                    {
                        return GetParameterValue(secondParameterMode, sequence, offset + SecondParameterIndexOffset) - offset;
                    }

                    return 3;
                case Opcode.JumpIfZero:
                    if ((int)GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset) == default)
                    {
                        return GetParameterValue(secondParameterMode, sequence, offset + SecondParameterIndexOffset) - offset;
                    }

                    return 3;
                case Opcode.LessThan:
                    sequence[(int)sequence[offset + ResultIndexOffset]] =
                        GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset)
                        < GetParameterValue(secondParameterMode, sequence, offset + SecondParameterIndexOffset)
                        ? 1 : 0;

                    return 4;
                case Opcode.Equals:
                    sequence[(int)sequence[offset + ResultIndexOffset]] =
                        (int)GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset)
                        == (int)GetParameterValue(secondParameterMode, sequence, offset + SecondParameterIndexOffset)
                        ? 1 : 0;

                    return 4;
                case Opcode.RelativeBase:
                    _RelativeBase += (int) GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset);

                    return 4;
                default:
                    throw new ArgumentException($"Unknown opcode: {sequence[offset]}");
            }
        }

        private static double GetParameterValue(ParameterMode mode, List<double> sequence, int offset)
        {
            return mode switch
            {
                ParameterMode.Position => sequence[(int)sequence[offset]],
                ParameterMode.Immediate => sequence[offset],
                ParameterMode.Relative => sequence[offset + _RelativeBase],
                _ => throw new ArgumentException($"Unknown mode: {mode}")
            };
        }

        public enum ParameterMode
        {
            Position,
            Immediate,
            Relative
        }

        public enum Opcode
        {
            Add = 1,
            Multiply = 2,
            Input = 3,
            Output = 4,
            JumpIfNotZero = 5,
            JumpIfZero = 6,
            LessThan = 7,
            Equals = 8,
            RelativeBase = 9
        }
    }
}
