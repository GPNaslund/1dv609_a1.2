namespace TaskManager.src.model
{
    public class DatabasePersistence : TaskPersistence
    {
        private readonly AppDatabaseContext Context;

        public DatabasePersistence(AppDatabaseContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext);
            Context = dbContext;
        }
        public void Save(Task taskToSave)
        {
            ArgumentNullException.ThrowIfNull(taskToSave);
            Context.Tasks.Add(taskToSave);
            Context.SaveChanges();
        }

        public List<Task> Read()
        {
            return Context.Tasks.ToList();
        }

        public void Delete(Task taskToDelete)
        {
            Context.Tasks.Remove(taskToDelete);
            Context.SaveChanges();
        }

        public void Update(Task taskToUpdate)
        {
            /*
                EF Core tracks entities (in this case task objects)
                which means changes to an entity (in this case done in the controller)
                are allready tracked, and just needs to be saved to persist.
            */
            Context.SaveChanges();
        }
    }
}