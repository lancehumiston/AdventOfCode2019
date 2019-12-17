using System;
using System.Collections.Generic;

namespace Day05
{
    public static class IntcodeTranspiler
    {
        private const int BaseCase = 99;
        private const int FirstParameterIndexOffset = 1;
        private const int SecondParameterIndexOffset = 2;
        private const int ResultIndexOffset = 3;

        /// <summary>
        /// An Intcode program is a list of integers separated by commas (like 1,0,0,3,99). To run one, start by
        /// looking at the first integer (called position 0). Here, you will find an opcode - either 1, 2, or 99.
        /// The opcode indicates what to do; for example, 99 means that the program is finished and should immediately
        /// halt. Encountering an unknown opcode means something went wrong.
        /// 
        /// Opcode 1 adds together numbers read from two positions and stores the result in a third position. The three
        /// integers immediately after the opcode tell you these three positions - the first two indicate the positions
        /// from which you should read the input values, and the third indicates the position at which the output should
        /// be stored.
        /// 
        /// Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them. Again,
        /// the three integers after the opcode indicate where the inputs and outputs are, not their values.
        ///
        /// Opcode 3 takes a single integer as input and saves it to the address given by its only parameter. For
        /// example, the instruction 3,50 would take an input value and store it at address 50.
        ///
        /// Opcode 4 outputs the value of its only parameter. For example, the instruction 4,50 would output the value
        /// at address 50.
        ///
        /// Opcode 5 is jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value
        /// from the second parameter. Otherwise, it does nothing.
        ///
        /// Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value
        /// from the second parameter. Otherwise, it does nothing.
        ///
        /// Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the
        /// position given by the third parameter. Otherwise, it stores 0.
        ///
        /// Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position
        /// given by the third parameter. Otherwise, it stores 0.
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static List<int> Transpile(List<int> sequence)
        {
            for (var offset = 0; sequence[offset] != BaseCase; offset += ProcessInstruction(sequence, offset))
            {
            }

            return sequence;
        }

        private static int ProcessInstruction(List<int> sequence, int offset)
        {
            var instruction = $"{sequence[offset]}".PadLeft(4, '0');
            var opcode = (Opcode) int.Parse(instruction[new Range(2, 4)]);
            var firstParameterMode = (ParameterMode) int.Parse(instruction[new Range(1, 2)]);
            var secondParameterMode = (ParameterMode) int.Parse(instruction[new Range(0, 1)]);

            switch (opcode)
            {
                case Opcode.Add:
                    sequence[sequence[offset + ResultIndexOffset]] = checked(GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset) + GetParameterValue(secondParameterMode, sequence, offset + SecondParameterIndexOffset));

                    return 4;
                case Opcode.Multiply:
                    sequence[sequence[offset + ResultIndexOffset]] = checked(GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset) * GetParameterValue(secondParameterMode, sequence, offset + SecondParameterIndexOffset));

                    return 4;
                case Opcode.Input:
                    sequence[sequence[offset + FirstParameterIndexOffset]] = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException());

                    return 2;
                case Opcode.Output:
                    Console.WriteLine($"diagnosticCode:{GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset)}");

                    return 2;
                case Opcode.JumpIfNotZero:
                    if (GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset) != default)
                    {
                        return GetParameterValue(secondParameterMode, sequence, offset + SecondParameterIndexOffset) - offset;
                    }

                    return 3;
                case Opcode.JumpIfZero:
                    if (GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset) == default)
                    {
                        return GetParameterValue(secondParameterMode, sequence, offset + SecondParameterIndexOffset) - offset;
                    }

                    return 3;
                case Opcode.LessThan:
                    sequence[sequence[offset + ResultIndexOffset]] =
                        GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset)
                        < GetParameterValue(secondParameterMode, sequence, offset + SecondParameterIndexOffset)
                        ? 1 : 0;

                    return 4;
                case Opcode.Equals:
                    sequence[sequence[offset + ResultIndexOffset]] =
                        GetParameterValue(firstParameterMode, sequence, offset + FirstParameterIndexOffset)
                        == GetParameterValue(secondParameterMode, sequence, offset + SecondParameterIndexOffset)
                        ? 1 : 0;

                    return 4;
                default:
                    throw new ArgumentException($"Unknown opcode: {sequence[offset]}");
            }
        }

        private static int GetParameterValue(ParameterMode mode, List<int> sequence, int offset)
        {
            return mode switch
            {
                ParameterMode.Position => sequence[sequence[offset]],
                ParameterMode.Immediate => sequence[offset],
                _ => throw new ArgumentException($"Unknown mode: {mode}")
            };
        }

        public enum ParameterMode
        {
            Position,
            Immediate
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
            Equals = 8
        }
    }
}
