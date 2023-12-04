namespace TaskManager.src.view
{
    public class ViewManager
    {
        private Dictionary<ViewType, ViewData> Views;

        public ViewManager()
        {
            Views = new Dictionary<ViewType, ViewData>();
            InitializeViewData();
        }
        public ViewData GetViewData(ViewType type)
        {
            return Views.GetValueOrDefault(type);
        }

        private void InitializeViewData()
        {
            Initialize_AddTaskViewData();
            Initialize_EditTaskViewData();
            Intialize_ListTasksViewData();
            Initialize_ViewAllTasksViewData();
            Initialize_MainMenuViewData();

        }

        private void Initialize_AddTaskViewData()
        {
            Views[ViewType.Add_Task] = new ViewData(
                header: "Add Task",
                menu: [
                "Each task needs: ",
                "- An header",
                "- An optional description",
                "- A due date, that is not before today"
                ],
                prompts: [
                    new Prompt("Name", "Enter the name: "),
                    new Prompt("Description", "Enter the description: "),
                    new Prompt("Due date", "Enter due date (yymmdd): "),
                    new Prompt("Creation failure", "Task creation failed"),
                    new Prompt("Try again", "Try again!")
                ]
            );
        }

        private void Initialize_EditTaskViewData()
        {
            Views[ViewType.Edit_Task] = new ViewData(
                header: "Edit task",
                menu: [
                "1. Edit Name",
                "2. Edit Description",
                "3. Edit Due date",
                "4. Edit Status",
                "5. Delete Task",
                "0. Go back"
                ],
                prompts: [
                    new Prompt("Select task", "=== SELECT TASK ==="),
                    new Prompt("Your choice", "Your choice: "),
                    new Prompt("Select task", "Select task: "),
                    new Prompt("New name", "New name: "),
                    new Prompt("Name edit error", "Could not edit name of task!"),
                    new Prompt("Try again", "Try again!"),
                    new Prompt("New description", "New description: "),
                    new Prompt("Descripiton edit error", "Could not edit description!"),
                    new Prompt("New due date", "New due date (yymmdd): "),
                    new Prompt("Due date edit error", "Could not update due date!"),
                    new Prompt("Status update error", "Could not update status."),
                    new Prompt("Delete confirmation", "Are you sure? y/n"),
                    new Prompt("Delete success", "Task deleted succesfully!"),
                    new Prompt("Delete input error", "Input must be y or n, try again!"),
                    new Prompt("Delete error", "Could not delete task!"),
                ]);
        }

        private void Intialize_ListTasksViewData()
        {
            Views[ViewType.List_Tasks] = new ViewData(
                header: "List Tasks",
                menu: [
                "1. List by due date",
                "2. List incompleted tasks",
                "3. List completed tasks",
                "4. List expired tasks",
                "0. Go back"
                ],
                prompts: [
                    new Prompt("Select listing", "Your choice: "),
                    new Prompt("Invalid input", "Invalid input: "),
                    new Prompt("Try again", "Try again!"),
                    new Prompt("No tasks", "No tasks fullfilled the list criteria.."),
                    new Prompt("Get list fail", "Failed to get list of tasks!"),
                ]
            );
        }

        private void Initialize_ViewAllTasksViewData()
        {
            Views[ViewType.View_All_Tasks] = new ViewData(
                header: "All Tasks",
                menu: ["Here are all the tasks:"],
                prompts : [
                    new Prompt("Get list fail", "Failed to get tasks!")
                ]
            );
        }

        private void Initialize_MainMenuViewData()
        {
            Views[ViewType.Main_Menu] = new ViewData(
                header: "Main Menu",
                menu: [
                "1. Add new Task",
                "2. View all Tasks",
                "3. List Tasks",
                "4. Edit Task",
                "0. Exit"
                ],
                prompts: [
                    new Prompt("Select option", "Your choice: "),
                    new Prompt("Input error", "Input must be a number presented in the menu, try again!"),
                ]
            );
        }


    }
}