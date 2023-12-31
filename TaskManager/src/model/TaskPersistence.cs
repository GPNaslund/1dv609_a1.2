namespace TaskManager.src.model
{
    public interface TaskPersistence
    {
        public void Save(Task taskToSave);

        public void Delete(Task taskToDelete);

        public void Update(Task taskToUpdatea);

        public List<Task> Read();
    }
}