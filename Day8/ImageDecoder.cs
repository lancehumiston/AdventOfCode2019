using System;
using System.Collections.Generic;
using System.Linq;

namespace Day08
{
    public static class ImageDecoder
    {
        public static List<List<int>> Decode(string pixels, int width, int height)
        {
            var pixelList = pixels.ToCharArray().Select(x => (int)char.GetNumericValue(x));
            var layerArea = width * height;
            var layers = new List<List<int>>
            {
                new List<int>()
            };

            foreach (var pixel in pixelList)
            {
                if (layers.Last().Count == layerArea)
                {
                    layers.Add(new List<int>
                    {
                        pixel
                    });
                }
                else
                {
                    layers.Last().Add(pixel);
                }
            }

            return layers;
        }

        public static int CalculateLayerChecksum(List<List<int>> decodedData)
        {
            var layerOfInterest = decodedData.OrderBy(x => x.Count(layer => layer == 0)).First();

            return layerOfInterest.Count(x => x == 1) * layerOfInterest.Count(x => x == 2);
        }

        public static List<int> ComputeVisibleImagePixels(IReadOnlyCollection<IReadOnlyCollection<int>> decodedData)
        {
            const int transparentPixel = 2;
            var visibleImage = Enumerable.Repeat(transparentPixel, decodedData.First().Count).ToArray(); ;

            for (var layer = 0; layer < decodedData.Count && visibleImage.Contains(transparentPixel); layer++)
            {
                for (var i = 0; i < decodedData.First().Count; i++)
                {
                    if (visibleImage[i] == transparentPixel)
                    {
                        visibleImage[i] = decodedData.ElementAt(layer).ElementAt(i);
                    }
                }
            }

            return visibleImage.ToList();
        }

        public static void Print(IReadOnlyCollection<int> decodedData, int width)
        {
            for (var i = 0; i < decodedData.Count; i++)
            {
                if (i % width == 0)
                {
                    Console.Write("\n");
                }

                Console.Write($"  {(decodedData.ElementAt(i).Equals(1) ? '#' : ' ')}");
            }
        }
    }
}
