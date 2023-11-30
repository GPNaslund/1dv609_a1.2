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

        public void DeleteTask(Task taskToDelete)
        {
            Persistence.Delete(taskToDelete);
        }

        public void UpdateTask(Task taskToUpdate)
        {
            Persistence.Update(taskToUpdate);
        }

        public List<Task> GetAllTasks()
        {
            return null;
        }
    }
}