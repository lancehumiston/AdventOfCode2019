using System.Collections.Generic;
using System.Linq;
using Day6;
using FluentAssertions;
using Xunit;

namespace Tests.Day6
{ 
    public class PlanetMapTests
    {
        [Fact]
        public void CalculateOrbitCount_ListOfOrbitPatterns_ReturnsOrbitCount()
        {
            // Arrange
            const string input = "COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L";
            const int answerToTheUltimateQuestionOfLifeTheUniverseAndEverything = 42;
            var orbitPatterns = input.Split(',').ToList();

            // Act
            var result = SolarSystem.CalculateOrbitCount(orbitPatterns);

            // Assert
            result.Should().Be(answerToTheUltimateQuestionOfLifeTheUniverseAndEverything);
        }

        [Fact]
        public void CountHopsToCenter_MapOfUniverse_ReturnsOrbitCount()
        {
            // Arrange
            const int expectedHopCount = 8;
            var mapOfUniverse = new Dictionary<string, string>
            {
                {"B", "COM"},
                {"C", "B"},
                {"D", "C"},
                {"E", "D"},
                {"F", "E"},
                {"G", "B"},
                {"H", "G"},
                {"I", "D"},
                {"J", "E"},
                {"K", "J"},
                {"L", "K"}
            };

            // Act
            var result = SolarSystem.CountHopsToCenter(mapOfUniverse, "L");

            // Assert
            result.Should().Be(expectedHopCount);
        }

        [Fact]
        public void MapTheUniverse_ListOfPlanets_MapOfTheUniverse()
        {
            // Arrange
            const string input = "COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L";
            var orbitPatterns = input.Split(',').ToList();
            var expectedMap = new Dictionary<string, string>
            {
                {"B", "COM"},
                {"C", "B"},
                {"D", "C"},
                {"E", "D"},
                {"F", "E"},
                {"G", "B"},
                {"H", "G"},
                {"I", "D"},
                {"J", "E"},
                {"K", "J"},
                {"L", "K"}
            };

            // Act
            var result = SolarSystem.BuildMapOfTheUniverse(orbitPatterns);

            foreach (var (k,v) in result)
            {
                var d = result[k] == expectedMap[k];
            }

            // Assert
            result.Should().ContainValues(expectedMap.Values)
                .And.Subject.Should().ContainKeys(expectedMap.Keys);
        }
    }
}
