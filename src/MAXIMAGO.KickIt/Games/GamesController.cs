using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MAXIMAGO.KickIt.Games
{
    //[Route("api/games")]
    //public class GamesController : Controller
    //{
    //    private GamesRepository gamesRepository;

    //    public GamesController(GamesRepository gamesRepository)
    //    {
    //        this.gamesRepository = gamesRepository;
    //    }

    //    [HttpGet]
    //    [Route("")]
    //    [ProducesResponseType(typeof(IEnumerable<Game>), (int)HttpStatusCode.OK)]
    //    public async Task<IActionResult> Get()
    //    {
    //        return Ok(await gamesRepository.Get());
    //    }

    //    [HttpPost]
    //    [Route("")]
    //    [ProducesResponseType(typeof(Game), (int)HttpStatusCode.OK)]
    //    [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
    //    public async Task<IActionResult> CreateGame([FromBody]Game game)
    //    {
    //        if (game == null)
    //        {
    //            return BadRequest("Game is missing");
    //        }

    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        if (game.Id != default(long) 
    //            && await gamesRepository.Exists(game.Id))
    //        {
    //            return StatusCode((int)HttpStatusCode.Conflict, $"A Game with the id {game.Id}' already exists.");
    //        }

    //        var savedGame = await gamesRepository.Save(game);
    //        return Created(Url.Link("GetGame", new { id = savedGame.Id }), savedGame);
    //    }

    //    [HttpGet]
    //    [Route("{id:guid}", Name = "GetGame")]
    //    [ProducesResponseType(typeof(IEnumerable<Game>), (int)HttpStatusCode.OK)]
    //    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    //    public async Task<IActionResult> GetGame(long id)
    //    {
    //        var game = await gamesRepository.Get(id);
    //        if (game == null)
    //        {
    //            return NotFound($"Game with the id '{id}' was not found.");
    //        }

    //        return Ok(game);
    //    }

    //    [HttpPut]
    //    [Route("{id}")]
    //    [ProducesResponseType(typeof(Game), (int)HttpStatusCode.OK)]
    //    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    //    [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
    //    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    //    public async Task<IActionResult> UpdateGame([FromRoute]long id, [FromBody]Game game)
    //    {
    //        if (game == null)
    //        {
    //            return BadRequest("Game missing");
    //        }

    //        if (game.Id != 0 && game.Id != id)
    //        {
    //            return StatusCode(
    //                (int)HttpStatusCode.Conflict,
    //                "Provided id from url does not match to provided player");
    //        }

    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        if (!await gamesRepository.Exists(id))
    //        {
    //            return NotFound($"Game with id '{id}' was not found");
    //        }

    //        var savedPlayer = await gamesRepository.Save(game);
    //        return Ok(savedPlayer);
    //    }

    //    [HttpDelete]
    //    [Route("{id}")]
    //    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
    //    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
    //    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotModified)]
    //    public async Task<IActionResult> DeleteGame(long id)
    //    {
    //        var game = await gamesRepository.Get(id);
    //        if (game == null)
    //        {
    //            return NotFound($"Game with id '{id}' not found");
    //        }

    //        var deleted = await gamesRepository.Delete(game);
    //        return (deleted ? NoContent() : StatusCode((int)HttpStatusCode.NotModified));
    //    }
    //}
}
