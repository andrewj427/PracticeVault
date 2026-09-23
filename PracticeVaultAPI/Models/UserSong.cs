namespace PracticeVaultAPI.Models
{
    public class UserSong
    {
        public int UserSongID { get; set; }
        public int UserID { get; set; }
        public int SongID { get; set; }

        public string? Tuning { get; set; }
        public string? Difficulty { get; set; }
        public string? TabURL { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = "Learn Later";
        public DateTime DateAdded { get; set; }
    }
}