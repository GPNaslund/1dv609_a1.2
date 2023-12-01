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
                    TaskService.ListTasksBy(ListByCommand.List_By_Due_Date);
                    return UserCommand.Unkown;
                case "2":
                    TaskService.ListTasksBy(ListByCommand.List_Incomplete_Tasks);
                    return UserCommand.Unkown;
                case "3":
                    TaskService.ListTasksBy(ListByCommand.List_Completed_Tasks);
                    return UserCommand.Unkown;
                case "4":
                    TaskService.ListTasksBy(ListByCommand.List_Expired_Tasks);
                    return UserCommand.Unkown;
                case "0":
                    return UserCommand.Main_Menu;
                default:
                    return UserCommand.Unkown;
            }
        }
    }
}