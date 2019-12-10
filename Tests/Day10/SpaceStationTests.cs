using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day10;
using FluentAssertions;
using Xunit;

namespace Tests.Day10
{
    public class SpaceStationTests
    {
        [Theory]
        [InlineData(@".#..#
.....
#####
....#
...##", 8, "3,4")]
        [InlineData(@"......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####", 33, "5,8")]
        [InlineData(@"#.#...#.#.
.###....#.
.#....#...
##.#.#.#.#
....#.#.#.
.##..###.#
..#...##..
..##....##
......#...
.####.###.", 35, "1,2")]
        [InlineData(@".#..#..###
####.###.#
....###.#.
..###.##.#
##.##.#.#.
....###..#
..#.#..#.#
#..#.#.###
.##...##.#
.....#.#..", 41, "6,3")]
        [InlineData(@".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##", 210, "11,13")]
        public void GetBestSpaceStation_MapOfAsteroids_ReturnsSpaceStationData(string asteroidMap, int expectedCount, string expectedLocation)
        {
            // Arrange
            var coordinates = expectedLocation.Split(',');
            var location = new Point(int.Parse(coordinates[0]), int.Parse(coordinates[1]));
            var rows = asteroidMap.Split(Environment.NewLine).ToList();

            // Act
            var (spaceStationLocation, asteroidCount, _) = SpaceStation.GetBestSpaceStation(rows);

            // Assert
            //spaceStationLocation.ShouldBeEquivalentTo(location);
            asteroidCount.Should().Be(expectedCount);
        }
    }
}
