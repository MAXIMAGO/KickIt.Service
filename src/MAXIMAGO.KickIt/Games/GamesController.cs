using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MAXIMAGO.KickIt.Games
{
    [Route("api/games")]
    public class GamesController : Controller
    {
        private GamesRepository gamesRepository;

        public GamesController(GamesRepository gamesRepository)
        {
            this.gamesRepository = gamesRepository;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<Game>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await gamesRepository.Get());
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<Game>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetGame(long id)
        {
            var game = await gamesRepository.Get(id);
            if (game == null)
            {
                return NotFound($"Game with the id '{id}' was not found.");
            }

            return Ok(game);
        }
    }
}
