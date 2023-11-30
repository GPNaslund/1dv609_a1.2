using Microsoft.EntityFrameworkCore;
using TaskManager.src.model;
using Task = TaskManager.src.model.Task;

namespace TaskManager.Tests.UnitTests.model
{
    public class DatabasePerisstenceTest
    {
        private readonly DatabasePersistence Sut;
        private readonly AppDatabaseContext Context;

        public DatabasePerisstenceTest()
        {
            var options = new DbContextOptionsBuilder<AppDatabaseContext>()
                    .UseInMemoryDatabase(databaseName: "TestDb")
                    .Options;

            Context = new AppDatabaseContext(options);
            Sut = new(Context);
        }

        [Fact]
        public void Save_ShouldSaveTaskToPersistence()
        {

            Task task = new("A", "B", DateTime.Now);

            Sut.Save(task);

            Assert.Contains(task, Sut.Read());
        }

        [Fact]
        public void Save_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                Sut.Save(null);
            });
        }
    }
}