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

            UserCommand result = UserCommand.Unkown;
            while (result == UserCommand.Unkown)
            {
                string input = View.GetInput("Your choice: ");
                result = HandleInput(input);
            }
            return result;
        }

        private UserCommand HandleInput(string input)
        {
            switch (input)
            {
                case "1":
                    return UserCommand.Add_Task;
                case "2":
                    return UserCommand.View_All_Tasks;
                case "3":
                    return UserCommand.List_Tasks;
                case "4":
                    return UserCommand.Edit_Task;
                case "0":
                    return UserCommand.Quit_Application;
                default:
                    return UserCommand.Unkown;
            }
        }
    }
}