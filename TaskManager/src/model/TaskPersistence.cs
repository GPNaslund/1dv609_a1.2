namespace TaskManager.src.model
{
    public interface TaskPersistence
    {
        public void Save(Task taskToSave);

        public void Delete(Task taskToDelete);

        public void Update(Task taskToUpdate);

        public List<Task> Read();
    }
}