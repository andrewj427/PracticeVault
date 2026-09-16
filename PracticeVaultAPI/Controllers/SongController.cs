using Microsoft.AspNetCore.Mvc;
using PracticeVaultAPI.Data;
using PracticeVaultAPI.Models;

namespace PracticeVaultAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSongs()
        {
            var songs = _songRepository.GetAll();
            return Ok(songs);
        }
    
        [HttpGet("{id}")]
        public IActionResult GetSong(int id)
        {
            var song = _songRepository.GetById(id);

            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }
        private readonly ISongRepository _songRepository;

        public SongsController(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }
        [HttpPost]
        public IActionResult CreateSong(Song song)
        {
            int id = _songRepository.Create(song);

            song.Id = id;

            return CreatedAtAction(
                nameof(GetSong),
                new { id = id },
                song
            );
        }
    }
}
