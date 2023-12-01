using View = TaskManager.src.view.View;
using TaskManager.src.model;

namespace TaskManager.src.controller
{
    public class ViewAllTasksController
    {
        private readonly ITaskService TaskService;
        public ViewAllTasksController(View view, ITaskService service)
        {
            ArgumentNullException.ThrowIfNull(view);
            ArgumentNullException.ThrowIfNull(service);
            TaskService = service;
        }

        public void Initialize()
        {
            TaskService.GetAllTasks();
        }
    }
}