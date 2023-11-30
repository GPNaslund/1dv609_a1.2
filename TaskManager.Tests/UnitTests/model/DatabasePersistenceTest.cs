using TaskManager.src.model;
using Task = TaskManager.src.model.Task;

namespace TaskManager.Tests.UnitTests.model 
{
    public class DatabasePerisstenceTest
    {
        [Fact]
        public void Save_ShouldSaveTaskToPersistence()
        {
            Task task = new("A", "B", DateTime.Now);
            DatabasePersistence Sut = new();

            Sut.Save(task);

            Assert.Contains(task, Sut.Read());
        }
    }
}