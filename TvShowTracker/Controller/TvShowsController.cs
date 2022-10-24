using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TvShowTracker.EntityFrameworkPaginateCore;
using TvShowTracker.Interfaces;
using TvShowTracker.Model;
namespace TvShowTracker.Controller
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TvShowsController : ControllerBase
    {
        private readonly ITvShowServices _tvShowServices;

        public TvShowsController(ITvShowServices tvShowServices)
        {
            _tvShowServices = tvShowServices;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Page<TvShow>>> GetTvShows
            ([FromQuery(Name = "$page")] int skip,
             [FromQuery(Name = "$pagesize")] int take,
             [FromQuery(Name = "$sortby")] string? sort)
        {
            take = take==0 ? 20 : take;
            skip = skip == 0 ? 0 : (skip-1) * take;
            try
            {
                var tvShows = await _tvShowServices.GetAll(skip, take, sort);
                if (tvShows.Results.Count() == 0)
                    return NotFound();
                else
                    return Ok(tvShows);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TvShowDTO>> GetTvShow(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                var tvShow = await _tvShowServices.GetbyId(id);
                if (tvShow == null)
                    return NotFound();
                else
                    return Ok(tvShow);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }
        [HttpGet("{id}/Actors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Page<Actor>>> GetActorsTvShow(
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
                var actors = await _tvShowServices.GetActorsTvShow(id, skip, take, sort);
                if (actors.Results.Count() == 0)
                    return NotFound();
                else
                    return Ok(actors);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        [HttpGet("{id}/Episodes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetEpisodesTvShow(
              int id,
             [FromQuery(Name = "$page")] int skip,
             [FromQuery(Name = "$pagesize")] int take,
             [FromQuery(Name = "$sortby")] string? sort)
        {
            if (id == 0) return BadRequest();
            take = take==0 ? 20 : take;
            skip = skip == 0 ? 0 : (skip-1) * take;
            try
            {
                var episodes = await _tvShowServices.GetEpisodesTvShow(id, skip, take, sort);
                if (episodes.Results.Count() == 0)
                    return NotFound();
                else
                    return Ok(episodes);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("{id}/Episodes/{idEpisode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EpisodeDTO>> GetEpisodeTvShow(int id, int idEpisode)
        {
            if (id == 0 || idEpisode == 0) return BadRequest();
            try
            {
                var episode = await _tvShowServices.GetEpisodeTvShow(id, idEpisode);
                if (episode == null)
                    return NotFound();
                else
                    return Ok(episode);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}