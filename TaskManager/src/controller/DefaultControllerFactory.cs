using Microsoft.EntityFrameworkCore;
using TaskManager.src.model;
using TaskManager.src.view;

namespace TaskManager.src.controller
{
    public class DefaultControllerFactory
    {
        private readonly DefaultConsoleService ConsoleService;
        private readonly TaskService TaskService;

        public DefaultControllerFactory(DbContextOptions<AppDatabaseContext> options)
        {
            ConsoleService = new();
            AppDatabaseContext DbContext = new(options);
            DatabasePersistence Persistence = new(DbContext);
            TaskService = new(Persistence);
        }


        public ExecutingController Create_MainMenuController()
        {
            DefaultConsoleService ConsoleService = new();
            ConsoleView View = new(ViewType.Main_Menu,ConsoleService);
            MainMenuController mainMenuController = new(View);
            return mainMenuController;
        }

        public ExecutingController Create_AddTaskController()
        {
            ConsoleView View = new(ViewType.Add_Task_View,ConsoleService);
            AddTaskController addTaskController = new(View, TaskService);
            return addTaskController;
        }

        public ExecutingController Create_EditTaskController()
        {
            ConsoleView View = new(ViewType.Edit_Task,ConsoleService);
            EditTaskController editTaskController = new(View, TaskService);
            return editTaskController;
        }

        public ExecutingController Create_ViewAllTasksController()
        {
            ConsoleView View = new(ViewType.View_All_Tasks, ConsoleService);
            ViewAllTasksController viewAllTasksController = new(View, TaskService);
            return viewAllTasksController;
        }
    }
}