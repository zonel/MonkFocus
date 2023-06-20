using System;
using System.ComponentModel.DataAnnotations;

namespace MonkFocusModels
{
    public class WorkSession
    {
        [Key]
        public int WorkSessionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Points { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}