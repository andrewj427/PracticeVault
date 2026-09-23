using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace PracticeVault.Pages
{
    public class EditSongModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public EditSongViewModel Song { get; set; } = new();

        public EditSongModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userSong = await _httpClient.GetFromJsonAsync<UserSongApiModel>(
                $"http://api:8080/api/UserSongs/{id}"
            );

            if (userSong == null)
                return NotFound();

            // Get the normal song information too
            var songInfo = await _httpClient.GetFromJsonAsync<SongApiModel>(
                $"http://api:8080/api/Songs/{userSong.SongID}"
            );

            Song = new EditSongViewModel
            {
                UserSongID = userSong.UserSongID,
                UserID = userSong.UserID,
                SongID = userSong.SongID,

                Title = songInfo?.Title ?? "",
                Artist = songInfo?.Artist ?? "",
                Album = songInfo?.Album,

                Tuning = userSong.Tuning,
                Difficulty = userSong.Difficulty,
                TabURL = userSong.TabURL,
                Notes = userSong.Notes,
                Status = userSong.Status,
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Song.Title) ||
                string.IsNullOrWhiteSpace(Song.Artist))
            {
                ModelState.AddModelError("", "Song title and artist are required.");
                return Page();
            }

            // Update the Songs table
            var songResponse = await _httpClient.PutAsJsonAsync(
                $"http://api:8080/api/Songs/{Song.SongID}",
                new
                {
                    id = Song.SongID,
                    title = Song.Title,
                    artist = Song.Artist,
                    album = Song.Album
                }
            );

            if (!songResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Unable to update song information.");
                return Page();
            }

            // Update the UserSongs table
            var userSongResponse = await _httpClient.PutAsJsonAsync(
                $"http://api:8080/api/UserSongs/{Song.UserSongID}",
                new
                {
                    userSongID = Song.UserSongID,
                    userID = Song.UserID,
                    songID = Song.SongID,
                    tuning = Song.Tuning,
                    difficulty = Song.Difficulty,
                    tabURL = Song.TabURL,
                    notes = Song.Notes,
                    status = Song.Status,
                }
            );

            if (!userSongResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Unable to update practice information.");
                return Page();
            }

            return RedirectToPage("/Library");
        }
    }

    public class EditSongViewModel
    {
        public int UserSongID { get; set; }
        public int UserID { get; set; }
        public int SongID { get; set; }

        public string Title { get; set; } = "";
        public string Artist { get; set; } = "";
        public string? Album { get; set; }

        public string? Tuning { get; set; }
        public string? Difficulty { get; set; }
        public string? TabURL { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = "Learn Later";
    }
        public class UserSongApiModel
        {
            public int UserSongID { get; set; }
            public int UserID { get; set; }
            public int SongID { get; set; }

            public string? Tuning { get; set; }
            public string? Difficulty { get; set; }
            public string? TabURL { get; set; }
            public string? Notes { get; set; }
            public string Status { get; set; } = "";
        }

        public class SongApiModel
        {
            public int Id { get; set; }
            public string Title { get; set; } = "";
            public string Artist { get; set; } = "";
            public string? Album { get; set; }
        }
    }
