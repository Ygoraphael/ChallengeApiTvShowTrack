using TvShowTracker.EntityFrameworkPaginateCore;
using TvShowTracker.Model;
namespace TvShowTracker.Interfaces
{
    public interface IActorServices
    {
        Task<Actor> GetActor(int id);
        Task<Page<Actor>> GetActors(int skip, int take, string sort);
        Task<Page<TvShow>> GetActorTvShows(int id, int skip, int take, string sort);
    }
}