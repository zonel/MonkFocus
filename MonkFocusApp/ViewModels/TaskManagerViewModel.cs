using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MonkFocusApp.Commands;
using MonkFocusApp.DTO;
using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories;

namespace MonkFocusApp.ViewModels;

/// <summary>
///     This is view model for the task manager page.
/// </summary>
internal class TaskManagerViewModel : BaseViewModel
{
    private readonly TaskRepository _taskRepository = new(new MonkFocusDbContext());
    private readonly int _userId;
    private readonly UserRepository _userRepository;


    #region Private backing fields

    private string _selectedPriorityAdd;

    private string _selectedpriorityupdatetask;

    private string _selectedstatusupdatetask;

    private string _taskIdUpdate;

    private string _tasknameadd;

    private ObservableCollection<UserTaskTableViewDTO> _tasksDisplay;

    #endregion

    public TaskManagerViewModel(int userId)
    {
        _userId = userId;
        _userRepository = new UserRepository(new MonkFocusDbContext());
        AddTaskCommand = new RelayCommand(AddTask);
        DeleteTaskCommand = new RelayCommand(DeleteTask);
        UpdateTaskCommand = new RelayCommand(UpdateTask);
        PopulateTasksDisplay();
    }

    #region Command

    public ICommand AddTaskCommand { get; }
    public ICommand UpdateTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }

    #endregion

    public string SelectedPriorityAdd
    {
        get => _selectedPriorityAdd;
        set
        {
            _selectedPriorityAdd = value;
            OnPropertyChanged();
        }
    }

    public string TaskNameAdd
    {
        get => _tasknameadd;
        set
        {
            _tasknameadd = value;
            OnPropertyChanged();
        }
    }

    public string TaskIdUpdate
    {
        get => _taskIdUpdate;
        set
        {
            _taskIdUpdate = value;
            OnPropertyChanged();
        }
    }

    public string SelectedPriorityUpdateTask
    {
        get => _selectedpriorityupdatetask;
        set
        {
            _selectedpriorityupdatetask = value;
            OnPropertyChanged();
        }
    }

    public string SelectedStatusUpdateTask
    {
        get => _selectedstatusupdatetask;
        set
        {
            _selectedstatusupdatetask = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<UserTaskTableViewDTO> TasksDisplay
    {
        get => _tasksDisplay;
        set
        {
            _tasksDisplay = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     This method adds a task to the database.
    /// </summary>
    private void AddTask()
    {
        var priorityNumber = SelectedPriorityAdd switch
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

        _taskRepository.AddTask(new UserTask
        {
            PriorityId = priorityNumber,
            TaskName = TaskName,
            UserId = _userId,
            StatusId = 1
        });

        PopulateTasksDisplay();
    }

    /// <summary>
    ///     This method deletes a task from the database.
    /// </summary>
    private void DeleteTask()
    {
        if (TaskIdUpdate is null || !int.TryParse(TaskIdUpdate, out var TaskId))
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

    /// <summary>
    ///     This method updates a task in the database.
    /// </summary>
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


    /// <summary>
    ///     This method populates the TasksDisplay property with data from the database.
    /// </summary>
    private void PopulateTasksDisplay()
    {
        var userTasks = _taskRepository.GetAllTasksForUser(_userId).OrderByDescending(c => c.PriorityId)
            .OrderBy(s => s.StatusId);
        var mappedTasks = userTasks.Select(task => new UserTaskTableViewDTO
        {
            TaskId = task.TaskId,
            TaskName = task.TaskName,
            Status = task.StatusId == 1 ? "In Progress" : "Done",
            Priority = GetPriorityText(task.PriorityId)
        });

        TasksDisplay = new ObservableCollection<UserTaskTableViewDTO>(mappedTasks);
    }

    /// <summary>
    ///     This helper method returns the priority text for a given priority id.
    /// </summary>
    /// <param name="priorityId">PriorityId from Context</param>
    /// <returns>Corresponding string to priorityID int value.</returns>
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