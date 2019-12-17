using System.Collections.Generic;
using Day12;
using FluentAssertions;
using Xunit;

namespace Tests.Day12
{
    public class MoonMapTests
    {
        [Theory]
        [MemberData(nameof(ParseCoordinatesTestData))]
        public void ParseCoordinates_CoordinateInput_ReturnsCoordinate(string input, Coordinate expectedOutput)
        {
            // Arrange & Act
            var result = MoonMap.ParseCoordinate(input);

            // Assert
            result.ShouldBeEquivalentTo(expectedOutput);
        }

        [Fact]
        public void SimulateSteps_Coordinates_ReturnsNewCoordinates()
        {
            // Arrange
            var moonCoordinates = new List<Coordinate>
            {
                new Coordinate(-1, 0, 2),
                new Coordinate(2, -10, -7),
                new Coordinate(4, -8, 8),
                new Coordinate(3, 5, -1)
            };

            // Act
            var result = MoonMap.SimulateSteps(moonCoordinates, 1);

            // Assert
            result.ShouldBeEquivalentTo(new List<Coordinate>
            {
                new Coordinate
                {
                    X  = (2,3),
                    Y = (-1,-1),
                    Z = (1,-1)
                },
                new Coordinate
                {
                    X  = (3,1),
                    Y = (-7,3),
                    Z = (-4,3)
                },
                new Coordinate
                {
                    X  = (1,-3),
                    Y = (-7,1),
                    Z = (5,-3)
                },
                new Coordinate
                {
                    X  = (2,-1),
                    Y = (2,-3),
                    Z = (0,1)
                }
            });
        }

        public static TheoryData<string, Coordinate> ParseCoordinatesTestData =>
            new TheoryData<string, Coordinate>
            {
                {
                    "<x=-1, y=0, z=2>",
                    new Coordinate(-1, 0, 2)
                },
                {
                    "<x=2, y=-10, z=-7>",
                    new Coordinate(2, -10, -7)
                },
                {
                    "<x=4, y=-8, z=8>",
                    new Coordinate(4, -8, 8)
                },
                {
                    "<x=3, y=5, z=-1>",
                    new Coordinate(3, 5, -1)
                }
            };
    }
}
