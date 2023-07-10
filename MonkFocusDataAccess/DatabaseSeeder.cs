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
                    new User { Username = "user1", Name = "John", Password = "password1", City = "Warsaw", Email = "john@example.com", JoinDate = new System.DateTime(2022, 5, 10), Points = 500, WakeUpTime = new System.TimeOnly(7, 30, 0), BedTime = new System.TimeOnly(23, 30, 0), WorkTimeGoal = new System.TimeSpan(7, 30, 0) },
                    new User { Username = "Sara", Name = "Sarah", Password = "abc123", City = "London", Email = "sarah@example.com", JoinDate = new System.DateTime(2021, 8, 15), Points = 1000, WakeUpTime = new System.TimeOnly(6, 30, 0), BedTime = new System.TimeOnly(22, 30, 0), WorkTimeGoal = new System.TimeSpan(8, 30, 0) },
                    new User { Username = "user3", Name = "Emily", Password = "qwerty", City = "New York", Email = "emily@example.com", JoinDate = new System.DateTime(2020, 3, 20), Points = 250, WakeUpTime = new System.TimeOnly(8, 0, 0), BedTime = new System.TimeOnly(23, 0, 0), WorkTimeGoal = new System.TimeSpan(6, 0, 0) },
                    new User { Username = "user4", Name = "Olivia", Password = "mike456", City = "Los Angeles", Email = "michael@example.com", JoinDate = new System.DateTime(2023, 1, 5), Points = 750, WakeUpTime = new System.TimeOnly(7, 0, 0), BedTime = new System.TimeOnly(22, 0, 0), WorkTimeGoal = new System.TimeSpan(7, 30, 0) },
                    new User { Username = "user5", Name = "Emma", Password = "emma789", City = "Paris", Email = "emma@example.com", JoinDate = new System.DateTime(2022, 9, 1), Points = 800, WakeUpTime = new System.TimeOnly(6, 0, 0), BedTime = new System.TimeOnly(23, 30, 0), WorkTimeGoal = new System.TimeSpan(8, 0, 0) },
                    new User { Username = "user6", Name = "Daniel", Password = "daniel321", City = "Berlin", Email = "daniel@example.com", JoinDate = new System.DateTime(2021, 4, 12), Points = 600, WakeUpTime = new System.TimeOnly(7, 30, 0), BedTime = new System.TimeOnly(22, 30, 0), WorkTimeGoal = new System.TimeSpan(6, 30, 0) },
                    new User { Username = "Michael", Name = "Michael", Password = "olivia555", City = "Sydney", Email = "olivia@example.com", JoinDate = new System.DateTime(2020, 11, 8), Points = 900, WakeUpTime = new System.TimeOnly(8, 0, 0), BedTime = new System.TimeOnly(23, 0, 0), WorkTimeGoal = new System.TimeSpan(7, 0, 0) },
                    new User { Username = "user8", Name = "Matthew", Password = "matt777", City = "Toronto", Email = "matthew@example.com", JoinDate = new System.DateTime(2023, 3, 17), Points = 400, WakeUpTime = new System.TimeOnly(6, 0, 0), BedTime = new System.TimeOnly(22, 0, 0), WorkTimeGoal = new System.TimeSpan(8, 30, 0) },
                    new User { Username = "user9", Name = "Ava", Password = "ava123", City = "Melbourne", Email = "ava@example.com", JoinDate = new System.DateTime(2022, 7, 25), Points = 350, WakeUpTime = new System.TimeOnly(7, 0, 0), BedTime = new System.TimeOnly(23, 0, 0), WorkTimeGoal = new System.TimeSpan(6, 30, 0) },
                    new User { Username = "user10", Name = "Liam", Password = "liam999", City = "Vancouver", Email = "liam@example.com", JoinDate = new System.DateTime(2021, 2, 3), Points = 700, WakeUpTime = new System.TimeOnly(8, 30, 0), BedTime = new System.TimeOnly(22, 30, 0), WorkTimeGoal = new System.TimeSpan(7, 0, 0) }
                };
                await dbContext.Users.AddRangeAsync(testusers);
            }

            //seeding test tasks
            if (!dbContext.Tasks.Any())
            {
                var testTasks = new List<UserTask>()
                {
                    new UserTask { UserId = 1, TaskName = "Go to Gym", PriorityId = 3, StatusId = 1 },
    new UserTask { UserId = 1, TaskName = "Finish today's work", PriorityId = 3, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Fix Bug #123", PriorityId = 3, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Morning Meditation", PriorityId = 1, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Take Supplements", PriorityId = 1, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Daily Journal Entry", PriorityId = 2, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Groceries", PriorityId = 1, StatusId = 2 },
    new UserTask { UserId = 2, TaskName = "Go for a run", PriorityId = 1, StatusId = 1 },
    new UserTask { UserId = 2, TaskName = "Read 10 pages of a book", PriorityId = 3, StatusId = 1 },
    new UserTask { UserId = 2, TaskName = "Do Stretching", PriorityId = 3, StatusId = 1 },
    new UserTask { UserId = 1, TaskName = "Attend Meeting", PriorityId = 2, StatusId = 1 },
    new UserTask { UserId = 1, TaskName = "Write Report", PriorityId = 2, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Prepare Presentation", PriorityId = 1, StatusId = 1 },
    new UserTask { UserId = 1, TaskName = "Send Emails", PriorityId = 2, StatusId = 1 },
    new UserTask { UserId = 1, TaskName = "Review Documents", PriorityId = 1, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Attend Training", PriorityId = 3, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Submit Expense Report", PriorityId = 3, StatusId = 1 },
    new UserTask { UserId = 2, TaskName = "Complete Coding Assignment", PriorityId = 1, StatusId = 2 },
    new UserTask { UserId = 2, TaskName = "Prepare for Presentation", PriorityId = 2, StatusId = 1 },
    new UserTask { UserId = 2, TaskName = "Attend Team Meeting", PriorityId = 1, StatusId = 2 },
    new UserTask { UserId = 2, TaskName = "Review Project Plan", PriorityId = 3, StatusId = 1 },
    new UserTask { UserId = 2, TaskName = "Code Refactoring", PriorityId = 2, StatusId = 1 },
    new UserTask { UserId = 2, TaskName = "Test New Feature", PriorityId = 3, StatusId = 2 },
    new UserTask { UserId = 2, TaskName = "Update Documentation", PriorityId = 1, StatusId = 1 },
    new UserTask { UserId = 2, TaskName = "Attend Webinar", PriorityId = 3, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Review Code Pull Request", PriorityId = 2, StatusId = 1 },
    new UserTask { UserId = 1, TaskName = "Create Test Cases", PriorityId = 1, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Deploy Application", PriorityId = 3, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Conduct User Testing", PriorityId = 2, StatusId = 1 },
    new UserTask { UserId = 1, TaskName = "Prepare Project Documentation", PriorityId = 1, StatusId = 1 },
    new UserTask { UserId = 1, TaskName = "Attend Client Meeting", PriorityId = 3, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Review System Architecture", PriorityId = 2, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Write Test Scripts", PriorityId = 1, StatusId = 2 },
    new UserTask { UserId = 1, TaskName = "Prepare Project Budget", PriorityId = 3, StatusId = 1 },
    new UserTask { UserId = 2, TaskName = "Attend Code Review", PriorityId = 2, StatusId = 1 }
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
                        StartTime = DateTime.Now.AddMinutes(-45),
                        EndTime = DateTime.Now,
                        Duration = DateTime.Now - DateTime.Now.AddMinutes(-45),
                        Points = Math.Floor((DateTime.Now - DateTime.Now.AddMinutes(-45)).TotalMinutes).ToString(),
                        UserId = 1
                    },
                    new WorkSession()
                    {
                        StartTime = DateTime.Now.AddHours(-2).AddMinutes(-30),
                        EndTime = DateTime.Now.AddHours(-1).AddMinutes(-30),
                        Duration = DateTime.Now.AddHours(-1).AddMinutes(-30) - DateTime.Now.AddHours(-2).AddMinutes(-30),
                        Points = Math.Floor((DateTime.Now.AddHours(-1).AddMinutes(-30) - DateTime.Now.AddHours(-2).AddMinutes(-30)).TotalMinutes).ToString(),
                        UserId = 2
                    },
                    new WorkSession()
                    {
                        StartTime = DateTime.Now.AddMinutes(-20),
                        EndTime = DateTime.Now,
                        Duration = DateTime.Now - DateTime.Now.AddMinutes(-20),
                        Points = Math.Floor((DateTime.Now - DateTime.Now.AddMinutes(-20)).TotalMinutes).ToString(),
                        UserId = 1
                    },
                    new WorkSession()
                    {
                        StartTime = DateTime.Now.AddHours(-3).AddMinutes(-15),
                        EndTime = DateTime.Now.AddHours(-2).AddMinutes(-30),
                        Duration = DateTime.Now.AddHours(-2).AddMinutes(-30) - DateTime.Now.AddHours(-3).AddMinutes(-15),
                        Points = Math.Floor((DateTime.Now.AddHours(-2).AddMinutes(-30) - DateTime.Now.AddHours(-3).AddMinutes(-15)).TotalMinutes).ToString(),
                        UserId = 2
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
