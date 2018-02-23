using MAXIMAGO.KickIt.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAXIMAGO.KickIt.InMemoryStorage.Games
{
    public sealed class InMemoryGamesRepository : GamesRepository
    {
        private static List<Game> games = new List<Game>();
        private object lockObj = new object();
        private static long currentId = 0;

        public async Task<bool> Exists(long id)
        {
            bool result;
            lock (lockObj)
            {
                result = games.Any(x => x.Id == id);
            }
            return await Task.FromResult(result);
        }

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

        public async Task<Game> Save(Game game)
        {
            Game result;
            lock (lockObj)
            {
                if (game.Id == 0)
                {
                    game.Id = ++currentId;
                }

                var savedGame = games.FirstOrDefault(x => x.Id == game.Id);
                if (savedGame == null)
                {
                    // new game
                    games.Add(game);
                    result = game;
                }
                else
                {
                    // existing game
                    UpdateGameInfo(savedGame, game);
                    result = savedGame;
                }
            }
            return await Task.FromResult(result);
        }

        private void UpdateGameInfo(Game savedGame, Game game)
        {
            savedGame.Guest = game.Guest;
            savedGame.Home = game.Home;
            savedGame.Sets = game.Sets;
        }

        public async Task<bool> Delete(Game game)
        {
            bool result;
            lock (lockObj)
            {
                result = games.RemoveAll(x => x.Id == game.Id) > 0;
            }
            return await Task.FromResult(result);
        }
    }
}
