using Microsoft.EntityFrameworkCore;
using TaskManager.src.model;
using Task = TaskManager.src.model.Task;

namespace TaskManager.Tests.UnitTests.model
{
    public class DatabasePerisstenceTest
    {
        private readonly DatabasePersistence Sut;
        private AppDatabaseContext context;

        public DatabasePerisstenceTest()
        {
            var options = new DbContextOptionsBuilder<AppDatabaseContext>()
                    .UseInMemoryDatabase(databaseName: "TestDb")
                    .Options;

            AppDatabaseContext context = new AppDatabaseContext(options);
            Sut = new(context);
        }
        [Fact]
        public void Save_ShouldSaveTaskToPersistence()
        {

            Task task = new("A", "B", DateTime.Now);

            Sut.Save(task);

            Assert.Contains(task, Sut.Read());
        }
    }
}