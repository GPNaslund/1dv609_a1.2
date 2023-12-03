namespace TaskManager.src.view
{
    public class ConsoleView
    {
        private string _header;
        public string Header
        {
            get => _header;
        }

        private string[] _menu;
        public string[] Menu
        {
            get => _menu;
        }

        private readonly ConsoleService ConsoleService;
        public ConsoleView(ViewType type, ConsoleService consoleService)
        {
            SetHeaderAndMenu(type);
            ConsoleService = consoleService;
        }

        public void DisplayHeader()
        {
            ConsoleService.WriteLine("=== " + _header.ToUpper() + " ===");
        }

        public void DisplayMenu()
        {
            
        }

        private void SetHeaderAndMenu(ViewType type)
        {
            switch (type)
            {
                case ViewType.Add_Task_View:
                    AddTaskView_Setup();
                    break;
                case ViewType.Edit_Task:
                    EditTaskView_Setup();
                    break;
                case ViewType.List_Tasks:
                    ListTasksView_Setup();
                    break;
                case ViewType.View_All_Tasks:
                    ViewAllTasksView_Setup();
                    break;
                case ViewType.Main_Menu:
                    MainMenuView_Setup();
                    break;
            }
        }

        private void AddTaskView_Setup()
        {
            _header = "Add Task";
            _menu = [
                "Each task needs: ",
                "- An header",
                "- An optional description",
                "- A due date, that is not before today"
            ];
        }

        private void EditTaskView_Setup()
        {
            _header = "Edit Task";
            _menu = [
                "1. Edit Name",
                "2. Edit Description",
                "3. Edit Status",
                "4. Edit Due date",
                "5. Delete Task",
                "0. Go back"
            ];
        }

        private void ListTasksView_Setup()
        {
            _header = "List Tasks";
            _menu = [
            "1. List by due date",
            "2. List incompleted tasks",
            "3. List completed tasks",
            "4. List expired tasks",
            "0. Go back"
        ];
        }

        private void ViewAllTasksView_Setup()
        {
            _header = "All Tasks";
            _menu = [""];
        }

        private void MainMenuView_Setup()
        {
            _header = "Main Menu";
            _menu = [   
                "1. Add new Task",
                "2. View all Tasks",
                "3. List Tasks",
                "4. Edit Task",
                "0. Exit"
            ];
        }
    }
}