using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MonkFocusApp.Commands;
using MonkFocusApp.DTO;
using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories;
using MonkFocusRepositories.Interfaces;


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

        private ObservableCollection<UserTaskTableViewDTO> _tasksDisplay;
        public ObservableCollection<UserTaskTableViewDTO> TasksDisplay
        {
            get { return _tasksDisplay; }
            set
            {
                _tasksDisplay = value;
                OnPropertyChanged(nameof(TasksDisplay));
            }
        }

        public TaskManagerViewModel(int userId)
        {
            _userId = userId;
            _userRepository = new UserRepository(new MonkFocusDbContext());
            AddTaskCommand = new RelayCommand(AddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask);
            UpdateTaskCommand = new RelayCommand(UpdateTask);
            //_tasksDisplay = new ObservableCollection<UserTask>(_taskRepository.GetAllTasksForUser(_userId));
            PopulateTasksDisplay();
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

            PopulateTasksDisplay();
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
            PopulateTasksDisplay();

        }
        private void UpdateTask()
        {
            int? priorityNumber = SelectedPriorityUpdateTask switch
            {
                "System.Windows.Controls.ComboBoxItem: Low" => 1,
                "System.Windows.Controls.ComboBoxItem: Medium" => 2,
                "System.Windows.Controls.ComboBoxItem: Crucial" => 3,
                _ => null 
            };

            int? statusNumber = SelectedStatusUpdateTask switch
            {
                "System.Windows.Controls.ComboBoxItem: In Progress" => 1,
                "System.Windows.Controls.ComboBoxItem: Done" => 2,
                _ => null 
            };

            var TaskId = TaskIdUpdate;
            if (TaskId is null)
            {
                MessageBox.Show("Task with this ID does no longer exist.");
                return;
            }
            var taskToUpdate = _taskRepository.GetAllTasksForUser(_userId)
                .FirstOrDefault(t => t.TaskId == int.Parse(TaskId));

            if (taskToUpdate != null)
            {
                taskToUpdate.TaskName = taskToUpdate.TaskName;
                taskToUpdate.PriorityId = priorityNumber ?? taskToUpdate.PriorityId;
                taskToUpdate.UserId = _userId;
                taskToUpdate.StatusId = statusNumber ?? taskToUpdate.StatusId;
                _taskRepository.UpdateTask(taskToUpdate);
            }
            PopulateTasksDisplay();

        }



        private void PopulateTasksDisplay()
        {
            var userTasks = _taskRepository.GetAllTasksForUser(_userId).OrderByDescending(c => c.PriorityId).OrderBy(s => s.StatusId);
            var mappedTasks = userTasks.Select(task => new UserTaskTableViewDTO
            {
                TaskId = task.TaskId,
                TaskName = task.TaskName,
                Status = task.StatusId == 1 ? "In Progress" : "Done",
                Priority = GetPriorityText(task.PriorityId)
            });

            TasksDisplay = new ObservableCollection<UserTaskTableViewDTO>(mappedTasks);
        }

        private string GetPriorityText(int priorityId)
        {
            switch (priorityId)
            {
                case 1:
                    return "Low";
                case 2:
                    return "Medium";
                case 3:
                    return "Crucial";
                default:
                    return string.Empty;
            }
        }
    }
}
