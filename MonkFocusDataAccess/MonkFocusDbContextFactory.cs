using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MonkFocusDataAccess
{
    public class MonkFocusDbContextFactory
    {
        private readonly string CONNECTION_STRING = "Data Source=Monkfocus.db"; //TODO: move to config file

        public MonkFocusDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MonkFocusDbContext>();
            optionsBuilder.UseSqlite(CONNECTION_STRING);
            var context = new MonkFocusDbContext(optionsBuilder.Options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
