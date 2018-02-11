using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MAXIMAGO.KickIt.Players
{
    [Route("api/players")]
    public sealed class PlayersController : Controller
    {
        private PlayerRepository playerRepository;

        public PlayersController(PlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository 
                ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<Player>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var players = await playerRepository.Get();
            return Ok(players);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(Player), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreatePlayer([FromBody]Player player)
        {
            if (player == null)
            {
                return BadRequest("Player missing");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await playerRepository.Exists(player.Id))
            {
                return StatusCode(
                    (int)HttpStatusCode.Conflict, 
                    $"Player with the id '{player.Id}' already exists.");
            }

            var savedPlayer = await playerRepository.Save(player);
            return Created(Url.Link("GetPlayer", new { id = player.Id }), savedPlayer);
        }
        
        [HttpGet]
        [Route("{id}", Name = "GetPlayer")]
        [ProducesResponseType(typeof(Player), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPlayer(long id)
        {
            var player = await playerRepository.Get(id);
            if (player == null)
            {
                return NotFound($"Player with id '{id}' not found");
            }

            return Ok(player);
        }
        
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(Player), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdatePlayer([FromRoute]long id, [FromBody]Player player)
        {
            if (player == null)
            {
                return BadRequest("Player missing");
            }

            if (player.Id != 0 && player.Id != id)
            {
                return StatusCode(
                    (int)HttpStatusCode.Conflict, 
                    "Provided id from url does not match to provided player");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await playerRepository.Exists(id))
            {
                return NotFound($"Player with id '{id}' was not found");
            }

            var savedPlayer = await playerRepository.Save(player);
            return Ok(savedPlayer);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotModified)]
        public async Task<IActionResult> DeletePlayer(long id)
        {
            var player = await playerRepository.Get(id);
            if (player == null)
            {
                return NotFound($"Player with id '{id}' not found");
            }

            var deleted = await playerRepository.Delete(player);
            return (deleted ? NoContent() : StatusCode((int)HttpStatusCode.NotModified));
        }
    }
}
