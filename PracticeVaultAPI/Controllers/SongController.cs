using Microsoft.AspNetCore.Mvc;
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
            var songs = new List<Song>
            {
                new Song
                {
                    Id = 1,
                    Title = "Hail to the King",
                    Artist = "Avenged Sevenfold",
                    Album = "Hail to the King",
                    DurationSeconds = 304
                },

                new Song
                {
                    Id = 2,
                    Title = "Bat Country",
                    Artist = "Avenged Sevenfold",
                    Album = "City of Evil",
                    DurationSeconds = 312
                },

                new Song
                {
                    Id = 3,
                    Title = "November Rain",
                    Artist = "Guns N' Roses",
                    Album = "Use Your Illusion I",
                    DurationSeconds = 537
                }
            };

            return Ok(songs);
        }
    }


    [HttpGet("{id}")]
        public IActionResult GetSong(int id)
        {
            var songs = new List<Song>
    {
        new Song
        {
            Id = 1,
            Title = "Hail to the King",
            Artist = "Avenged Sevenfold",
            Album = "Hail to the King",
            DurationSeconds = 304
        },

        new Song
        {
            Id = 2,
            Title = "Bat Country",
            Artist = "Avenged Sevenfold",
            Album = "City of Evil",
            DurationSeconds = 312
        },

        new Song
        {
            Id = 3,
            Title = "November Rain",
            Artist = "Guns N' Roses",
            Album = "Use Your Illusion I",
            DurationSeconds = 537
        }
    };

            var song = songs.FirstOrDefault(s => s.Id == id);

            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }
    } }
