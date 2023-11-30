using System.Reflection.Metadata.Ecma335;

namespace TaskManager.src.model
{
    public class Task
    {
        private string _name;
        public string Name 
        {
            get => _name;
            set => ValidateName(value);
        }
        public Task(string name, string description, DateTime dueDate)
        {
            Name = name;
        }

        private string ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("A tasks name cannot be empty!");
            }
            return name;
        }
    }
}