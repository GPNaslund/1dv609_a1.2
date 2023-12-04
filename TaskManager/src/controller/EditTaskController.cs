using TaskManager.src.model;
using View = TaskManager.src.view.View;
using Task = TaskManager.src.model.Task;
using TaskStatus = TaskManager.src.model.TaskStatus;
using System.Globalization;
using TaskManager.src.view;

namespace TaskManager.src.controller
{
    public class EditTaskController : ExecutingController
    {
        private readonly ITaskService TaskService;
        private readonly View View;
        private readonly ViewData Data;
        public EditTaskController(View view, ITaskService service, ViewData data)
        {
            ArgumentNullException.ThrowIfNull(view);
            ArgumentNullException.ThrowIfNull(service);
            TaskService = service;
            View = view;
            Data = data;
        }

        public UserCommand Initialize()
        {
            View.DisplayMessage(Data.GetPromptContent("Select task"));
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
                string userChoice = View.GetInput(Data.GetPromptContent("Your choice"));
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
            View.DisplayMessage(Data.GetPromptContent("Go back"));
            while (true)
            {
                string newInput = View.GetInput(Data.GetPromptContent("Select task"));
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
                    string newName = View.GetInput(Data.GetPromptContent("New name"));
                    chosenTask.Name = newName;
                    TaskService.UpdateTask(chosenTask);
                    return;
                }
                catch (Exception e)
                {
                    View.DisplayMessage(Data.GetPromptContent("Name edit error"));
                    View.DisplayMessage(e.Message);
                    View.DisplayMessage(Data.GetPromptContent("Try again"));
                }
            }
        }

        private void EditDescription(Task chosenTask)
        {
            while (true)
            {
                try
                {
                    string newDescription = View.GetInput(Data.GetPromptContent("New description"));
                    chosenTask.Description = newDescription;
                    TaskService.UpdateTask(chosenTask);
                    return;
                }
                catch (Exception e)
                {
                    View.DisplayMessage(Data.GetPromptContent("Descripiton edit error"));
                    View.DisplayMessage(e.Message);
                    View.DisplayMessage(Data.GetPromptContent("Try again"));
                }

            }
        }

        private void EditDueDate(Task chosenTask)
        {
            while (true)
            {
                try
                {
                    string dateInput = View.GetInput(Data.GetPromptContent("New due date"));
                    chosenTask.DueDate = DateTime.ParseExact(dateInput, "yyMMdd", CultureInfo.InvariantCulture);
                    TaskService.UpdateTask(chosenTask);
                    return;
                }
                catch (Exception e)
                {
                    View.DisplayMessage(Data.GetPromptContent("Due date edit error"));
                    View.DisplayMessage(e.Message);
                    View.DisplayMessage(Data.GetPromptContent("Try again"));
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
                    string userSelection = View.GetInput(Data.GetPromptContent("New status"));
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
                         throw new ArgumentException(Data.GetPromptContent("Status edit error"));
                    }
                }
                catch (Exception e)
                {
                    View.DisplayMessage(Data.GetPromptContent("Status update error"));
                    View.DisplayMessage(e.Message);
                    View.DisplayMessage(Data.GetPromptContent("Try again"));
                }
            }
        }

        private void DeleteTask(Task taskToDelete)
        {
            while (true)
            {
                try
                {
                    string userChoice = View.GetInput(Data.GetPromptContent("Delete confirmation"));
                    switch (userChoice)
                    {
                        case "y":
                            TaskService.DeleteTask(taskToDelete);
                            View.DisplayMessage(Data.GetPromptContent("Delete success"));
                            return;
                        case "n":
                            return;
                        default:
                            throw new ArgumentException(Data.GetPromptContent("Delete input error"));
                    }
                }
                catch (Exception e)
                {
                    View.DisplayMessage(Data.GetPromptContent("Delete error"));
                    View.DisplayMessage(e.Message);
                    View.DisplayMessage(Data.GetPromptContent("Try again"));
                }

            }
        }
    }
}