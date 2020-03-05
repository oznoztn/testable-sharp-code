using Blog.Services;
using Xunit;

namespace Blog.Tests.Services
{
    public class ImageServiceTests
    {
        [Fact]
        public void CalculateOptimalSizeShouldReturnMinimumSizeWhenSizeIsLessThanTheAllowedMinimum()
        {
            // arrange
            const int minimumSize = 100;
            const int originalHeight = 200;
            const int originalWidth = 200;
            const int resizedSize = 50;
            var imageService = new ImageService();

            // act
            (var width, var height) = 
                imageService.CalculateOptimalSize(resizedSize, minimumSize, originalWidth, originalHeight);

            // assert

            Assert.Equal(minimumSize, width);
            Assert.Equal(minimumSize, height);
        }
    }
}
