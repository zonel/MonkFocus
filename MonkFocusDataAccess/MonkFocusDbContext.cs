using Microsoft.EntityFrameworkCore;
using MonkFocusModels;


namespace MonkFocusDataAccess
{
    public class MonkFocusDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<WorkSession> WorkSessions { get; set; }
        public DbSet<WebsitesToBlock> WebsitesToBlock { get; set; }
        public DbSet<Quotes> Quotes { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

        private string cs = "Server=(localdb)\\\\MSSQLLocalDB;Database=RestaurantDb;Trusted_Connection=True;";


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer();
        }
    }
}
