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
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                DatabasePersistence Sut = new(null);
            });
        }

        [Fact]
        public void Save_ShouldSaveTaskToPersistence()
        {

            Task task = CreateTestTask();

            Sut.Save(task);

            Assert.Contains(task, Sut.Read());
        }

        [Fact]
        public void Save_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Sut.Save(null);
            });
        }

        [Fact]
        public void Delete_ShouldDeleteInstanceSuccessfully()
        {
            Task task = CreateTestTask();
            Sut.Save(task);
            Sut.Delete(task);
            Assert.DoesNotContain(task, Sut.Read());
        }

        private Task CreateTestTask(string name = "A", string description = "B")
        {
            return new Task(name, description, DateTime.Now);
        }

    }
}