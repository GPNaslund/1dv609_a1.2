namespace TaskManager.src.model
{
    public class TaskService
    {
        public Task CreateTask(string name, string description, DateTime dueDate)
        {
            return new Task(name, description, dueDate);
        }
    }
}