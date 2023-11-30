namespace TaskManager.src.model
{
    public interface TaskPersistence
    {
        public void Save(Task taskToSave);
    }
}