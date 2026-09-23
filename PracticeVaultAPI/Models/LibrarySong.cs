namespace PracticeVaultAPI.Models
{
    public class LibrarySong
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
}