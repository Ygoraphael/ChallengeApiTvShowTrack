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
    public class ActorsController : ControllerBase
    {
        private readonly IActorServices _actorServices;
        public ActorsController(IActorServices actorServices)
        {
            _actorServices = actorServices;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Page<ActorDTO>>> GetActors
            ([FromQuery(Name = "$page")] int skip,
             [FromQuery(Name = "$pagesize")] int take,
             [FromQuery(Name = "$sortby")] string? sort)
        {
            take = take == 0 ? 20 : take;
            skip = skip == 0 ? 0 : (skip - 1) * take;
            try
            {
                var actors = await _actorServices.GetActors(skip, take, sort);
                if (actors.Results.Count() == 0)
                    return NotFound();
                else
                    return Ok(actors);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Actor>> GetActorById(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                var actor = await _actorServices.GetActor(id);
                if (actor == null)
                    return NotFound();
                else
                    return Ok(actor);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("{id}/TvShows")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Page<TvShowDTO>>> GetActorTvShows
            (int id,
             [FromQuery(Name = "$page")] int skip,
             [FromQuery(Name = "$pagesize")] int take,
             [FromQuery(Name = "$sortby")] string? sort)
        {
            if (id == 0) return BadRequest();
            take = take == 0 ? 20 : take;
            skip = skip == 0 ? 0 : (skip - 1) * take;
            try
            {
                var actors = await _actorServices.GetActorTvShows(id,skip, take, sort);
                if (actors.Results.Count() == 0)
                    return NotFound();
                else
                    return Ok(actors);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}