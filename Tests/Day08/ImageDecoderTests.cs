using System.Collections.Generic;
using System.Linq;
using Day8;
using FluentAssertions;
using Xunit;

namespace Tests.Day8
{
    public   class ImageDecoderTests
    {
        [Fact]
        public void Decode_PixelList_ReturnsLayers()
        {
            // Arrange
            const string input = "123456789012";
            var (width, height) = (3, 2);
            var expectedResult = new List<List<int>>
            {
                new List<int>
                {
                    1, 2, 3, 4, 5, 6
                },
                new List<int>
                {
                    7, 8, 9, 0, 1, 2
                }
            };

            // Act
            var result = ImageDecoder.Decode(input, width, height);

            // Assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public void CalculateVisibleImagePixels_LayerList_ReturnsVisibleLayer()
        {
            // Arrange
            var input = new List<List<int>>
            {
                new List<int>
                {
                    0,2,2,2
                },
                new List<int>
                {
                    1,1,2,2
                },
                new List<int>
                {
                    2,2,1,2
                },
                new List<int>
                {
                    0,0,0,0
                }
            };
            var expectedResult = new List<int>
            {
                0, 1, 1, 0
            };

            // Act
            var result = ImageDecoder.ComputeVisibleImagePixels(input);

            // Assert
            result.ShouldBeEquivalentTo(expectedResult);
        }
    }
}
