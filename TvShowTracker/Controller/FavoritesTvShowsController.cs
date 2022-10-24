using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TvShowTracker.Interfaces;
using TvShowTracker.Model;
namespace TvShowTracker.Controller
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoritesTvShowsController : ControllerBase
    {
        private readonly IFavoriteServices _favoriteServices;
        public FavoritesTvShowsController(IFavoriteServices favoriteServices)
        {
            _favoriteServices = favoriteServices;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetFavoriteTvShow(
             [FromQuery(Name = "$page")] int skip,
             [FromQuery(Name = "$pagesize")] int take,
             [FromQuery(Name = "$sortby")] string? sort)
        {
            int id = (int)Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            take = take == 0 ? 20 : take;
            skip = skip == 0 ? 0 : (skip - 1) * take;
            try
            {
                var favoriteTvShow = await _favoriteServices.GetFavoriteTvShows(id, skip, take, sort);
                if (favoriteTvShow.Results.Count() == 0)
                    return NotFound();
                else
                    return Ok(favoriteTvShow);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostFavoriteTvShow(FavoriteTvShowDTO tvShow)
        {
            if (tvShow.TvShowId == 0) return BadRequest();
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            try
            {
                await _favoriteServices.AddFavoriteIfDoesntExist(id, tvShow.TvShowId);
                return CreatedAtAction("GetFavoriteTvShow", null);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFavoriteTvShow(int id)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (id == 0) return BadRequest();
            try
            {
                var worked = await _favoriteServices.DeleteFavorite(userId, id);
                if (worked) return NoContent();
                else return NotFound();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}