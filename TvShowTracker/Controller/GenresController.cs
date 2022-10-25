using TvShowTracker.EntityFrameworkPaginateCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TvShowTracker.Interfaces;
using TvShowTracker.Model;

namespace TvShowTracker.Controller
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class GenresController : ControllerBase
    {
        private readonly IGenreServices _genreService;
        public GenresController(IGenreServices genreService)
        {
            _genreService = genreService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Page<Genre>>> GetGenres
            ([FromQuery(Name = "$page")] int skip,
             [FromQuery(Name = "$pagesize")] int take,
             [FromQuery(Name = "$sortby")] string? sort)
        {
            take = take == 0 ? 20 : take;
            skip = skip == 0 ? 0 : (skip - 1) * take;
            try
            {
                var genre = await _genreService.GetGenres(skip, take, sort);
                if (genre.Results.Count() == 0)
                    return NotFound();
                else
                    return Ok(genre);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }
        [HttpGet("{id}/TvShows")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Page<TvShow>>> GetTvShowsGenre(
              int id,
             [FromQuery(Name = "$page")] int skip,
             [FromQuery(Name = "$pagesize")] int take,
             [FromQuery(Name = "$sortby")] string? sort)
        {
            if (id == 0) return BadRequest();
            take = take == 0 ? 20 : take;
            skip = skip == 0 ? 0 : (skip - 1) * take;
            try
            {
                var genre = await _genreService.GetTvShowsGenre(id, skip, take, sort);
                if (genre.Results.Count() == 0)
                    return NotFound();
                else
                    return Ok(genre);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}