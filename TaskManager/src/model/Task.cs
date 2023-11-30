using TaskStatus = TaskManager.src.model.TaskStatus;

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
            set => _dueDate = ValidateDueDate(value);
        }

        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get => _creationDate;
        }

        private TaskStatus _status;
        public TaskStatus Status {
            get => _status;
            set => _status = ValidateTaskStatus(value);
        }

        public Task(string name, string description, DateTime dueDate)
        {
            Name = name;
            Description = description;
            _creationDate = DateTime.Now;
            DueDate = dueDate;
            _status = TaskStatus.Not_Completed;

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

        private DateTime ValidateDueDate(DateTime dueDate)
        {
            if (DateTime.Compare(dueDate.Date, CreationDate.Date) < 0)
            {
                throw new ArgumentException("Due date cannot be before todays date");
            }
            return dueDate;
        }

        private TaskStatus ValidateTaskStatus(TaskStatus status)
        {
            ArgumentNullException.ThrowIfNull(status);
            return status;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Description: {Description}, Due Date: {DueDate}";
        }
    }
}