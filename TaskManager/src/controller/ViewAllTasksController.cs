using View = TaskManager.src.view.View;
using TaskManager.src.model;

namespace TaskManager.src.controller
{
    public class ViewAllTasksController
    {
        public ViewAllTasksController(View view, ITaskService service)
        {
            ArgumentNullException.ThrowIfNull(view);
            ArgumentNullException.ThrowIfNull(service);
        }

        public void Initialize()
        {
            
        }
    }
}