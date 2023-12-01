using View = TaskManager.src.view.View;

namespace TaskManager.src.controller
{
    public class AddTaskController
    {
        private View View;
        public AddTaskController(View view)
        {
            ArgumentNullException.ThrowIfNull(view);
            View = view;
        }

        public void Initialize()
        {
            string name = View.GetInput("Enter the name: ");
            string description = View.GetInput("Enter the description: ");
            string dueDateString = View.GetInput("Enter due date (yymmdd): ");
            
        }
    }
}