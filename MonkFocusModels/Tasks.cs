using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkFocusModels
{
    public class Tasks
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public virtual Users User { get; set; }
    }
}
