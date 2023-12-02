namespace TaskManager.src.controller
{
    public interface ControllerFactory
    {
        public ExecutingController Create_MainMenuController();

        public ExecutingController Create_AddTaskMenuController();

        public ExecutingController Create_ViewAllTasksMenuController();

        public ExecutingController Create_ListTasksMenuController();

        public ExecutingController Create_EditTaskMenuController();
    }
}