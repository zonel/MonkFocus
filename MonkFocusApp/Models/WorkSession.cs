using System;

namespace MonkFocusModels
{
    public class WorkSession
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration => EndTime - StartTime;
        public string Description { get; set; }
        public int UserId { get; set; }
        public virtual Users User { get; set; }
    }
}