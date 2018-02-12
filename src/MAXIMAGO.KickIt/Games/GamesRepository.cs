using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAXIMAGO.KickIt.Games
{
    public interface GamesRepository
    {
        Task<IEnumerable<Game>> Get();

        Task<Game> Get(string id);
    }
}
