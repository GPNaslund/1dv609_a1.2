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

        public UserCommand Initialize()
        {
            View.DisplayHeader();
            View.DisplayMenu();
            View.GetInput("Your choice: ");
            return UserCommand.Add_Task;
        }
    }
}