using System;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        private const string DefaultFilePath = "input.txt";

        /// <summary>
        /// --- Day 8: Space Image Format ---
        /// https://adventofcode.com/2019/day/8
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            if (args.Any(x => x.Equals("--help")))
            {
                Console.WriteLine($"Provide the path to the picture data input .txt file (default: `{DefaultFilePath}`)");
                Environment.Exit(Environment.ExitCode);
            }

            var imageDataFilePath = args.FirstOrDefault(x => x.EndsWith(".txt")) ?? DefaultFilePath;

            var imageData = File.ReadAllText(imageDataFilePath);

            const int width = 25;
            var decodedData = ImageDecoder.Decode(imageData, width, 6);

            Console.WriteLine(ImageDecoder.CalculateLayerChecksum(decodedData));

            ImageDecoder.Print(ImageDecoder.ComputeVisibleImagePixels(decodedData), width);
        }
    }
}
