using System;
using System.Linq;

namespace Day04
{
    public static class Cryptography
    {
        public static int CalculatePasswordCombinationCount(int minValue, int maxValue)
        {
            var combinationCount = 0;

            for (var i = minValue + 1; i < maxValue; i++)
            {
                if (DoesContainConsecutiveCharPattern(i, x => x.Equals(2)) && !DoesDecrease(i))
                {
                    combinationCount++;
                }
            }

            return combinationCount;
        }

        public static bool DoesDecrease(int value)
        {
            var valueAsChars = $"{value}".ToCharArray();

            for (var i = 0; i < valueAsChars.Length - 1; i++)
            {
                if (valueAsChars[i] > valueAsChars[i + 1])
                {
                    return true;
                }
            }

            return false;
        }

        public static bool DoesContainConsecutiveCharPattern(int value, Func<int, bool> meetsLengthRequirementFunc)
        {
            return $"{value}"
                .ToList()
                .GroupBy(k => k, v => v, (_, character) => character.Count())
                .Any(meetsLengthRequirementFunc);
        }
    }
}
