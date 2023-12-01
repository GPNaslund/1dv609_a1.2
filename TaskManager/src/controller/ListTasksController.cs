using View = TaskManager.src.view.View;
using Task = TaskManager.src.model.Task;

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
            UserCommand nextCommand = UserCommand.Unkown;
            while (nextCommand == UserCommand.Unkown)
            {
                View.DisplayHeader();
                View.DisplayMenu();
                string userInput = View.GetInput("Your choice: ");
                nextCommand = HandleUserInput(userInput);
            }
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
                    View.DisplayMessage("Invalid input: " + input + ". Try again!");
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
                    View.DisplayMessage("No tasks fullfilled the list criteria..");
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
                View.DisplayMessage("Failed to get list of tasks!");
                View.DisplayMessage(e.Message);
            }
            
        }
    }
}