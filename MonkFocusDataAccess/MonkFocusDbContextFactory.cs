using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MonkFocusDataAccess
{
    public class MonkFocusDbContextFactory : IDesignTimeDbContextFactory<MonkFocusDbContext>
    {
        public MonkFocusDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MonkFocusDbContext>();
            optionsBuilder.UseSqlite("DataSource=Monkfocus.db");

            return new MonkFocusDbContext(optionsBuilder.Options);
        }

        public MonkFocusDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MonkFocusDbContext>();
            optionsBuilder.UseSqlite("DataSource=Monkfocus.db");

            return new MonkFocusDbContext(optionsBuilder.Options);
        }
    }
}
