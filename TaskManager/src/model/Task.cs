using System.Reflection.Metadata.Ecma335;

namespace TaskManager.src.model
{
    public class Task
    {
        public Task(string name, string description, DateTime dueDate)
        {
            throw new ArgumentException();
        }
    }
}