using View = TaskManager.src.view.View;
using Task = TaskManager.src.model.Task;
using TaskManager.src.model;
using TaskManager.src.view;

namespace TaskManager.src.controller
{
    public class ViewAllTasksController : ExecutingController
    {
        private readonly ITaskService TaskService;
        private readonly View View;

        private readonly ViewData Data;
        public ViewAllTasksController(View view, ITaskService service, ViewData data)
        {
            ArgumentNullException.ThrowIfNull(view);
            ArgumentNullException.ThrowIfNull(service);
            TaskService = service;
            View = view;
            Data = data;
        }

        public UserCommand Initialize()
        {
            View.DisplayHeader();
            try 
            {

                List<Task> allTasks = TaskService.GetAllTasks();
                foreach (Task task in allTasks)
                {
                    View.DisplayMessage(task.ToString());
                }
                return UserCommand.Main_Menu;
            }
            catch (Exception e)
            {
                View.DisplayMessage(Data.GetPromptContent("Get list fail"));
                View.DisplayMessage(e.Message);
                return UserCommand.Main_Menu;
            }
        }
    }
}