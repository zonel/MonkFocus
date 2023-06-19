using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkFocusModels
{
    public class UserSettings
    {
        public Users UserId { get; set; }
        public int SleepTime { get; set; }
        public int WakeTime { get; set; }
        public int WorkTimeGoal { get; set; }
    }
}
