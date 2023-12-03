using TaskManager.src.view;

namespace TaskManager.src.controller
{
    public class DefaultControllerFactory
    {
        public ExecutingController Create_MainMenuController()
        {
            DefaultConsoleService ConsoleService = new();
            ConsoleView View = new(ViewType.Main_Menu,ConsoleService);
            MainMenuController mainMenuController = new(View);
            return mainMenuController;
        }
    }
}