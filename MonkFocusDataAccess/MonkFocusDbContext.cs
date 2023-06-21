using Microsoft.EntityFrameworkCore;
using MonkFocusApp.Models;
using MonkFocusModels;


namespace MonkFocusDataAccess
{
    public class MonkFocusDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }

        public DbSet<WorkSession> WorkSessions { get; set; }
        public DbSet<WebsitesToBlock> WebsitesToBlock { get; set; }

        protected readonly string CONNECTION_STRING = "Data Source=Monkfocus.db";

        public MonkFocusDbContext(DbContextOptions options) : base(options)
        {
        }

        public MonkFocusDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.Username).IsRequired();
                entity.Property(u => u.Name).IsRequired();
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.JoinDate).IsRequired();
                entity.Property(u => u.Points).IsRequired();
                entity.Property(u => u.BedTime).IsRequired();
            });

            #endregion

            #region UserTask

            modelBuilder.Entity<UserTask>(entity =>
            {
                entity.HasKey(t => t.TaskId);
                entity.Property(t => t.TaskName).IsRequired();

                entity.HasOne(t => t.User)
                    .WithMany(u => u.Task)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.Status)
                    .WithMany()
                    .HasForeignKey(t => t.StatusId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.Priority)
                    .WithMany()
                    .HasForeignKey(t => t.PriorityId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region WorkSession
            modelBuilder.Entity<WorkSession>(entity =>
            {
                entity.HasKey(ws => ws.WorkSessionId);

                entity.HasOne(ws => ws.User)
                    .WithMany(u => u.WorkSessions)
                    .HasForeignKey(ws => ws.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(ws => ws.StartTime).IsRequired();
                entity.Property(ws => ws.EndTime).IsRequired();
                entity.Property(ws => ws.Duration).IsRequired();
                entity.Property(ws => ws.Points).IsRequired();
            });
            #endregion

            #region Status
            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(s => s.StatusId);
                entity.Property(s => s.StatusName).IsRequired();
            });
            #endregion

            #region Priority
            modelBuilder.Entity<Priority>(entity =>
            {
                entity.HasKey(p => p.PriorityId);
                entity.Property(p => p.PriorityName).IsRequired();
            });
            #endregion

            #region WebsitesToBlock
            modelBuilder.Entity<WebsitesToBlock>(entity =>
            {
                entity.HasKey(wtb => wtb.WebsitesToBlockId);
                entity.Property(wtb => wtb.Domain).IsRequired();
            });
            #endregion

            #region Quote
            modelBuilder.Entity<Quote>(entity =>
            {
                entity.HasKey(q => q.QuoteId);
                entity.Property(q => q.FullQuote).IsRequired();
                entity.Property(q => q.Author).IsRequired();
            });
            #endregion

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=Monkfocus.db").EnableSensitiveDataLogging(true);
        }
    }
}
