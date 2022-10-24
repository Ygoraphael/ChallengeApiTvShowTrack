using TvShowTracker.EntityFrameworkPaginateCore;
using TvShowTracker.Model;
namespace TvShowTracker.Interfaces
{
    public interface ITvShowServices
    {
        Task<Page<TvShow>> GetAll(int skip, int take, string sort);
        Task<TvShowDTO> GetbyId(int id);
        Task<Page<Actor>> GetActorsTvShow(int id, int skip, int take, string sort);
        Task<Page<ICollection<Episode>>> GetEpisodesTvShow(int id, int skip, int take, string sort);
        Task<EpisodeDTO> GetEpisodeTvShow(int id, int idEp);
    }
}