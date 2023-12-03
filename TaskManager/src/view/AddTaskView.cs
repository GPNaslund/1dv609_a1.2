namespace TaskManager.src.view
{
    public class AddTaskView
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

        public AddTaskView()
        {
            _header = "";
            _menu = [];
        }
    }
}