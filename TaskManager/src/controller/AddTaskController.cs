using System.Globalization;
using TaskManager.src.model;
using View = TaskManager.src.view.View;

namespace TaskManager.src.controller
{
    public class AddTaskController
    {
        private readonly View View;
        private readonly ITaskService TaskService;
        public AddTaskController(View view, ITaskService taskService)
        {
            ArgumentNullException.ThrowIfNull(view);
            ArgumentNullException.ThrowIfNull(taskService);
            View = view;
            TaskService = taskService;
        }

        public UserCommand Initialize()
        {
            while (true)
            {
                try
                {
                    PromptAndCreateNewTask();
                    return UserCommand.Main_Menu;
                }
                catch (Exception e)
                {
                    View.DisplayMessage("Task creation failed");
                    View.DisplayMessage(e.Message);
                    View.DisplayMessage("Try again!");
                }
            }
        }

        
        private void PromptAndCreateNewTask()
        {
            string name = View.GetInput("Enter the name: ");
            string description = View.GetInput("Enter the description: ");
            string dueDateString = View.GetInput("Enter due date (yymmdd): ");
            DateTime dueDate = DateTime.ParseExact(dueDateString, "yyMMdd", CultureInfo.InvariantCulture);
            TaskService.CreateTask(name, description, dueDate);
        }
    }
}