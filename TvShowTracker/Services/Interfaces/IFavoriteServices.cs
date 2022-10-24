using TvShowTracker.EntityFrameworkPaginateCore;
using TvShowTracker.Model;
namespace TvShowTracker.Interfaces
{
    public interface IFavoriteServices
    {
        Task<Page<TvShow>> GetFavoriteTvShows(int userId, int skip, int take, string sort);
        Task AddFavoriteIfDoesntExist(int userId, int tvShowId);
        Task<Boolean> DeleteFavorite(int userId, int tvShowId);
    }
}