using System.Collections.Generic;
using Day2;
using FluentAssertions;
using Xunit;

namespace Tests.Day2
{
    public class IntcodeTranspilerTests
    {
        /// - 1,0,0,0,99 becomes 2,0,0,0,99 (1 + 1 = 2).
        /// - 2,3,0,3,99 becomes 2,3,0,6,99 (3 * 2 = 6).
        /// - 2,4,4,5,99,0 becomes 2,4,4,5,99,9801 (99 * 99 = 9801).
        /// - 1,1,1,4,99,5,6,0,99 becomes 30,1,1,4,2,5,6,0,99.
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
                    new List<int> { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 },
                    new List<int> { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 }
                },
                new object[]
                {
                    new List<int> { 1, 0, 0, 0, 99 },
                    new List<int> { 2, 0, 0, 0, 99 }
                },
                new object[]
                {
                    new List<int> { 2, 3, 0, 3, 99 },
                    new List<int> { 2, 3, 0, 6, 99 }
                },
                new object[]
                {
                    new List<int> { 2, 4, 4, 5, 99, 0 },
                    new List<int> { 2, 4, 4, 5, 99, 9801 }
                },
                new object[]
                {
                    new List<int> { 1, 1, 1, 4, 99, 5, 6, 0, 99 },
                    new List<int> { 30, 1, 1, 4, 2, 5, 6, 0, 99 }
                }
            };
    }
}
