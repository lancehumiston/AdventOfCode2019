using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day12
{
    public class Coordinate
    {
        public Coordinate() { }

        public Coordinate(int x, int y, int z)
        {
            X = (x, 0);
            Y = (y, 0);
            Z = (z, 0);
        }

        public Coordinate((int, int) x, (int, int) y, (int, int) z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// {Position, Velocity} in x-dimension
        /// </summary>
        public (int, int) X { get; set; }

        /// <summary>
        /// {Position, Velocity} in y-dimension
        /// </summary>
        public (int, int) Y { get; set; }

        /// <summary>
        /// {Position, Velocity} in z-dimension
        /// </summary>
        public (int, int) Z { get; set; }
    }

    public static class MoonMap
    {
        public static Coordinate ParseCoordinate(string input)
        {
            var coordinateValues = Regex.Matches(input, @"-?\d+")
                .Select(match => int.Parse(match.Value))
                .ToList();

            return new Coordinate(coordinateValues[0], coordinateValues[1], coordinateValues[2]);
        }

        public static List<Coordinate> SimulateSteps(List<Coordinate> moonCoordinates, int stepCount)
        {
            for (var i = 0; i < stepCount; i++)
            {
                var newCoordinates = new List<Coordinate>();

                foreach (var moon in moonCoordinates)
                {
                    var xVelocity = ApplyGravity(moon.X, moonCoordinates.Where(x => !x.Equals(moon)).Select(x => x.X.Item1));
                    var yVelocity = ApplyGravity(moon.Y, moonCoordinates.Where(x => !x.Equals(moon)).Select(x => x.Y.Item1));
                    var zVelocity = ApplyGravity(moon.Z, moonCoordinates.Where(x => !x.Equals(moon)).Select(x => x.Z.Item1));

                    var xCoordinate = ApplyVelocity(moon.X.Item1, xVelocity);
                    var yCoordinate = ApplyVelocity(moon.Y.Item1, yVelocity);
                    var zCoordinate = ApplyVelocity(moon.Z.Item1, zVelocity);

                    newCoordinates.Add(new Coordinate((xCoordinate, xVelocity), (yCoordinate, yVelocity), (zCoordinate, zVelocity)));
                }

                moonCoordinates = new List<Coordinate>(newCoordinates);
            }

            return moonCoordinates;
        }

        private static int ApplyGravity ((int, int) sourceValue, IEnumerable<int> destinationValues)
        {
            var delta = 0;
            var (sourceCoordinate, sourceVelocity) = sourceValue;

            foreach (var destinationValue in destinationValues)
            {
                if (sourceCoordinate < destinationValue)
                {
                    delta++;
                }
                else if (sourceCoordinate > destinationValue)
                {
                    delta--;
                }
            }

            return sourceVelocity + delta;
        }

        private static int ApplyVelocity(int currentCoordinate, int velocity)
        {
            return currentCoordinate + velocity;
        }

        public static int CalculateEnergy(List<Coordinate> moonCoordinates)
        {
            var energies = new List<int>();

            foreach (var moonCoordinate in moonCoordinates)
            {
                var potentialEnergy = Math.Abs(moonCoordinate.X.Item1) + Math.Abs(moonCoordinate.Y.Item1) + Math.Abs(moonCoordinate.Z.Item1);
                var kineticEnergy = Math.Abs(moonCoordinate.X.Item2) + Math.Abs(moonCoordinate.Y.Item2) + Math.Abs(moonCoordinate.Z.Item2);

                energies.Add(potentialEnergy * kineticEnergy);
            }

            return energies.Sum();
        }
    }
}
