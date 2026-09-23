using Microsoft.AspNetCore.Mvc;
using PracticeVaultAPI.Data;
using PracticeVaultAPI.Models;

namespace PracticeVaultAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSongsController : ControllerBase
    {
        private readonly IUserSongRepository _userSongRepository;

        public UserSongsController(IUserSongRepository userSongRepository)
        {
            _userSongRepository = userSongRepository;
        }

        [HttpPost]
        public IActionResult CreateUserSong([FromBody] UserSong userSong)
        {
            if (userSong.UserID <= 0 || userSong.SongID <= 0)
            {
                return BadRequest("UserID and SongID are required.");
            }

            int newId = _userSongRepository.Create(userSong);
            userSong.UserSongID = newId;

            return Ok(userSong);
        }
        [HttpGet("user/{userId}")]
        public IActionResult GetLibrary(int userId)
        {
            var songs = _userSongRepository.GetLibrary(userId);

            return Ok(songs);
        }
        [HttpGet("{id}")]
        public IActionResult GetUserSong(int id)
        {
            var userSong = _userSongRepository.GetById(id);

            if (userSong == null)
                return NotFound();

            return Ok(userSong);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserSong(int id, [FromBody] UserSong userSong)
        {
            userSong.UserSongID = id;

            bool updated = _userSongRepository.Update(userSong);

            if (!updated)
                return NotFound();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUserSong(int id)
        {
            bool deleted = _userSongRepository.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}