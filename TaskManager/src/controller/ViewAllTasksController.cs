using View = TaskManager.src.view.View;
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

        public void Initialize()
        {
            View.DisplayHeader();
            TaskService.GetAllTasks();
        }
    }
}