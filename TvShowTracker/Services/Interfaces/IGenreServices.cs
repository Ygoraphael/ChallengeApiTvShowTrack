using TvShowTracker.EntityFrameworkPaginateCore;
using TvShowTracker.Model;
namespace TvShowTracker.Interfaces
{
    public interface IGenreServices
    {
        Task<Page<Genre>> GetGenres(int skip, int take,string sort);
        Task<Page<TvShow>> GetTvShowsGenre(int id, int skip, int take, string sort);
    }
}