using Restaurante.Domain.Products.Models;
using Xunit;

namespace Restaurante.Test.Domain.Entities
{
    public class PhotoTests
    {
        [Fact]
        public void ShouldCreateNewPhotoCorrectlyWithExpectedPath()
        {
            //arrange
            var expectedPath = "C:\\Teste";

            //act
            var photo = new Photo(expectedPath);

            //assert
            Assert.Equal(expectedPath, photo.Path);
        }

        [Fact]
        public void ShouldUpdatePhotoPath()
        {
            //arrange
            var expectedPath = "Test\\update";
            var photo = new Photo("teste");

            //act
            photo.UpdatePath(expectedPath);

            //assert
            Assert.Equal(expectedPath, photo.Path);
        }
    }
}
