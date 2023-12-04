using TaskManager.src.model;
using View = TaskManager.src.view.View;
using Task = TaskManager.src.model.Task;
using TaskStatus = TaskManager.src.model.TaskStatus;
using System.Globalization;

namespace TaskManager.src.controller
{
    public class EditTaskController : ExecutingController
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
            
            UserCommand currentState = UserCommand.Unkown;
            while (currentState != UserCommand.Main_Menu)
            {
                View.DisplayMessage("=== " + selectedTask.ToString() + " ===");
                View.DisplayMenu();
                string userChoice = View.GetInput("Your choice: ");
                currentState = HandleMenuChoice(userChoice, selectedTask);
            }
            return currentState;
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

        private UserCommand HandleMenuChoice(string choice, Task chosenTask)
        {
            switch (choice)
            {
                case "1":
                    EditName(chosenTask);
                    return UserCommand.Edit_Task;
                case "2":
                    EditDescription(chosenTask);
                    return UserCommand.Edit_Task;
                case "3":
                    EditDueDate(chosenTask);
                    return UserCommand.Edit_Task;
                case "4":
                    EditStatus(chosenTask);
                    return UserCommand.Edit_Task;
                case "5":
                    DeleteTask(chosenTask);
                    return UserCommand.Edit_Task;
                case "0":
                    return UserCommand.Main_Menu;
                default:
                    return UserCommand.Edit_Task;
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
            IEnumerable<TaskStatus> allStatuses = Enum.GetValues(typeof(TaskStatus)).Cast<TaskStatus>();
            int menuOptionNumber = 1;
            foreach (TaskStatus status in allStatuses)
            {
                View.DisplayMessage(menuOptionNumber + ". " + status.ToString().Replace("_", " "));
                menuOptionNumber += 1;
            }
            while (true)
            {
                try
                {
                    string userSelection = View.GetInput("Select new status: ");
                    if (int.TryParse(userSelection, out int selectedIndex) &&
                        selectedIndex >= 1 && selectedIndex <= allStatuses.Count()
                    )
                    {
                        chosenTask.Status = allStatuses.ElementAt(selectedIndex - 1);
                        TaskService.UpdateTask(chosenTask);
                        return;
                    }
                    else
                    {
                         throw new ArgumentException("Input must be one of the options, try again!");
                    }
                }
                catch (Exception e)
                {
                    View.DisplayMessage("Could not update status.");
                    View.DisplayMessage(e.Message);
                    View.DisplayMessage("Try again!");
                }
            }
        }

        private void DeleteTask(Task taskToDelete)
        {
            while (true)
            {
                try
                {
                    string userChoice = View.GetInput("Are you sure? y/n");
                    switch (userChoice)
                    {
                        case "y":
                            TaskService.DeleteTask(taskToDelete);
                            View.DisplayMessage("Task deleted succesfully!");
                            return;
                        case "n":
                            return;
                        default:
                            throw new ArgumentException("Input must be y or n, try again!");
                    }
                }
                catch (Exception e)
                {
                    View.DisplayMessage("Could not delete task!");
                    View.DisplayMessage(e.Message);
                    View.DisplayMessage("Try again!");
                }

            }
        }
    }
}