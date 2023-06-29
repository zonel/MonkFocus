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
            //seeding Statuses
            if (!dbContext.Statuses.Any())
            {
                var statuses = new List<Status>()
                {
                    new Status { StatusId = 1, StatusName = "In Progress" },
                    new Status { StatusId = 2, StatusName = "Done" },
                };
                await dbContext.Statuses.AddRangeAsync(statuses);
            }

            //seeding Priorities
            if (!dbContext.Priorities.Any())
            {
                var priorities = new List<Priority>()
                {
                    new Priority { PriorityId = 1, PriorityName = "Low" },
                    new Priority { PriorityId = 2, PriorityName = "Medium" },
                    new Priority { PriorityId = 3, PriorityName = "Crucial" },
                };
                await dbContext.Priorities.AddRangeAsync(priorities);
            }

            //seeding test users
            if (!dbContext.Users.Any())
            {
                var testusers = new List<User>()
                {
                    new User
                    {
                        Username = "root",
                        Name = "Robert",
                        Password = "toor",
                        City = "Kraków",
                        Email = "root@root.com",
                        JoinDate = new System.DateTime(2019, 1, 1),
                        Points = 1337,
                        WakeUpTime = new System.TimeOnly(8, 0, 0),
                        BedTime = new System.TimeOnly(23, 0, 0),
                        WorkTimeGoal = new System.TimeSpan(8, 0, 0),
                    },
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
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Go to Gym",
                        PriorityId = 3,
                        StatusId = 1,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Finish today's work",
                        PriorityId = 3,
                        StatusId = 2,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Fix Bug #123",
                        PriorityId = 3,
                        StatusId = 2,
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
                        StatusId = 2,
                    },
                    new UserTask
                    {
                        UserId = 2,
                        TaskName = "Go for a run",
                        PriorityId = 1,
                        StatusId = 1,
                    },
                    new UserTask
                    {
                        UserId = 2,
                        TaskName = "Read 10 pages of a book",
                        PriorityId = 3,
                        StatusId = 1,
                    },
                    new UserTask
                    {
                        UserId = 2,
                        TaskName = "Do Stretching",
                        PriorityId = 3,
                        StatusId = 1,
                    },
                };
                await dbContext.Tasks.AddRangeAsync(testTasks);
            }

            //seeding work sessions
            if (!dbContext.WorkSessions.Any())
            {
                var worksessions = new List<WorkSession>()
                {
                    new WorkSession()
                    {
                        StartTime = DateTime.Now.AddHours(-1),
                        EndTime = DateTime.Now,
                        Duration = DateTime.Now - DateTime.Now.AddHours(-1),
                        Points = Math.Floor((DateTime.Now - DateTime.Now.AddHours(-1)).TotalMinutes).ToString(),
                        UserId = 1,
                    },
                    new WorkSession()
                    {
                        StartTime = DateTime.Now.AddHours(-2),
                        EndTime = DateTime.Now.AddHours(-1),
                        Duration = DateTime.Now - DateTime.Now.AddHours(-1),
                        Points = Math.Floor((DateTime.Now - DateTime.Now.AddHours(-1)).TotalMinutes).ToString(),
                        UserId = 1,
                    },
                    new WorkSession()
                    {
                        StartTime = DateTime.Now.AddHours(-4),
                        EndTime = DateTime.Now.AddHours(-3),
                        Duration = DateTime.Now - DateTime.Now.AddHours(-1),
                        Points = Math.Floor((DateTime.Now - DateTime.Now.AddHours(-1)).TotalMinutes).ToString(),
                        UserId = 1,
                    },
                    new WorkSession()
                    {
                        StartTime = DateTime.Now.AddHours(-5),
                        EndTime = DateTime.Now.AddHours(-4),
                        Duration = DateTime.Now - DateTime.Now.AddHours(-1),
                        Points = Math.Floor((DateTime.Now - DateTime.Now.AddHours(-1)).TotalMinutes).ToString(),
                        UserId = 2,
                    },
                    new WorkSession()
                    {
                        StartTime = DateTime.Now.AddHours(-1),
                        EndTime = DateTime.Now,
                        Duration = DateTime.Now - DateTime.Now.AddHours(-1),
                        Points = Math.Floor((DateTime.Now - DateTime.Now.AddHours(-1)).TotalMinutes).ToString(),
                        UserId = 2,
                    },
                };
                await dbContext.WorkSessions.AddRangeAsync(worksessions);
            }

            //seeding quotes
            if (!dbContext.Quotes.Any())
            {
                var Quotes = new List<Quote>
                {
                    new Quote
                    {
                        FullQuote = "Life isn’t about getting and having, it’s about giving and being.",
                        Author = "Kevin Kruse"
                    },
                    new Quote
                    {
                        FullQuote = "Whatever the mind of man can conceive and believe, it can achieve.",
                        Author = "Napoleon Hill"
                    },
                    new Quote
                    {
                        FullQuote = "Strive not to be a success, but rather to be of value.",
                        Author = "Albert Einstein"
                    },
                    new Quote
                    {
                        FullQuote =
                            "Two roads diverged in a wood, and I—I took the one less traveled by, And that has made all the difference.",
                        Author = "Robert Frost"
                    },
                    new Quote
                    {
                        FullQuote = "I attribute my success to this: I never gave or took any excuse.",
                        Author = "Florence Nightingale"
                    },
                    new Quote
                    {
                        FullQuote = "You miss 100% of the shots you don’t take.",
                        Author = "Wayne Gretzky"
                    },
                    new Quote
                    {
                        FullQuote =
                            "I’ve missed more than 9000 shots in my career. I’ve lost almost 300 games. 26 times I’ve been trusted to take the game winning shot and missed. I’ve failed over and over and over again in my life. And that is why I succeed.",
                        Author = "Michael Jordan"
                    },
                    new Quote
                    {
                        FullQuote = "The most difficult thing is the decision to act, the rest is merely tenacity.",
                        Author = "Amelia Earhart"
                    },
                    new Quote
                    {
                        FullQuote = "Every strike brings me closer to the next home run.",
                        Author = "Babe Ruth"
                    },
                    new Quote
                    {
                        FullQuote = "Definiteness of purpose is the starting point of all achievement.",
                        Author = "W. Clement Stone"
                    },
                    new Quote
                    {
                        FullQuote = "We must balance conspicuous consumption with conscious capitalism.",
                        Author = "Kevin Kruse"
                    },
                    new Quote
                    {
                        FullQuote = "Life is what happens to you while you’re busy making other plans.",
                        Author = "John Lennon"
                    },
                    new Quote
                    {
                        FullQuote = "We become what we think about.",
                        Author = "Earl Nightingale"
                    },
                    new Quote
                    {
                        FullQuote =
                            "Twenty years from now you will be more disappointed by the things that you didn’t do than by the ones you did do, so throw off the bowlines, sail away from safe harbor, catch the trade winds in your sails. Explore, Dream, Discover.",
                        Author = "Mark Twain"
                    },
                    new Quote
                    {
                        FullQuote = "Life is 10% what happens to me and 90% of how I react to it.",
                        Author = "Charles Swindoll"
                    },
                    new Quote
                    {
                        FullQuote =
                            "The most common way people give up their power is by thinking they don’t have any.",
                        Author = "Alice Walker"
                    },
                    new Quote
                    {
                        FullQuote = "The mind is everything. What you think you become.",
                        Author = "Buddha"
                    },
                    new Quote
                    {
                        FullQuote = "The best time to plant a tree was 20 years ago. The second best time is now.",
                        Author = "Chinese Proverb"
                    },
                    new Quote
                    {
                        FullQuote = "An unexamined life is not worth living.",
                        Author = "Socrates"
                    },
                    new Quote
                    {
                        FullQuote = "Eighty percent of success is showing up.",
                        Author = "Woody Allen"
                    },
                    new Quote
                    {
                        FullQuote = "Your time is limited, so don’t waste it living someone else’s life.",
                        Author = "Steve Jobs"
                    },
                    new Quote
                    {
                        FullQuote = "Winning isn’t everything, but wanting to win is.",
                        Author = "Vince Lombardi"
                    },
                    new Quote
                    {
                        FullQuote = "I am not a product of my circumstances. I am a product of my decisions.",
                        Author = "Stephen Covey"
                    },
                    new Quote
                    {
                        FullQuote =
                            "Every child is an artist. The problem is how to remain an artist once he grows up.",
                        Author = "Pablo Picasso"
                    },
                    new Quote
                    {
                        FullQuote =
                            "You can never cross the ocean until you have the courage to lose sight of the shore.",
                        Author = "Christopher Columbus"
                    },
                    new Quote
                    {
                        FullQuote =
                            "I’ve learned that people will forget what you said, people will forget what you did, but people will never forget how you made them feel.",
                        Author = "Maya Angelou"
                    },
                    new Quote
                    {
                        FullQuote = "Either you run the day, or the day runs you.",
                        Author = "Jim Rohn"
                    },
                    new Quote
                    {
                        FullQuote = "Whether you think you can or you think you can’t, you’re right.",
                        Author = "Henry Ford"
                    },
                    new Quote
                    {
                        FullQuote =
                            "The two most important days in your life are the day you are born and the day you find out why.",
                        Author = "Mark Twain"
                    },
                    new Quote
                    {
                        FullQuote =
                            "Whatever you can do, or dream you can, begin it. Boldness has genius, power and magic in it.",
                        Author = "Johann Wolfgang von Goethe"
                    },
                    new Quote
                    {
                        FullQuote = "The best revenge is massive success.",
                        Author = "Frank Sinatra"
                    }
                };
                await dbContext.Quotes.AddRangeAsync(Quotes);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
