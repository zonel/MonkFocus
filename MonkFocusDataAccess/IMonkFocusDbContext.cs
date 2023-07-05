using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MonkFocusApp.Models;
using MonkFocusModels;

namespace MonkFocusDataAccess
{
    public interface IMonkFocusDbContext
    {
        #region Tables
         DbSet<User> Users { get; set; }
         DbSet<UserTask> Tasks { get; set; }
         DbSet<Quote> Quotes { get; set; }
         DbSet<Status> Statuses { get; set; }
         DbSet<Priority> Priorities { get; set; }
         DbSet<WorkSession> WorkSessions { get; set; }
        #endregion

        void SaveChanges();
    }
}
