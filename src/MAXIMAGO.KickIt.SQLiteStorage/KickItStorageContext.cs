using MAXIMAGO.KickIt.Games;
using MAXIMAGO.KickIt.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MAXIMAGO.KickIt.SQLiteStorage
{
    public class KickItStorageContext : DbContext
    {
        public KickItStorageContext(DbContextOptions<KickItStorageContext> contextOptions)
            : base(contextOptions)
        {
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Game> Games { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PlayerTeam>()
                .HasKey(t => new { t.PlayerId, t.TeamId });
        }
    }
}
