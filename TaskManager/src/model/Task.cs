using System.Reflection.Metadata.Ecma335;

namespace TaskManager.src.model
{
    public class Task
    {
        private string _name;
        public string Name 
        {
            get => _name;
            set => _name = ValidateName(value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => _description = value ?? throw new ArgumentNullException();
        }
        public Task(string name, string description, DateTime dueDate)
        {
            Name = name;
            Description = description;
        }

        private string ValidateName(string name)
        {
            ArgumentNullException.ThrowIfNull(name);
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("A tasks name cannot be empty!");
            }
            return name;
        }
    }
}