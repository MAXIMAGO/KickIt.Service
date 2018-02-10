using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAXIMAGO.KickIt.Players
{
    /// <summary>
    /// Access to player information
    /// </summary>
    public interface PlayerRepository
    {
        /// <summary>
        /// Get all players information
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Player>> Get();

        /// <summary>
        /// Gets a single player
        /// </summary>
        /// <param name="id">Identity of the player</param>
        /// <returns></returns>
        Task<Player> Get(long id);

        /// <summary>
        /// Saves the player information
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        Task<Player> Save(Player player);

        /// <summary>
        /// Deletes the player
        /// </summary>
        /// <param name="player"></param>
        /// <returns>Status of deletion. TRUE means deleted succesfully, FALSE error</returns>
        Task<bool> Delete(Player player);

        /// <summary>
        /// Checks if a player with a certain id already exists
        /// </summary>
        /// <param name="id">Identity of the player</param>
        /// <returns></returns>
        Task<bool> Exists(long id);
    }
}
