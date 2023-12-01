using View = TaskManager.src.view.View;

using TaskManager.src.model;

namespace TaskManager.src.controller
{
    public class ListTasksController
    {
        private readonly View View;
        public ListTasksController(View view, ITaskService service)
        {
            ArgumentNullException.ThrowIfNull(view);
            ArgumentNullException.ThrowIfNull(service);
            View = view;
        }

        public void Initialize()
        {
            View.DisplayMenu();
        }
    }
}