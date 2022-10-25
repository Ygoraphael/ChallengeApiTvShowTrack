using TvShowTracker.Model;
namespace TvShowTracker.Interfaces
{
    public interface ILoginServices
    {
        Task<string> Login(UserPostDTO userLogin);
    }
}