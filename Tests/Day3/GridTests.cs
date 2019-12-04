using System.Collections.Generic;
using System.Drawing;
using Day3;
using FluentAssertions;
using Xunit;

namespace Tests.Day3
{
    public class GridTests
    {
        [Fact]
        public void PlotPoints_ListOfVectors_ReturnsListOfPoints()
        {
            // Arrange
            var vectors = new List<string>{"R2","U1","L1","D1"};
            var expectedPoints = new List<Point>
            {
                new Point(0,0),
                new Point(1,0),
                new Point(2,0),
                new Point(2,1),
                new Point(1,1),
                new Point(1,0)
            };

            // Act
            var result = Grid.PlotPoints(vectors);

            // Assert
            result.Should().BeEquivalentTo(expectedPoints);
        }

        [Theory]
        [MemberData(nameof(OrderByManhattanDistanceTestData))]
        public void OrderByManhattanDistance_ListOfVectors_ReturnsListOfPoints(List<string> wire1, List<string> wire2, int expectedDistance)
        {
            // Arrange
            var wires = new List<List<Point>>
            {
                Grid.PlotPoints(wire1),
                Grid.PlotPoints(wire2)
            };
            var intersectionPoints = Grid.FindIntersectionPoints(wires);

            // Act
            var result = Grid.FindClosestManhattanDistance(intersectionPoints);

            // Assert
            result.Should().Be(expectedDistance);
        }

        public static IEnumerable<object[]> OrderByManhattanDistanceTestData =>
            new List<object[]>
            {
                new object[]
                {
                    new List<string> { "R8", "U5", "L5", "D3" },
                    new List<string> { "U7", "R6", "D4", "L4" },
                    6
                },
                new object[]
                {
                    new List<string> { "R75", "D30", "R83", "U83", "L12", "D49", "R71", "U7", "L72" },
                    new List<string> { "U62", "R66", "U55", "R34", "D71", "R55", "D58", "R83" },
                    159
                },
                new object[]
                {
                    new List<string> { "R98", "U47", "R26", "D63", "R33", "U87", "L62", "D20", "R33", "U53", "R51" },
                    new List<string> { "U98", "R91", "D20", "R16", "D67", "R40", "U7", "R15", "U6", "R7" },
                    135
                }
            };

        [Theory]
        [MemberData(nameof(FindLowestStepCountToIntersectionTestData))]
        public void FindLowestStepCountToIntersection_ListOfVectors_ReturnsListOfPoints(List<string> wire1, List<string> wire2, int expectedLowestStepCount)
        {
            // Arrange
            var wires = new List<List<Point>>
            {
                Grid.PlotPoints(wire1),
                Grid.PlotPoints(wire2)
            };
            var intersectionPoints = Grid.FindIntersectionPoints(wires);

            // Act
            var result = Grid.FindLowestStepCountToIntersection(wires, intersectionPoints);

            // Assert
            result.Should().Be(expectedLowestStepCount);
        }

        public static IEnumerable<object[]> FindLowestStepCountToIntersectionTestData =>
            new List<object[]>
            {
                new object[]
                {
                    new List<string> { "R8", "U5", "L5", "D3" },
                    new List<string> { "U7", "R6", "D4", "L4" },
                    30
                },
                new object[]
                {
                    new List<string> { "R75", "D30", "R83", "U83", "L12", "D49", "R71", "U7", "L72" },
                    new List<string> { "U62", "R66", "U55", "R34", "D71", "R55", "D58", "R83" },
                    610
                },
                new object[]
                {
                    new List<string> { "R98", "U47", "R26", "D63", "R33", "U87", "L62", "D20", "R33", "U53", "R51" },
                    new List<string> { "U98", "R91", "D20", "R16", "D67", "R40", "U7", "R15", "U6", "R7" },
                    410
                }
            };
    }
}
