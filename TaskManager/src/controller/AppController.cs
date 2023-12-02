namespace TaskManager.src.controller
{
    public class AppController
    {
        private readonly ControllerFactory Factory;
        public AppController(ControllerFactory factory)
        {
            ArgumentNullException.ThrowIfNull(factory);
            Factory = factory;
        }

        public void Run()
        {
            UserCommand currentCommand = UserCommand.Main_Menu;
            while (currentCommand != UserCommand.Quit_Application)
            {
                ExecutingController currentController = HandleUserCommand(currentCommand);
                currentCommand = currentController.Initialize();
            }
        }

        private ExecutingController HandleUserCommand(UserCommand commandToActOn)
        {
            switch (commandToActOn)
            {
                case UserCommand.Add_Task:
                    return Factory.Create_AddTaskMenuController();
                case UserCommand.View_All_Tasks:
                    return Factory.Create_ViewAllTasksMenuController();
                case UserCommand.List_Tasks:
                    return Factory.Create_ListTasksMenuController();
                case UserCommand.Edit_Task:
                    return Factory.Create_EditTaskMenuController();
                case UserCommand.Main_Menu:
                    return Factory.Create_MainMenuController();
                default:
                    throw new ArgumentException("UserCommand is not implemented!");
            }
        }
    }
}