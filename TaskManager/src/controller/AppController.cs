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
            Factory.Create_MainMenuController().Initialize();
        }
    }
}