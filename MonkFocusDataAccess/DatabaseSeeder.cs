using Microsoft.EntityFrameworkCore;
using MonkFocusApp.Models;
using MonkFocusModels;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
                    new Status { StatusId = 1, StatusName = "In Progress" },
                    new Status { StatusId = 2, StatusName = "Done" },
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

            //seeding test users
            if (!dbContext.WorkSessions.Any())
            {
                var worksessions = new List<WorkSession>()
                {

                    //new WorkSession()
                    //{
                    //     StartTime   = DateTime.Now.AddHours(-1),
                    //     EndTime     = DateTime.Now,
                    //     Duration    = DateTime.Now - DateTime.Now.AddHours(-1),
                    //     Points      = Math.Floor((DateTime.Now - DateTime.Now.AddHours(-1)).TotalMinutes).ToString(),
                    //     UserId      = 1,
                    //},
                    //new WorkSession()
                    //{
                    //    StartTime   = DateTime.Now.AddHours(-2),
                    //    EndTime     = DateTime.Now.AddHours(-1),
                    //    Duration    = DateTime.Now - DateTime.Now.AddHours(-1),
                    //    Points      = Math.Floor((DateTime.Now - DateTime.Now.AddHours(-1)).TotalMinutes).ToString(),
                    //    UserId      = 1,
                    //},
                    //new WorkSession()
                    //{
                    //    StartTime   = DateTime.Now.AddHours(-4),
                    //    EndTime     = DateTime.Now.AddHours(-3),
                    //    Duration    = DateTime.Now - DateTime.Now.AddHours(-1),
                    //    Points      = Math.Floor((DateTime.Now - DateTime.Now.AddHours(-1)).TotalMinutes).ToString(),
                    //    UserId      = 1,
                    //},
                    //new WorkSession()
                    //{
                    //    StartTime   = DateTime.Now.AddHours(-5),
                    //    EndTime     = DateTime.Now.AddHours(-4),
                    //    Duration    = DateTime.Now - DateTime.Now.AddHours(-1),
                    //    Points      = Math.Floor((DateTime.Now - DateTime.Now.AddHours(-1)).TotalMinutes).ToString(),
                    //    UserId      = 1,
                    //},
                    new WorkSession()
                    {
                         StartTime   = DateTime.Now.AddHours(-1),
                         EndTime     = DateTime.Now,
                         Duration    = DateTime.Now - DateTime.Now.AddHours(-1),
                         Points      = Math.Floor((DateTime.Now - DateTime.Now.AddHours(-1)).TotalMinutes).ToString(),
                         UserId      = 1,
                    },
                };
                await dbContext.WorkSessions.AddRangeAsync(worksessions);
            }

            //seeding quotes
            if (!dbContext.Quotes.Any())
            {
                var quotes = new List<Quote>();

                string json = await System.IO.File.ReadAllTextAsync("C:\\Users\\Bartek\\source\\MonkFocus\\MonkFocusDataAccess\\quotes.json"); //TODO: change to relative path

                JObject jsonObj = JObject.Parse(json);

                JArray quotesArray = (JArray)jsonObj["quotes"];

                foreach (var quoteObject in quotesArray)
                {
                    string quoteText = quoteObject["quote"].ToString();
                    string quoteAuthor = quoteObject["author"].ToString();

                    quotes.Add(new Quote()
                    {
                        Author = quoteAuthor,
                        FullQuote = quoteText
                    });
                }

                await dbContext.Quotes.AddRangeAsync(quotes);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
