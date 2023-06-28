using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkFocusApp.DTO
{
    /// <summary>
    /// This class is used to display the user's tasks in the table view.
    /// </summary>
    public class UserTaskTableViewDTO
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
    }
}
