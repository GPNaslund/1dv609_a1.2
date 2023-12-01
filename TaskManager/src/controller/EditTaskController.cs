using TaskManager.src.model;
using View = TaskManager.src.view.View;
using Task = TaskManager.src.model.Task;
using TaskStatus = TaskManager.src.model.TaskStatus;
using System.Globalization;

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
            switch (choice)
            {
                case "1":
                    EditName(chosenTask);
                    return;
                case "2":
                    EditDescription(chosenTask);
                    return;
                case "3":
                    EditDueDate(chosenTask);
                    return;
                case "4":
                    EditStatus(chosenTask);
                    return;
                case "5":
                    string userChoice = View.GetInput("Are you sure? y/n");
                    if (userChoice.ToLower() == "y")
                    {
                        TaskService.DeleteTask(chosenTask);
                    }
                    return;
                default:
                    return;
            }
        }

        private void EditName(Task chosenTask)
        {
            while (true)
            {
                try
                {
                    string newName = View.GetInput("New name: ");
                    chosenTask.Name = newName;
                    TaskService.UpdateTask(chosenTask);
                    return;
                }
                catch (Exception e)
                {
                    View.DisplayMessage("Could not edit name of task!");
                    View.DisplayMessage(e.Message);
                    View.DisplayMessage("Try again!");
                }
            }
        }

        private void EditDescription(Task chosenTask)
        {
            while (true)
            {
                try
                {
                    string newDescription = View.GetInput("New description: ");
                    chosenTask.Description = newDescription;
                    TaskService.UpdateTask(chosenTask);
                    return;
                }
                catch (Exception e)
                {
                    View.DisplayMessage("Could not edit description!");
                    View.DisplayMessage(e.Message);
                    View.DisplayMessage("Try again!");
                }

            }
        }

        private void EditDueDate(Task chosenTask)
        {
            while (true)
            {
                try
                {
                    string dateInput = View.GetInput("New due date (yymmdd): ");
                    chosenTask.DueDate = DateTime.ParseExact(dateInput, "yyMMdd", CultureInfo.InvariantCulture);
                    TaskService.UpdateTask(chosenTask);
                    return;
                }
                catch (Exception e)
                {
                    View.DisplayMessage("Could not update due date!");
                    View.DisplayMessage(e.Message);
                    View.DisplayMessage("Try again!");
                }
            }
        }

        private void EditStatus(Task chosenTask)
        {
            List<string> statusOptions = [
                "1. Not completed",
                "2. In progress",
                "3. Completed"
            ];
            foreach (string option in statusOptions)
            {
                View.DisplayMessage(option);
            }
            while (true)
            {
                string userSelection = View.GetInput("Select new status: ");
                switch (userSelection)
                {
                    case "1":
                        chosenTask.Status = TaskStatus.Not_Completed;
                        TaskService.UpdateTask(chosenTask);
                        return;
                    case "2":
                        chosenTask.Status = TaskStatus.In_Progress;
                        TaskService.UpdateTask(chosenTask);
                        return;
                    case "3":
                        chosenTask.Status = TaskStatus.Completed;
                        TaskService.UpdateTask(chosenTask);
                        return;
                    default:
                        View.DisplayMessage("Input must be one of the options, try again!");
                        break;
                }
            }

        }
    }
}