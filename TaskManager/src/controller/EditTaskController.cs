using TaskManager.src.model;
using View = TaskManager.src.view.View;

namespace TaskManager.src.controller
{
    public class EditTaskController
    {
        private readonly ITaskService TaskService;
        public EditTaskController(View view, ITaskService service)
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