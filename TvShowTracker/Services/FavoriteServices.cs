using TvShowTracker.EntityFrameworkPaginateCore;
using Microsoft.EntityFrameworkCore;
using TvShowTracker.Interfaces;
using TvShowTracker.Model;
using TvShowTracker.Data;
using AutoMapper;
namespace TvShowTracker.Services
{
    public class FavoriteServices : IFavoriteServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public FavoriteServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddFavoriteIfDoesntExist(int userId, int tvShowId)
        {
            try
            {
                var favorite = await _context.FavoriteTvShows.AsNoTracking().FirstOrDefaultAsync(f => f.UserId == userId && f.TvShowId == tvShowId);
                if (favorite == null) {
                    _context.FavoriteTvShows.Add(new FavoriteTvShow() { UserId = userId, TvShowId = tvShowId });
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<Boolean> DeleteFavorite(int userId, int tvShowId)
        {
            try
            {
                var favorite = await _context.FavoriteTvShows.FirstOrDefaultAsync(f=> f.UserId == userId && f.TvShowId == tvShowId);
                if (favorite == null)
                    return false;
                else
                {
                    _context.FavoriteTvShows.Remove(favorite);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Page<TvShow>> GetFavoriteTvShows(int userId, int skip, int take, string sort)
        {
            try
            {
                int count = _context.FavoriteTvShows.AsNoTracking()
                                .Where(f => f.UserId == userId).Count();
                return await _context.FavoriteTvShows.AsNoTracking()
                                .Where(f => f.UserId == userId)
                                .Select(f => f.TvShow)
                                .PaginateAsync((skip / take) + 1, take, count, sort);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}