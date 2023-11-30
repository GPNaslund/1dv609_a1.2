using Moq;
using TaskManager.src.model;
using Task = TaskManager.src.model.Task;

namespace TaskManager.Tests.UnitTests.model
{
    public class TaskServiceTest
    {
        private readonly Mock<TaskPersistence> MockPersistence;

        public TaskServiceTest()
        {
            MockPersistence = new Mock<TaskPersistence>();
        }
        [Fact]
        public void CreateTask_ShouldCreateTaskAndCallPersistenceToSaveTheTask()
        {
            TaskService sut = new TaskService(MockPersistence.Object);

            Task result = sut.CreateTask("A", "B", DateTime.Now);

            Assert.Equal(new Task("A", "B", DateTime.Now), result);
            MockPersistence.Verify(obj => obj.Save(It.IsAny<Task>()), Times.Once());
        }
    }
}