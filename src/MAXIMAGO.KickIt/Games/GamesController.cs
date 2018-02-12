using Microsoft.AspNetCore.Mvc;
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
    }
}
