using MAXIMAGO.KickIt.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MAXIMAGO.KickIt.SQLiteStorage
{
    public static class DbContextExtensions
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this KickItStorageContext context)
        {
            if (!context.Players.Any())
            {
                var jsonFile = Path.Combine("seed", "players.json");
                var players = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(jsonFile));
                context.Players.AddRange(players);
                context.SaveChanges();
            }
        }
    }
}
