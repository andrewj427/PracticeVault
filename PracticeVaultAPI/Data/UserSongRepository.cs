using Microsoft.Data.SqlClient;
using PracticeVaultAPI.Models;

namespace PracticeVaultAPI.Data
{
    public class UserSongRepository : IUserSongRepository
    {
        private readonly string _connectionString;

        public UserSongRepository(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")!;
        }

        public int Create(UserSong userSong)
        {
            using SqlConnection connection =
                new SqlConnection(_connectionString);

            connection.Open();

            string sql = @"
        INSERT INTO UserSongs
            (UserID, SongID, Tuning, Difficulty, TabURL, Notes, Status)
        OUTPUT INSERTED.UserSongID
        VALUES
            (@UserID, @SongID, @Tuning, @Difficulty, @TabURL, @Notes, @Status);";

            using SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@UserID", userSong.UserID);
            command.Parameters.AddWithValue("@SongID", userSong.SongID);

            command.Parameters.AddWithValue(
                "@Tuning",
                (object?)userSong.Tuning ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@Difficulty",
                (object?)userSong.Difficulty ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@TabURL",
                (object?)userSong.TabURL ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@Notes",
                (object?)userSong.Notes ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@Status",
                userSong.Status);

            return Convert.ToInt32(command.ExecuteScalar());
        }
        public List<LibrarySong> GetLibrary(int userId)
        {
            var songs = new List<LibrarySong>();

            using SqlConnection connection =
                new SqlConnection(_connectionString);

            connection.Open();

            string sql = @"
    SELECT
        us.UserSongID,
        s.SongID,
        s.Title,
        s.Artist,
        s.Album,
        us.Tuning,
        us.Difficulty,
        us.TabURL,
        us.Notes,
        us.Status
    FROM UserSongs us
    INNER JOIN Songs s
        ON us.SongID = s.SongID
    WHERE us.UserID = @UserID
    ORDER BY us.DateAdded DESC;";

            using SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@UserID", userId);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                songs.Add(new LibrarySong
                {
                    UserSongID = reader.GetInt32(0),
                    SongID = reader.GetInt32(1),
                    Title = reader.GetString(2),
                    Artist = reader.GetString(3),
                    Album = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Tuning = reader.IsDBNull(5) ? null : reader.GetString(5),
                    Difficulty = reader.IsDBNull(6) ? null : reader.GetString(6),
                    TabURL = reader.IsDBNull(7) ? null : reader.GetString(7),
                    Notes = reader.IsDBNull(8) ? null : reader.GetString(8),
                    Status = reader.IsDBNull(9) ? "" : reader.GetString(9)
                });
            }

            return songs;
        }

        public UserSong? GetById(int userSongId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string sql = @"
        SELECT
            UserSongID,
            UserID,
            SongID,
            Tuning,
            Difficulty,
            TabURL,
            Notes,
            Status,
            DateAdded
        FROM UserSongs
        WHERE UserSongID = @UserSongID;";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@UserSongID", userSongId);

            using SqlDataReader reader = command.ExecuteReader();

            if (!reader.Read())
                return null;

            return new UserSong
            {
                UserSongID = reader.GetInt32(0),
                UserID = reader.GetInt32(1),
                SongID = reader.GetInt32(2),

                Tuning = reader.IsDBNull(3) ? null : reader.GetString(3),
                Difficulty = reader.IsDBNull(4) ? null : reader.GetString(4),
                TabURL = reader.IsDBNull(5) ? null : reader.GetString(5),
                Notes = reader.IsDBNull(6) ? null : reader.GetString(6),
                Status = reader.IsDBNull(7) ? "" : reader.GetString(7),

                DateAdded = reader.GetDateTime(8)
            };
        }

        public bool Update(UserSong userSong)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string sql = @"
        UPDATE UserSongs
        SET
            Tuning = @Tuning,
            Difficulty = @Difficulty,
            TabURL = @TabURL,
            Notes = @Notes,
            Status = @Status
        WHERE UserSongID = @UserSongID;";

            using SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@UserSongID",
                userSong.UserSongID);

            command.Parameters.AddWithValue(
                "@Tuning",
                (object?)userSong.Tuning ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@Difficulty",
                (object?)userSong.Difficulty ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@TabURL",
                (object?)userSong.TabURL ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@Notes",
                (object?)userSong.Notes ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@Status",
                userSong.Status);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int userSongId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string sql = @"
        DELETE FROM UserSongs
        WHERE UserSongID = @UserSongID;";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@UserSongID", userSongId);

            return command.ExecuteNonQuery() > 0;
        }
    }
}