using MAXIMAGO.KickIt.Games;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MAXIMAGO.KickIt.InMemoryStorage.Games
{
    public sealed class InMemoryGamesRepository : GamesRepository
    {
        public Task<IEnumerable<Game>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Game> Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}
