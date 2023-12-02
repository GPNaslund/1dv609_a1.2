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
            currentController.Initialize();
        }
    }
}