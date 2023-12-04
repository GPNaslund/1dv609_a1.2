using TaskManager.src.view;
using View = TaskManager.src.view.View;

namespace TaskManager.src.controller
{
    public class MainMenuController : ExecutingController
    {
        private readonly View View;
        private readonly ViewData Data;
        public MainMenuController(View view, ViewData data)
        {
            ArgumentNullException.ThrowIfNull(view);
            View = view;
            Data = data;
        }

        public UserCommand Initialize()
        {
            View.DisplayHeader();
            View.DisplayMenu();

            UserCommand result = UserCommand.Unkown;
            while (result == UserCommand.Unkown)
            {
                string input = View.GetInput(Data.GetPromptContent("Select option"));
                result = HandleInput(input);
                if (result == UserCommand.Unkown)
                {
                    View.DisplayMessage(Data.GetPromptContent("Input error"));
                }
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