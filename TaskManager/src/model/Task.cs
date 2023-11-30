using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualBasic;

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
            set => _description = ValidateDescription(value);
        }

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get => _dueDate;
            set => throw new ArgumentException();
        }
        public Task(string name, string description, DateTime dueDate)
        {
            Name = name;
            Description = description;
            DueDate = dueDate;
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

        private string ValidateDescription(string description)
        {
            ArgumentNullException.ThrowIfNull(description);
            return description;
        }
    }
}