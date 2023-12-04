using Microsoft.EntityFrameworkCore;
using TaskManager.src.model;
using TaskManager.src.view;

namespace TaskManager.src.controller
{
    public class DefaultControllerFactory : ControllerFactory
    {
        private readonly DefaultConsoleService ConsoleService;
        private readonly TaskService TaskService;

        private readonly IViewManager ViewManager;

        public DefaultControllerFactory(DbContextOptions<AppDatabaseContext> options, IViewManager viewManager)
        {
            ConsoleService = new();
            AppDatabaseContext DbContext = new(options);
            DatabasePersistence Persistence = new(DbContext);
            TaskService = new(Persistence);
            ViewManager = viewManager;
        }


        public ExecutingController Create_MainMenuController()
        {
            DefaultConsoleService ConsoleService = new();
            ConsoleView View = new(ViewManager.GetViewData(ViewType.Main_Menu), ConsoleService);
            MainMenuController mainMenuController = new(View, ViewManager.GetViewData(ViewType.Main_Menu));
            return mainMenuController;
        }

        public ExecutingController Create_AddTaskController()
        {
            ConsoleView View = new(ViewManager.GetViewData(ViewType.Add_Task), ConsoleService);
            AddTaskController addTaskController = new(View, TaskService, ViewManager.GetViewData(ViewType.Add_Task));
            return addTaskController;
        }

        public ExecutingController Create_EditTaskController()
        {
            ConsoleView View = new(ViewManager.GetViewData(ViewType.Edit_Task), ConsoleService);
            EditTaskController editTaskController = new(View, TaskService, ViewManager.GetViewData(ViewType.Edit_Task));
            return editTaskController;
        }

        public ExecutingController Create_ViewAllTasksController()
        {
            ConsoleView View = new(ViewManager.GetViewData(ViewType.View_All_Tasks), ConsoleService);
            ViewAllTasksController viewAllTasksController = new(View, TaskService, ViewManager.GetViewData(ViewType.View_All_Tasks));
            return viewAllTasksController;
        }

        public ExecutingController Create_ListTasksController()
        {
            ConsoleView View = new(ViewManager.GetViewData(ViewType.List_Tasks), ConsoleService);
            ListTasksController listTasksController = new(View, TaskService, ViewManager.GetViewData(ViewType.List_Tasks));
            return listTasksController;
        }
    }
}