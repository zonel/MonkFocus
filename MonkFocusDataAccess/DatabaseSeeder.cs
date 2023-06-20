using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkFocusApp.Models;
using MonkFocusModels;

namespace MonkFocusDataAccess
{
    public class DatabaseSeeder
    {
        private MonkFocusDbContext dbContext;

        public DatabaseSeeder(MonkFocusDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void SeedData()
        {
            if (!dbContext.Statuses.Any())
            {
                var statuses = new List<Status>()
                {
                    new Status { StatusId = 1, StatusName = "Working on" },
                    new Status { StatusId = 2, StatusName = "Done" },
                    new Status { StatusId = 3, StatusName = "On Hold" },
                };
                dbContext.Statuses.AddRange(statuses);

            }

            if (!dbContext.Priorities.Any())
            {
                var priorities = new List<Priority>()
                {
                    new Priority { PriorityId = 1, PriorityName = "Low" },
                    new Priority { PriorityId = 2, PriorityName = "Medium" },
                    new Priority { PriorityId = 3, PriorityName = "Crucial" },
                };
                dbContext.Priorities.AddRange(priorities);
            }

            dbContext.SaveChanges();
        }
    }
}
