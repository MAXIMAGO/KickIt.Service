using MAXIMAGO.KickIt.Games;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAXIMAGO.KickIt.InMemoryStorage.Games
{
    public sealed class InMemoryGamesRepository : GamesRepository
    {
        private static List<Game> games = new List<Game>();
        private object lockObj = new object();

        public async Task<IEnumerable<Game>> Get()
        {
            IEnumerable<Game> result;
            lock (lockObj)
            {
                result = games.ToArray();
            }
            return await Task.FromResult(result);
        }

        public async Task<Game> Get(long id)
        {
            Game result;
            lock(lockObj)
            {
                result = games.FirstOrDefault(game => game.Id == id);
            }
            return await Task.FromResult(result);
        }
    }
}
