using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TaskManager.src.model;
using TaskManager.src.view;

namespace TaskManager.src.controller
{
    public class DefaultControllerFactory
    {
        public ExecutingController Create_MainMenuController()
        {
            DefaultConsoleService ConsoleService = new();
            ConsoleView View = new(ViewType.Main_Menu,ConsoleService);
            MainMenuController mainMenuController = new(View);
            return mainMenuController;
        }

        public ExecutingController Create_AddTaskController()
        {
            DefaultConsoleService ConsoleService = new();
            ConsoleView View = new(ViewType.Add_Task_View,ConsoleService);
            var options = new DbContextOptionsBuilder<AppDatabaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
            AppDatabaseContext DbContext = new(options);
            DatabasePersistence databasePersistence = new(DbContext);
            TaskService TaskService = new(databasePersistence);
            AddTaskController addTaskController = new(View, TaskService);
            return addTaskController;
        }

        public ExecutingController Create_EditTaskController()
        {
            DefaultConsoleService ConsoleService = new();
            ConsoleView View = new(ViewType.Edit_Task,ConsoleService);
            var options = new DbContextOptionsBuilder<AppDatabaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
            AppDatabaseContext DbContext = new(options);
            DatabasePersistence databasePersistence = new(DbContext);
            TaskService TaskService = new(databasePersistence);
            EditTaskController editTaskController = new(View, TaskService);
            return editTaskController;
        }
    }
}