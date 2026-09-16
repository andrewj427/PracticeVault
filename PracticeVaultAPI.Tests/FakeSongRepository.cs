using PracticeVaultAPI.Data;
using PracticeVaultAPI.Models;

namespace PracticeVaultAPI.Tests
{
    public class FakeSongRepository : ISongRepository
    {
        private readonly List<Song> _songs = new()
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
            }
        };

        public List<Song> GetAll()
        {
            return _songs;
        }

        public Song? GetById(int id)
        {
            return _songs.FirstOrDefault(song => song.Id == id);
        }

        public int Create(Song song)
        {
            song.Id = _songs.Count + 1;
            _songs.Add(song);

            return song.Id;
        }
    }
}