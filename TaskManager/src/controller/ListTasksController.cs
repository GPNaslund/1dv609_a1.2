using View = TaskManager.src.view.View;

using TaskManager.src.model;

namespace TaskManager.src.controller
{
    public class ListTasksController
    {
        private readonly View View;
        private readonly ITaskService TaskService;
        public ListTasksController(View view, ITaskService service)
        {
            ArgumentNullException.ThrowIfNull(view);
            ArgumentNullException.ThrowIfNull(service);
            View = view;
            TaskService = service;
        }

        public void Initialize()
        {
            View.DisplayHeader();
            View.DisplayMenu();
            TaskService.ListTasksBy(ListByCommand.List_By_Due_Date);
        }
    }
}