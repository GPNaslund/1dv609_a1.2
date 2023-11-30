namespace TaskManager.src.model
{
    public class DatabasePersistence
    {
        private readonly AppDatabaseContext Context;

        public DatabasePersistence(AppDatabaseContext dbContext)
        {
            Context = dbContext;
        }
        public void Save(Task taskToSave)
        {
            Context.Tasks.Add(taskToSave);
            Context.SaveChanges();
        }

        public List<Task> Read()
        {
            return Context.Tasks.ToList();
        }
    }
}