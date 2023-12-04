namespace TaskManager.src.view
{
    public class ViewData
    {
        private string _header;
        public string Header {
            get => _header;
        }

        private string[] _menu;
        public string[] Menu 
        {
            get => _menu;
        }

        private Prompt[] _prompts;
        public Prompt[] Prompts
        {
            get => _prompts;
        }
        public ViewData(string header, string[] menu, Prompt[] prompts)
        {
            ArgumentNullException.ThrowIfNull(header);
            ArgumentNullException.ThrowIfNull(menu);
            ArgumentNullException.ThrowIfNull(prompts);

            _header = header;
            _menu = menu;
            _prompts = prompts;
        }

        public string GetPromptContent(string promptName)
        {
            return null;
        }
    }
}