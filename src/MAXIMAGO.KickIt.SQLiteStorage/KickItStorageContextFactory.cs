using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAXIMAGO.KickIt.SQLiteStorage
{
    class KickItStorageContextFactory : IDesignTimeDbContextFactory<KickItStorageContext>
    {
        public KickItStorageContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<KickItStorageContext>();
            optionsBuilder.UseSqlite("Data Source=MAXIMAGO.KickIt.db;");
            return new KickItStorageContext(optionsBuilder.Options);
        }
    }
}
