using TaskManager.src.model.exceptions;

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
            return Persistence.Read();
        }

        public List<Task> ListTasksBy(ListByCommand command)
        {
           List<Task> tasks = Persistence.Read();
            switch (command)
            {
                case ListByCommand.List_By_Due_Date:
                    return [.. tasks.OrderBy(task => task.DueDate)];
                case ListByCommand.List_Incomplete_Tasks:
                    return tasks.Where(task => task.Status == TaskStatus.Not_Completed).ToList();
                case ListByCommand.List_Completed_Tasks:
                    return tasks.Where(task => task.Status == TaskStatus.Completed).ToList();
                case ListByCommand.List_Expired_Tasks:
                    return tasks.Where(task => task.DueDate.CompareTo(DateTime.Now) <= 0).ToList();
                default:
                    throw new ListByCommandNotImplementedException();
            }
        }
    }
}