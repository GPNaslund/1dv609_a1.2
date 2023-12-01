namespace TaskManager.src.model
{
    public interface ITaskService
    {
        public Task CreateTask(string name, string description, DateTime dueDate);

        public void DeleteTask(Task taskToDelete);

        public void UpdateTask(Task taskToUpdate);

        public List<Task> GetAllTasks();

        public List<Task> ListTasksBy(ListByCommand command);
    }
}