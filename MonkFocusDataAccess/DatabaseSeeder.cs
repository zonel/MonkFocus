using Microsoft.EntityFrameworkCore;
using MonkFocusApp.Models;
using MonkFocusModels;
using System.Threading.Tasks;

namespace MonkFocusDataAccess
{
    public class DatabaseSeeder
    {
        private MonkFocusDbContext dbContext;

        public DatabaseSeeder(MonkFocusDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async void SeedData()
        {
            var content = await dbContext.Statuses.ToListAsync();
            var quotess = await dbContext.Quotes.ToListAsync();


            if (!dbContext.Statuses.Any())
            {
                var statuses = new List<Status>()
                {
                    new Status { StatusId = 1, StatusName = "Working on" },
                    new Status { StatusId = 2, StatusName = "Done" },
                    new Status { StatusId = 3, StatusName = "On Hold" },
                };
                await dbContext.Statuses.AddRangeAsync(statuses);

            }

            if (!dbContext.Priorities.Any())
            {
                var priorities = new List<Priority>()
                {
                    new Priority { PriorityId = 1,PriorityName = "Low" },
                    new Priority { PriorityId = 2,PriorityName = "Medium" },
                    new Priority { PriorityId = 3,PriorityName = "Crucial" },
                };
                await dbContext.Priorities.AddRangeAsync(priorities);
            }

            //seeding test users
            if (!dbContext.Users.Any())
            {
                var testusers = new List<User>()
                {
                    //new User
                    //{
                    //    Username = "root",
                    //    Name = "Robert",
                    //    Password = "toor",
                    //    City = "Kraków",
                    //    Email = "root@root.com",
                    //    JoinDate = new System.DateTime(2019, 1, 1),
                    //    Points = 1337,
                    //    WakeUpTime = new System.TimeOnly(8, 0, 0),
                    //    BedTime = new System.TimeOnly(23, 0, 0),
                    //    WorkTimeGoal = new System.TimeSpan(8, 0, 0),
                    //},
                    new User
                    {
                        Username = "adam",
                        Name = "Adam",
                        Password = "adam321",
                        City = "Katowice",
                        Email = "adam@gmail.com",
                        JoinDate = new System.DateTime(2023, 2, 25),
                        Points = 37,
                        WakeUpTime = new System.TimeOnly(6, 0, 0),
                        BedTime = new System.TimeOnly(22, 0, 0),
                        WorkTimeGoal = new System.TimeSpan(6, 0, 0),
                    },
                };
                await dbContext.Users.AddRangeAsync(testusers);
            }

            //seeding test tasks
            if (!dbContext.Tasks.Any())
            {
                var testTasks = new List<UserTask>()
                {
                    //new UserTask
                    //{
                    //    UserId = 1,
                    //    TaskName = "Go to Gym",
                    //    PriorityId = 3,
                    //    StatusId = 1,
                    //},
                    //new UserTask
                    //{
                    //    UserId = 1,
                    //    TaskName = "Finish today's work",
                    //    PriorityId = 3,
                    //    StatusId = 3,
                    //},
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Fix Bug #123",
                        PriorityId = 3,
                        StatusId = 3,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Morning Meditation",
                        PriorityId = 1,
                        StatusId = 2,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Take Supplements",
                        PriorityId = 1,
                        StatusId = 2,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Daily Journal Entry",
                        PriorityId = 2,
                        StatusId = 2,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Groceries",
                        PriorityId = 1,
                        StatusId = 3,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Go for a run",
                        PriorityId = 1,
                        StatusId = 1,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Read 10 pages of a book",
                        PriorityId = 3,
                        StatusId = 3,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Do Stretching",
                        PriorityId = 3,
                        StatusId = 3,
                    },
                };
                await dbContext.Tasks.AddRangeAsync(testTasks);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
