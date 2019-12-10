using System.Collections.Generic;
using Day5;
using FluentAssertions;
using Xunit;

namespace Tests.Day5
{
    public class IntcodeTranspilerTests
    {
        /// For example, consider the program 1002,4,3,4,33.
        /// 
        /// The first instruction, 1002,4,3,4, is a multiply instruction - the rightmost two digits of the first value,
        /// 02, indicate opcode 2, multiplication. Then, going right to left, the parameter modes are 0 (hundreds digit),
        /// 1 (thousands digit), and 0 (ten-thousands digit, not present and therefore zero):
        /// 
        /// ABCDE
        ///  1002
        /// 
        /// DE - two-digit opcode,      02 == opcode 2
        ///  C - mode of 1st parameter,  0 == position mode
        ///  B - mode of 2nd parameter,  1 == immediate mode
        ///  A - mode of 3rd parameter,  0 == position mode,
        ///                                   omitted due to being a leading zero
        /// 
        /// This instruction multiplies its first two parameters. The first parameter, 4 in position mode, works like
        /// it did before - its value is the value stored at address 4 (33). The second parameter, 3 in immediate mode,
        /// simply has value 3. The result of this operation, 33 * 3 = 99, is written according to the third parameter,
        /// 4 in position mode, which also works like it did before - 99 is written to address 4.
        [Theory]
        [MemberData(nameof(TranspileTestData))]
        public void Transpile(List<int> sequence, List<int> expectedResult)
        {
            var result = IntcodeTranspiler.Transpile(sequence);

            result.Should().BeEquivalentTo(expectedResult);
        }

        public static IEnumerable<object[]> TranspileTestData =>
            new List<object[]>
            {
                new object[]
                {
                    new List<int> { 1002,4,3,4,33 },
                    new List<int> { 1002,4,3,4,99 }
                }
            };
    }
}
