using System.Linq;
using System.Windows;
using System.Windows.Input;
using MonkFocusApp.Commands;
using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories;


namespace MonkFocusApp.ViewModels
{
    class TaskManagerViewModel : BaseViewModel
    {
        private readonly int _userId;
        private readonly UserRepository _userRepository;
        private readonly TaskRepository _taskRepository = new TaskRepository(new MonkFocusDbContext());

        public ICommand AddTaskCommand { get; }
        public ICommand UpdateTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }


        private string _selectedPriorityAdd;
        public string SelectedPriorityAdd
        {
            get { return _selectedPriorityAdd; }
            set
            {
                _selectedPriorityAdd = value;
                OnPropertyChanged(nameof(SelectedPriorityAdd));
            }
        }
        
        private string _tasknameadd;
        public string TaskNameAdd
        {
            get { return _tasknameadd; }
            set
            {
                _tasknameadd = value;
                OnPropertyChanged(nameof(TaskNameAdd));
            }
        }

        private string _taskIdUpdate;
        public string TaskIdUpdate
        {
            get { return _taskIdUpdate; }
            set
            {
                _taskIdUpdate = value;
                OnPropertyChanged(nameof(TaskIdUpdate));
            }
        }

        private string _selectedpriorityupdatetask;
        public string SelectedPriorityUpdateTask
        {
            get { return _selectedpriorityupdatetask; }
            set
            {
                _selectedpriorityupdatetask = value;
                OnPropertyChanged(nameof(SelectedPriorityUpdateTask));
            }
        }

        private string _selectedstatusupdatetask;
        public string SelectedStatusUpdateTask
        {
            get { return _selectedstatusupdatetask; }
            set
            {
                _selectedstatusupdatetask = value;
                OnPropertyChanged(nameof(SelectedStatusUpdateTask));
            }
        }

        public TaskManagerViewModel(int userId)
        {
            _userId = userId;
            _userRepository = new UserRepository(new MonkFocusDbContext());
            AddTaskCommand = new RelayCommand(AddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask);
            UpdateTaskCommand = new RelayCommand(UpdateTask);
        }

        private void AddTask()
        {
            int priorityNumber = SelectedPriorityAdd switch
            {
                "System.Windows.Controls.ComboBoxItem: Low" => 1,
                "System.Windows.Controls.ComboBoxItem: Medium" => 2,
                "System.Windows.Controls.ComboBoxItem: Crucial" => 3,
                _ => 0 
            };

            var TaskName = TaskNameAdd;
            //gets data from task and priority field, checks if it's correct and adds it to the database
            if (priorityNumber is 0 || TaskName is null)
            {
                MessageBox.Show("Please fill in all fields"); 
                return;
            }
            _taskRepository.AddTask(new UserTask()
            {
                PriorityId = priorityNumber,
                TaskName = TaskName,
                UserId = _userId,
                StatusId = 1 
            });
        }
        private void DeleteTask()
        {
            if (TaskIdUpdate is null || !int.TryParse(TaskIdUpdate, out int TaskId))
            {
                MessageBox.Show("Please fill in all fields");
                return;
            }

            var taskToDelete = _taskRepository.GetAllTasksForUser(_userId)
                .FirstOrDefault(t => t.TaskId == TaskId);
            if (taskToDelete is null)
            {
                MessageBox.Show("Task with this ID does no longer exist.");
                return;
            }

            _taskRepository.DeleteTaskById(TaskId);
            MessageBox.Show($"Removed task with ID: {TaskId}");
        }
        private void UpdateTask()
        {
            int priorityNumber = SelectedPriorityUpdateTask switch
            {
                "System.Windows.Controls.ComboBoxItem: Low" => 1,
                "System.Windows.Controls.ComboBoxItem: Medium" => 2,
                "System.Windows.Controls.ComboBoxItem: Crucial" => 3,
                _ => 0 
            };

            int statusNumber = SelectedStatusUpdateTask switch
            {
                "System.Windows.Controls.ComboBoxItem: In Progress" => 1,
                "System.Windows.Controls.ComboBoxItem: Done" => 2,
                _ => 0 
            };

            var TaskId = TaskIdUpdate;
            var taskToUpdate = _taskRepository.GetAllTasksForUser(_userId)
                .FirstOrDefault(t => t.TaskId == int.Parse(TaskId));

            if (taskToUpdate != null)
            {
                taskToUpdate.TaskName = taskToUpdate.TaskName;
                taskToUpdate.PriorityId = priorityNumber;
                taskToUpdate.UserId = _userId;
                taskToUpdate.StatusId = statusNumber;
                _taskRepository.UpdateTask(taskToUpdate);
            }
        }
    }
}
