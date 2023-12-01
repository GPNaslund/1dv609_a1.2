using TaskManager.src.model;
using View = TaskManager.src.view.View;
using Task = TaskManager.src.model.Task;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TaskManager.src.controller
{
    public class EditTaskController
    {
        private readonly ITaskService TaskService;
        private readonly View View;
        public EditTaskController(View view, ITaskService service)
        {
            ArgumentNullException.ThrowIfNull(view);
            ArgumentNullException.ThrowIfNull(service);
            TaskService = service;
            View = view;
        }

        public UserCommand Initialize()
        {
            View.DisplayMessage("=== SELECT TASK ===");
            int selectedIndex = PromptUserToSelectTask();
            if (selectedIndex == 0)
            {
                return UserCommand.Main_Menu;
            }
            Task selectedTask = TaskService.GetAllTasks()[selectedIndex - 1];
            string userChoice = View.GetInput("Your choice: ");
            HandleMenuChoice(userChoice, selectedTask);
            return UserCommand.Unkown;
        }

        private int PromptUserToSelectTask()
        {
            List<Task> allTasks = TaskService.GetAllTasks();
            for (int i = 0; i < allTasks.Count; i++)
            {
                View.DisplayMessage(i + 1 + ". " + allTasks[i].ToString());
            }
            View.DisplayMessage("0. Go Back");
            while (true)
            {
                string newInput = View.GetInput("Select task: ");
                if (ValidateTaskSelectInput(allTasks, newInput))
                {
                    return int.Parse(newInput);
                }
            }
        }

        private bool ValidateTaskSelectInput(List<Task> allTasks, string userInput)
        {
            try
            {
                int convertedInput = int.Parse(userInput);
                if (convertedInput >= 0 && convertedInput <= allTasks.Count)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private void HandleMenuChoice(string choice, Task chosenTask)
        {
            switch(choice)
            {
                case "1":
                    string newName = View.GetInput("New name: ");
                    chosenTask.Name = newName;
                    TaskService.UpdateTask(chosenTask);
                    return;
                default:
                    return;
            }
        }
    }
}