using Microsoft.EntityFrameworkCore;
using TaskManager.src.controller;
using TaskManager.src.model;

namespace TaskManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var DbPath = System.IO.Path.Join(path, "task-data.db");
            var options = new DbContextOptionsBuilder<AppDatabaseContext>()
                .UseSqlite($"Data Source={DbPath}")
                .Options;
            DefaultControllerFactory factory = new DefaultControllerFactory(options);
            AppController mainController = new AppController(factory);
            mainController.Run();
        }
    }
}
