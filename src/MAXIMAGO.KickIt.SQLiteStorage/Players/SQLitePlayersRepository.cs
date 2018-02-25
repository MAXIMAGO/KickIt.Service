using MAXIMAGO.KickIt.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAXIMAGO.KickIt.SQLiteStorage.Players
{
    public sealed class SQLitePlayersRepository : PlayerRepository, IDisposable
    {
        private KickItStorageContext context;

        public SQLitePlayersRepository(KickItStorageContext context)
        {
            this.context = context;
        }

        public async Task<bool> Delete(Player player)
        {
            context.Players.Attach(player);
            context.Players.Remove(player);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Exists(long id)
        {
            return await context.Players.ToAsyncEnumerable().Any(x => x.Id == id);
        }

        public async Task<IEnumerable<Player>> Get()
        {
            return await context.Players.ToAsyncEnumerable().ToArray();
        }

        public async Task<Player> Get(long id)
        {
            return await context.Players.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Player> Save(Player player)
        {
            Player playerEntry;
            if (await Exists(player.Id))
            {
                var savedPlayer = await context.Players.SingleOrDefaultAsync(x => x.Id == player.Id);
                savedPlayer.UpdatePlayer(player);
                await context.SaveChangesAsync();
                playerEntry = savedPlayer;
            }
            else
            {
                playerEntry = context.Players.Add(player).Entity;
                await context.SaveChangesAsync();
            }

            return playerEntry;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    this.context.Dispose();
                }
                
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
