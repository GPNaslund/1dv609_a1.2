namespace TaskManager.src.model
{
    public class TaskService
    {
        private readonly TaskPersistence Persistence;

        public TaskService(TaskPersistence persistence)
        {
            Persistence = persistence;
        }
        public Task CreateTask(string name, string description, DateTime dueDate)
        {
            Task taskToSave = new Task(name, description, dueDate);
            Persistence.Save(taskToSave);
            return taskToSave;
        }
    }
}