using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkFocusApp.Models;

namespace MonkFocusModels
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }

        public string TaskName { get; set; }
        public User User { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
    }
}
