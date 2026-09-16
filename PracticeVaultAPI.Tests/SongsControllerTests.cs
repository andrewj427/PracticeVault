using Microsoft.AspNetCore.Mvc;
using PracticeVaultAPI.Controllers;
using PracticeVaultAPI.Models;

namespace PracticeVaultAPI.Tests
{
    public class SongsControllerTests
    {
        [Fact]
        public void GetSongs_ReturnsOkResult()
        {
            var repository = new FakeSongRepository();
            var controller = new SongsController(repository);

            var result = controller.GetSongs();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetSong_ValidId_ReturnsCorrectSong()
        {
            var repository = new FakeSongRepository();
            var controller = new SongsController(repository);

            var result = controller.GetSong(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var song = Assert.IsType<Song>(okResult.Value);

            Assert.Equal("Hail to the King", song.Title);
        }

        [Fact]
        public void GetSong_InvalidId_ReturnsNotFound()
        {
            var repository = new FakeSongRepository();
            var controller = new SongsController(repository);

            var result = controller.GetSong(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}