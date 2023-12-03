namespace TaskManager.src.controller
{
    public interface ControllerFactory
    {
        public ExecutingController Create_MainMenuController();

        public ExecutingController Create_AddTaskController();

        public ExecutingController Create_ViewAllTasksController();

        public ExecutingController Create_ListTasksController();

        public ExecutingController Create_EditTaskController();
    }
}