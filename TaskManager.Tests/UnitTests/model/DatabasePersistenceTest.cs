using Microsoft.EntityFrameworkCore;
using TaskManager.src.model;
using Task = TaskManager.src.model.Task;

namespace TaskManager.Tests.UnitTests.model
{
    public class DatabasePerisstenceTest
    {
        private readonly DatabasePersistence Sut;
        private readonly AppDatabaseContext Context;

        private readonly Task TestTask;

        public DatabasePerisstenceTest()
        {
            var options = new DbContextOptionsBuilder<AppDatabaseContext>()
                    .UseInMemoryDatabase(databaseName: "TestDb")
                    .Options;

            Context = new AppDatabaseContext(options);
            Sut = new(Context);
            TestTask = new("A", "B", DateTime.Now);
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
            Sut.Save(TestTask);

            Assert.Contains(TestTask, Sut.Read());
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
            Sut.Save(TestTask);
            Sut.Delete(TestTask);
            Assert.DoesNotContain(TestTask, Sut.Read());
        }

        [Fact]
        public void Update_ShouldSaveChangesToPersistence()
        {
            Sut.Save(TestTask);

            TestTask.Name = "New";
            TestTask.Description = "New";

            Sut.Update();

            Assert.Contains(TestTask, Sut.Read());

        }

    }
}