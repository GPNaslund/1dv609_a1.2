namespace TaskManager.src.controller
{
    public class AppController
    {
        private readonly TaskFactory Factory;
        public AppController(TaskFactory factory)
        {
            ArgumentNullException.ThrowIfNull(factory);
            Factory = factory;
        }

        public void Run()
        {
            ExecutingController currentController = Factory.Create_MainMenuController();
            UserCommand result = currentController.Initialize();
            if (result == UserCommand.Add_Task)
            {
                ExecutingController addTaskMenuController = Factory.Create_AddTaskMenuController();
                addTaskMenuController.Initialize();
            }
        }
    }
}