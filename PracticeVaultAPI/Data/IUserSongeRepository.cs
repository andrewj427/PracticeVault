using PracticeVaultAPI.Models;

public interface IUserSongRepository
{
    int Create(UserSong userSong);
    List<LibrarySong> GetLibrary(int userId);
    UserSong? GetById(int userSongId);
    bool Update(UserSong userSong);
    bool Delete(int userSongId);
}