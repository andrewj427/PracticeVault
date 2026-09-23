using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;

namespace PracticeVault.Pages
{
    public class LibraryModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<LibrarySongViewModel> Songs { get; set; } = new();

        public LibraryModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task OnGetAsync()
        {
            // UserID 1 for now until login/session is connected
            Songs = await _httpClient.GetFromJsonAsync<List<LibrarySongViewModel>>(
                "http://api:8080/api/UserSongs/user/1"
            ) ?? new List<LibrarySongViewModel>();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(
                $"http://api:8080/api/UserSongs/{id}"
            );

            if (!response.IsSuccessStatusCode)
            {
                return Page();
            }

            return RedirectToPage();
        }

    }
}

    public class LibrarySongViewModel
    {
        public int UserSongID { get; set; }
        public int SongID { get; set; }
        public string Title { get; set; } = "";
        public string Artist { get; set; } = "";
        public string? Album { get; set; }
        public string? Tuning { get; set; }
        public string? Difficulty { get; set; }
        public string? TabURL { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = "";
    }
    
