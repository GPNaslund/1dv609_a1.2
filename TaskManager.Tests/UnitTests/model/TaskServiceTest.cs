using TaskManager.src.model;
using Task = TaskManager.src.model.Task;

namespace TaskManager.Tests.UnitTests.model
{
    public class TaskServiceTest
    {
        [Fact]
        public void CreateTask_ShouldCreateTaskAndCallPersistenceToSaveTheTask()
        {
            TaskService sut = new TaskService();
            Task result = sut.CreateTask("A", "B", DateTime.Now);
            Assert.Equal(new Task("A", "B", DateTime.Now), result);
        }
    }
}