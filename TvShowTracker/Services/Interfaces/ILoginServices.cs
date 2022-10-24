using TvShowTracker.Model;
namespace TvShowTracker.Interfaces
{
    public interface ILoginServices
    {
        string Login(UserPostDTO userLogin);
    }
}