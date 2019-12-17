using System;
using System.Collections.Generic;

namespace Day02
{
    public static class IntcodeTranspiler
    {
        private const int BaseCase = 99;
        private const int FirstParameterIndexOffset = 1;
        private const int SecondParameterIndexOffset = 2;
        private const int ResultIndexOffset = 3;
        private const int AddOpcode = 1;

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
        /// For example, if your Intcode computer encounters 1,10,20,30, it should read the values at positions 10 and
        /// 20, add those values, and then overwrite the value at position 30 with their sum.
        /// 
        /// Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them. Again,
        /// the three integers after the opcode indicate where the inputs and outputs are, not their values.
        /// 
        /// Once you're done processing an opcode, move to the next one by stepping forward 4 positions.
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static List<int> Transpile(List<int> sequence)
        {
            for (var offset = 0; sequence[offset] != BaseCase; offset += 4)
            {
                ProcessInstruction(sequence, offset);
            }

            return sequence;
        }

        private static void ProcessInstruction(List<int> sequence, int offset)
        {
            var operation = sequence[offset].Equals(AddOpcode) ? _Add : _Multiply;

            var firstParameterIndex = sequence[offset + FirstParameterIndexOffset];
            var secondParameterIndex = sequence[offset + SecondParameterIndexOffset];
            var resultIndex = sequence[offset + ResultIndexOffset];

            sequence[resultIndex] = operation(sequence[firstParameterIndex], sequence[secondParameterIndex]);
        }

        private static readonly Func<int, int, int> _Add = (x, y) => checked(x + y);

        private static readonly Func<int, int, int> _Multiply = (x, y) => checked(x * y);
    }
}
