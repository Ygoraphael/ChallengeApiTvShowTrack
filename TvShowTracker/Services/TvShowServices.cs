using TvShowTracker.EntityFrameworkPaginateCore;
using Microsoft.EntityFrameworkCore;
using TvShowTracker.Interfaces;
using TvShowTracker.Model;
using TvShowTracker.Data;
using AutoMapper;
namespace TvShowTracker.Services
{
    public class TvShowServices : ITvShowServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TvShowServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Page<Actor>> GetActorsTvShow(int id, int skip, int take, string sort)
        {
            try
            {
                int count = await _context.EpisodesActors
                                    .AsNoTracking()
                                    .Include(a => a.Episode)
                                     .ThenInclude(e => e.Season)
                                     .Where(a => a.Episode.Season.TvShowId == id)
                                     .Select(a => a.Actor)
                                     .Distinct()
                                     .CountAsync();
                return await _context.EpisodesActors
                                    .AsNoTracking()
                                    .Include(a => a.Episode)
                                     .ThenInclude(e => e.Season)
                                     .Where(a => a.Episode.Season.TvShowId == id)
                                     .Select(a => a.Actor)
                                     .Distinct()
                                    .Skip(skip)
                                    .Take(take)
                                    .PaginateAsync((skip / take) + 1, take, count, sort);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Page<TvShow>> GetAll(int skip, int take, string sort)
        {
            try
            {
                int count = await _context.TvShows.AsNoTracking().CountAsync();
                return await _context.TvShows
                                    .AsNoTracking()
                                    .Skip(skip)
                                    .Take(take)
                                    .PaginateAsync((skip/take)+1, take, count, sort);
            }
            catch
            {
                throw;
            }
        }
        public async Task<TvShowDTO> GetbyId(int id)
        {
            try
            {
                var tvShow = await _context.TvShows.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                return _mapper.Map<TvShowDTO>(tvShow);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Page<Episode>> GetEpisodesTvShow(int id, int skip, int take, string sort)
        {
            try
            {
                int count = await _context.Episodes.AsNoTracking()
                                        .Where(s => s.Season.TvShowId == id)
                                        .CountAsync();
                return await _context.Episodes.AsNoTracking()
                                     .Where(s => s.Season.TvShowId == id)
                                     .Skip(skip)
                                     .Take(take)
                                    .PaginateAsync((skip / take) + 1, take, count, sort);
            }
            catch
            {
                throw;
            }
        }
        public async Task<EpisodeDTO> GetEpisodeTvShow(int id, int idEp)
        {
            try
            {
                var episode = (List<Episode>) await _context.Seasons
                                    .AsNoTracking()
                                    .Where(e => e.TvShowId == id)
                                    .Select(s => s.Episodes.Where(e => e.Id == idEp))
                                    .FirstOrDefaultAsync();
                return _mapper.Map<EpisodeDTO>(episode.FirstOrDefault());
            }
            catch
            {
                throw;
            }
        }
    }
}