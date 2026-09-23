using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace PracticeVault.Pages
{
    public class AddSongModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public SongInputModel Song { get; set; } = new();

        public AddSongModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Song.Title) ||
                string.IsNullOrWhiteSpace(Song.Artist))
            {
                ModelState.AddModelError("", "Song title and artist are required.");
                return Page();
            }

            // Create the song first
            var songResponse = await _httpClient.PostAsJsonAsync(
                "http://api:8080/api/Songs",
                new
                {
                    title = Song.Title,
                    artist = Song.Artist,
                    album = Song.Album
                });

            if (!songResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Unable to add song.");
                return Page();
            }

            var createdSong =
                await songResponse.Content.ReadFromJsonAsync<CreatedSongModel>();

            if (createdSong == null)
            {
                ModelState.AddModelError("", "Unable to retrieve new song.");
                return Page();
            }

            // Then connect the song to the user
            var userSongResponse = await _httpClient.PostAsJsonAsync(
                "http://api:8080/api/UserSongs",
                new
                {
                    userID = 1,
                    songID = createdSong.Id,
                    tuning = Song.TuningType == "Drop"
    ? $"{Song.TuningType} {Song.TuningNote}"
    : $"{Song.TuningNote} {Song.TuningType}",
                    difficulty = Song.Difficulty,
                    tabURL = Song.TabURL,
                    notes = Song.Notes,
                    status = Song.Status,
                });

            if (!userSongResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError(
                    "",
                    "Song was created, but practice information could not be saved."
                );

                return Page();
            }

            return RedirectToPage("/Library");
        }

        public class CreatedSongModel
        {
            public int Id { get; set; }
        }

        public class SongInputModel
        {
            public string Title { get; set; } = "";
            public string Artist { get; set; } = "";
            public string? Album { get; set; }

            public string? TuningNote { get; set; }
            public string? TuningType { get; set; }
            public string? Difficulty { get; set; }
            public string Status { get; set; } = "Learn Later";
            public string? TabURL { get; set; }
            public string? Notes { get; set; }
        }
    }
}