using TvShowTracker.EntityFrameworkPaginateCore;
using Microsoft.EntityFrameworkCore;
using TvShowTracker.Interfaces;
using TvShowTracker.Model;
using TvShowTracker.Data;
using AutoMapper;
namespace TvShowTracker.Services
{
    public class ActorServices : IActorServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ActorServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Actor> GetActor(int id)
        {
            try
            {
                return await _context.Actors.AsNoTracking().SingleAsync(a=>a.Id == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Page<Actor>> GetActors(int skip, int take, string sort)
        {
            try
            {
                int count = await _context.Actors.AsNoTracking().CountAsync();
                return await _context.Actors.AsNoTracking()
                                .Skip(skip)
                                .Take(take)
                                .PaginateAsync((skip / take) + 1, take, count, sort); 
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<Page<TvShow>> GetActorTvShows(int id, int skip, int take, string sort)
        {
            try
            {
                int count = await _context.EpisodesActors
                                    .AsNoTracking()
                                    .Where(a => a.ActorId == id)
                                    .Select(a => a.Episode.Season.TvShow)
                                    .Distinct()
                                    .CountAsync();
                return await _context.EpisodesActors
                                    .AsNoTracking()
                                    .Where(a => a.ActorId == id)
                                    .Select(a => a.Episode.Season.TvShow)
                                    .Skip(skip)
                                    .Take(take)
                                    .Distinct()
                                    .PaginateAsync((skip / take) + 1, take, count, sort);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}