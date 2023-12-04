using View = TaskManager.src.view.View;
using Task = TaskManager.src.model.Task;

using TaskManager.src.model;
using TaskManager.src.view;

namespace TaskManager.src.controller
{
    public class ListTasksController : ExecutingController
    {
        private readonly View View;
        private readonly ITaskService TaskService;
        private readonly ViewData Data;
        public ListTasksController(View view, ITaskService service, ViewData data)
        {
            ArgumentNullException.ThrowIfNull(view);
            ArgumentNullException.ThrowIfNull(service);
            View = view;
            TaskService = service;
            Data = data;
        }

        public UserCommand Initialize()
        {
            UserCommand nextCommand = UserCommand.Unkown;
            while (nextCommand == UserCommand.Unkown)
            {
                View.DisplayHeader();
                View.DisplayMenu();
                string userInput = View.GetInput(Data.GetPromptContent("Select listing"));
                nextCommand = HandleUserInput(userInput);
            }
            return nextCommand;
        }

        private UserCommand HandleUserInput(string input)
        {
            switch (input)
            {
                case "1":
                    DisplayTaskList(ListByCommand.List_By_Due_Date);
                    return UserCommand.Unkown;
                case "2":
                    DisplayTaskList(ListByCommand.List_Incomplete_Tasks);
                    return UserCommand.Unkown;
                case "3":
                    DisplayTaskList(ListByCommand.List_Completed_Tasks);
                    return UserCommand.Unkown;
                case "4":
                    DisplayTaskList(ListByCommand.List_Expired_Tasks);
                    return UserCommand.Unkown;
                case "0":
                    return UserCommand.Main_Menu;
                default:
                    View.DisplayMessage(Data.GetPromptContent("Invalid input") + input + Data.GetPromptContent("Try again"));
                    return UserCommand.Unkown;
            }
        }

        private void DisplayTaskList(ListByCommand command)
        {
            try
            {
                List<Task> tasks = TaskService.ListTasksBy(command);
                if (tasks.Count == 0)
                {
                    View.DisplayMessage(Data.GetPromptContent("No tasks"));
                }
                else
                {
                    foreach (Task task in tasks)
                    {
                        View.DisplayMessage(task.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                View.DisplayMessage(Data.GetPromptContent("Get list fail"));
                View.DisplayMessage(e.Message);
            }
            
        }
    }
}