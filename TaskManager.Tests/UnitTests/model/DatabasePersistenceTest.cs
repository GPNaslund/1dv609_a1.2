using Microsoft.EntityFrameworkCore;
using TaskManager.src.model;
using Task = TaskManager.src.model.Task;

namespace TaskManager.Tests.UnitTests.model
{
    public class DatabasePerisstenceTest
    {
        [Fact]
        public void Save_ShouldSaveTaskToPersistence()
        {
            var options = new DbContextOptionsBuilder<AppDatabaseContext>()
                                .UseInMemoryDatabase(databaseName: "TestDb")
                                .Options;

            AppDatabaseContext context = new AppDatabaseContext(options);

            Task task = new("A", "B", DateTime.Now);
            DatabasePersistence Sut = new(context);

            Sut.Save(task);

            Assert.Contains(task, Sut.Read());
        }
    }
}