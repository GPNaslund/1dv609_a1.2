using View = TaskManager.src.view.View;
using Task = TaskManager.src.model.Task;
using TaskManager.src.model;

namespace TaskManager.src.controller
{
    public class ViewAllTasksController
    {
        private readonly ITaskService TaskService;
        private readonly View View;
        public ViewAllTasksController(View view, ITaskService service)
        {
            ArgumentNullException.ThrowIfNull(view);
            ArgumentNullException.ThrowIfNull(service);
            TaskService = service;
            View = view;
        }

        public UserCommand Initialize()
        {
            View.DisplayHeader();
            List<Task> allTasks = TaskService.GetAllTasks();
            foreach (Task task in allTasks)
            {
                View.DisplayMessage(task.ToString());
            }
            return UserCommand.Unkown;
        }
    }
}