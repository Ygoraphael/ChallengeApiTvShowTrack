using TvShowTracker.EntityFrameworkPaginateCore;
using Microsoft.EntityFrameworkCore;
using TvShowTracker.Interfaces;
using TvShowTracker.Model;
using TvShowTracker.Data;
using AutoMapper;
namespace TvShowTracker.Services
{
    public class GenreServices : IGenreServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GenreServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Page<Genre>> GetGenres(int skip, int take, string sort)
        {
            try
            {
                int count = await _context.Genres.AsNoTracking().CountAsync();
                return await _context.Genres.AsNoTracking()
                                .PaginateAsync((skip / take) + 1, take, count, sort);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Page<TvShow>> GetTvShowsGenre(int id, int skip, int take, string sort)
        {
            try
            {
                int count = await _context.TvShowGenres
                                     .AsNoTracking()
                                     .Where(t => t.GenreId == id)
                                     .CountAsync();
                return await _context.TvShowGenres
                                .AsNoTracking()
                                .Where(t => t.GenreId == id)
                                .Select(s => s.TvShow)
                                .PaginateAsync((skip / take) + 1, take, count, sort);
            }
            catch
            {
                throw;
            }
        }
    }
}