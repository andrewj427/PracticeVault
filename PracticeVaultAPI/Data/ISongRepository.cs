using PracticeVaultAPI.Models;

namespace PracticeVaultAPI.Data
{
    public interface ISongRepository
    {
        List<Song> GetAll();
        Song? GetById(int id);
        int Create(Song song);
    }
}