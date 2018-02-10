using MAXIMAGO.KickIt.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAXIMAGO.KickIt.InMemoryStorage.Players
{
    public class InMemoryPlayerRepository : PlayerRepository
    {
        private static List<Player> players = new List<Player>();
        private static long currentId = 0;
        private object lockObj = new object();

        public async Task<bool> Delete(Player player)
        {
            bool result;
            lock(lockObj)
            {
                result = players.RemoveAll(x => x.Id == player.Id) > 0;
            }
            return await Task.FromResult(result);
        }

        public async Task<bool> Exists(long id)
        {
            bool result;
            lock(lockObj)
            {
                result = players.Any(x => x.Id == id);
            }

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Player>> Get()
        {
            var resultList = new List<Player>();
            lock(lockObj)
            {
                resultList.AddRange(players);
            }

            return await Task.FromResult(resultList);
        }

        public async Task<Player> Get(long id)
        {
            Player result;
            lock(lockObj)
            {
                result = players.FirstOrDefault(x => x.Id == id);
            }

            return await Task.FromResult(result);
        }

        public async Task<Player> Save(Player player)
        {
            Player result;
            lock (lockObj)
            {
                if (player.Id == 0)
                {
                    player.Id = ++currentId;
                }

                var savedPlayer = players.FirstOrDefault(x => x.Id == player.Id);
                if (savedPlayer == null)
                {
                    // new player
                    players.Add(player);
                    result = player;
                } 
                else
                {
                    // existing player
                    UpdatePlayerInfo(savedPlayer, player);
                    result = savedPlayer;
                }
            }
            return await Task.FromResult(result);
        }

        private void UpdatePlayerInfo(Player savedPlayer, Player player)
        {
            savedPlayer.FirstName = player.FirstName;
            savedPlayer.LastName = player.LastName;
            savedPlayer.EmailAddress = player.EmailAddress;
            savedPlayer.Gender = player.Gender;
        }
    }
}
