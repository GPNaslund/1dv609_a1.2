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
        public ConsoleView(ViewType type)
        {
            SetHeaderAndMenu(type);
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
    }
}