using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MonkFocusModels;

namespace MonkFocusApp.DTO
{
    public class UserTaskDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public UserTaskDTO(UserTask userTask)
        {
            TaskName = userTask.TaskName;
            Priority = userTask.PriorityId.ToString();
            Status = GetStatusString(userTask.StatusId);
        }

        private string GetStatusString(int statusId)
        {
            return statusId == 2 ? "✔️" : " ";
        }

        private string _priority;
        public string Priority
        {
            get { return _priority; }
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                    OnPropertyChanged(nameof(Priority));
                }
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        private string _taskname;
        public string TaskName
        {
            get { return _taskname; }
            set
            {
                if (_taskname != value)
                {
                    _taskname = value;
                    OnPropertyChanged(nameof(TaskName));
                }
            }
        }


        #region INotifyPropertyChanged

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}
