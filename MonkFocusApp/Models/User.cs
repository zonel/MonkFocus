using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkFocusModels
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public int Points { get; set; }
        public TimeOnly WakeUpTime { get; set; }
        public TimeOnly BedTime { get; set; }
        public TimeSpan WorkTimeGoal { get; set; }
        public IEnumerable<WorkSession> WorkSessions { get; set; } //skonfiguruj relacje user - worksession
        public IEnumerable<UserTask> Task { get; set; }
    }
}
