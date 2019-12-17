using System;
using System.Linq.Expressions;
using Day04;
using FluentAssertions;
using Xunit;

namespace Tests.Day04
{
    public class CryptographyTests
    {
        /// Part One
        /// - 223450 does not meet these criteria (decreasing pair of digits 50).
        [Theory]
        [InlineData(223450, true)]
        [InlineData(123, false)]
        [InlineData(111, false)]
        [InlineData(120, true)]
        public void DoesDecrease_NumericInput_ReturnsExpectedFlag(int value, bool expected)
        {
            var result = Cryptography.DoesDecrease(value);

            result.Should().Be(expected);
        }

        [Fact]
        public void CalculatePasswordCombinationCount_NumericInput_ReturnsExpectedCount()
        {
            const int min = 0;
            const int max = 100;
            const int expected = 9;

            var result = Cryptography.CalculatePasswordCombinationCount(min, max);

            result.Should().Be(expected);
        }

        /// Part One (2+ consecutive)
        /// - 111111 meets these criteria (double 11, never decreases).
        /// - 123789 does not meet these criteria (no double).
        ///
        /// Part Two  (2 consecutive)
        /// - 112233 meets these criteria because the digits never decrease and all repeated digits are exactly two digits long.
        /// - 123444 no longer meets the criteria (the repeated 44 is part of a larger group of 444).
        /// - 111122 meets the criteria (even though 1 is repeated more than twice, it still contains a double 22).
        [Theory]
        [MemberData(nameof(DoesContainConsecutiveCharPatternTestData))]
        public void DoesContainConsecutiveCharPattern_NumericInput_ReturnsExpectedFlag(int value, Expression<Func<int, bool>> meetsLengthRequirement, bool expected)
        {
            var meetsLengthRequirementFunc = meetsLengthRequirement.Compile();

            var result = Cryptography.DoesContainConsecutiveCharPattern(value, meetsLengthRequirementFunc);

            result.Should().Be(expected);
        }

        public static TheoryData<int, Expression<Func<int, bool>>, bool> DoesContainConsecutiveCharPatternTestData =>
            new TheoryData<int, Expression<Func<int, bool>>, bool>
            {
                {
                    111111,
                    x => x >= 2,
                    true
                },
                {
                    123789,
                    x => x >= 2,
                    false
                },
                {
                    123444,
                    x => x >= 2,
                    true
                },
                {
                    112233,
                    x => x == 2,
                    true
                },
                {
                    123444,
                    x => x == 2,
                    false
                },
                {
                    111122,
                    x => x == 2,
                    true
                }
            };
    }
}
