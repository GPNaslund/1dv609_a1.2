using System.Globalization;
using TaskManager.src.model;
using TaskManager.src.view;
using View = TaskManager.src.view.View;

namespace TaskManager.src.controller
{
    public class AddTaskController : ExecutingController
    {
        private readonly View View;
        private readonly ITaskService TaskService;

        private readonly ViewData Data;

        public AddTaskController(View view, ITaskService taskService, ViewData data)
        {
            ArgumentNullException.ThrowIfNull(view);
            ArgumentNullException.ThrowIfNull(taskService);
            View = view;
            TaskService = taskService;
            Data = data;

        }

        public UserCommand Initialize()
        {
            while (true)
            {
                try
                {
                    View.DisplayHeader();
                    View.DisplayMenu();
                    PromptAndCreateNewTask();
                    return UserCommand.Main_Menu;
                }
                catch (Exception e)
                {
                    View.DisplayMessage(Data.GetPromptContent("Creation failure"));
                    View.DisplayMessage(e.Message);
                    View.DisplayMessage(Data.GetPromptContent("Try again"));
                }
            }
        }

        
        private void PromptAndCreateNewTask()
        {
            string name = View.GetInput(Data.GetPromptContent("Name"));
            string description = View.GetInput(Data.GetPromptContent("Description"));
            string dueDateString = View.GetInput(Data.GetPromptContent("Due date"));
            DateTime dueDate = DateTime.ParseExact(dueDateString, "yyMMdd", CultureInfo.InvariantCulture);
            TaskService.CreateTask(name, description, dueDate);
        }
    }
}