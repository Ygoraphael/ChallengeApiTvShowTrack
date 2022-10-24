using TvShowTracker.Model;
namespace TvShowTracker.Interfaces
{
    public interface IUserServices
    {
        Task<UserGetDTO> GetUser(int id);
        Task<UserGetDTO> CreateUser(UserPostDTO user);
    }
}