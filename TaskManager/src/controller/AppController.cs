namespace TaskManager.src.controller
{
    public class AppController 
    {
        public AppController(TaskFactory factory)
        {
            ArgumentNullException.ThrowIfNull(factory);
        }
    }
}