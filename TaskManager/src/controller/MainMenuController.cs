using View = TaskManager.src.view.view;

namespace TaskManager.src.controller
{
    public class MainMenuController
    {
        private readonly View View;
        public MainMenuController(View view)
        {
            ArgumentNullException.ThrowIfNull(view);
            View = view;
        }

        public void Initialize()
        {
            View.DisplayHeader();
            View.DisplayMenu();
            View.GetInput("Your choice: ");
        }
    }
}