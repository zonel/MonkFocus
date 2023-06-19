using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkFocusModels
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string email { get; set; }
        public DateTime JoinDate { get; set; }
        public int Points { get; set; }
        public WorkSession WorkSessions { get; set; }
    }
}
