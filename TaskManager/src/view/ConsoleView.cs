namespace TaskManager.src.view
{
    public class ConsoleView : View
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
        public ConsoleView(ViewData data, ConsoleService consoleService)
        {
            ArgumentNullException.ThrowIfNull(consoleService);
            ConsoleService = consoleService;
            _header = data.Header;
            _menu = data.Menu;
        }

        public void DisplayHeader()
        {
            ConsoleService.WriteLine("=== " + _header.ToUpper() + " ===");
        }

        public void DisplayMenu()
        {
            foreach (string menuItem in _menu)
            {
                ConsoleService.WriteLine(menuItem);
            }
        }

        public void DisplayMessage(string message)
        {
            ConsoleService.WriteLine(message);
        }

        public string GetInput(string prompt)
        {
            return ConsoleService.ReadLine(prompt);
        }

    }
}