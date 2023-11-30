using Moq;
using TaskManager.src.model;
using Task = TaskManager.src.model.Task;

namespace TaskManager.Tests.UnitTests.model
{
    public class TaskServiceTest
    {
        private readonly Mock<TaskPersistence> MockPersistence;
        private readonly TaskService Sut;

        public TaskServiceTest()
        {
            MockPersistence = new Mock<TaskPersistence>();
            Sut = new TaskService(MockPersistence.Object);
        }

        [Fact]
        public void CreateTask_ShouldCreateTaskAndCallPersistenceToSaveTheTask()
        {
            Task result = Sut.CreateTask("A", "B", DateTime.Now);

            Assert.Equal(new Task("A", "B", DateTime.Now), result);
            MockPersistence.Verify(obj => obj.Save(It.IsAny<Task>()), Times.Once());
        }

        [Fact]
        public void DeleteTask_ShouldCallPersistenceToDeleteTheTask()
        {
            Sut.DeleteTask(new Task("A", "B", DateTime.Now));

            MockPersistence.Verify(obj => obj.Delete(It.IsAny<Task>()), Times.Once());
        }

    }
}