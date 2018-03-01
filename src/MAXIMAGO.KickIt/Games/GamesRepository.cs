using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAXIMAGO.KickIt.Games
{
    /// <summary>
    /// The interface describes the access to the games
    /// </summary>
    public interface GamesRepository
    {
        /// <summary>
        /// Gets all games
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Game>> Get();

        /// <summary>
        /// Get a game by the games identifier
        /// </summary>
        /// <param name="id">identifier of the game</param>
        /// <returns></returns>
        Task<Game> Get(long id);

        /// <summary>
        /// Checks, if a Game id already is exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Exists(long id);

        /// <summary>
        /// Saves or updates a Game
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        Task<Game> Save(Game game);

        /// <summary>
        /// Deletes a game
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        Task<bool> Delete(Game game);
    }
}
