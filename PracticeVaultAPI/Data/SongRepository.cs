using Microsoft.Data.SqlClient;
using PracticeVaultAPI.Models;

namespace PracticeVaultAPI.Data
{
    public class SongRepository : ISongRepository
    {
        private readonly string _connectionString;

        public SongRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public List<Song> GetAll()
        {
            var songs = new List<Song>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"
                SELECT SongID, Title, Artist, Album, DurationSeconds
                FROM Songs";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                songs.Add(new Song
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Artist = reader.GetString(2),
                    Album = reader.IsDBNull(3) ? null : reader.GetString(3),
                    DurationSeconds = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                });
            }

            return songs;
        }

        public Song? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"
                SELECT SongID, Title, Artist, Album, DurationSeconds
                FROM Songs
                WHERE SongID = @SongID";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SongID", id);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return null;

            return new Song
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Artist = reader.GetString(2),
                Album = reader.IsDBNull(3) ? null : reader.GetString(3),
                DurationSeconds = reader.IsDBNull(4) ? null : reader.GetInt32(4)
            };
        }
        public int Create(Song song)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"
        INSERT INTO Songs (Title, Artist, Album, DurationSeconds)
        OUTPUT INSERTED.SongID
        VALUES (@Title, @Artist, @Album, @DurationSeconds)";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Title", song.Title);
            command.Parameters.AddWithValue("@Artist", song.Artist);
            command.Parameters.AddWithValue("@Album",
                (object?)song.Album ?? DBNull.Value);
            command.Parameters.AddWithValue("@DurationSeconds",
                (object?)song.DurationSeconds ?? DBNull.Value);

            return (int)command.ExecuteScalar()!;
        }
    }
}