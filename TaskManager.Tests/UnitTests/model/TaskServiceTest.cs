using Moq;
using TaskManager.src.model;
using Task = TaskManager.src.model.Task;

namespace TaskManager.Tests.UnitTests.model
{
    public class TaskServiceTest
    {
        private readonly Mock<TaskPersistence> MockPersistence;
        private readonly TaskService Sut;

        private readonly Task TestTask;

        public TaskServiceTest()
        {
            MockPersistence = new Mock<TaskPersistence>();
            Sut = new TaskService(MockPersistence.Object);
            TestTask = new Task("A", "B", DateTime.Now);
        }

        [Fact]
        public void CreateTask_ShouldCreateTaskAndCallPersistenceToSaveTheTask()
        {
            Task result = Sut.CreateTask(TestTask.Name, TestTask.Description, TestTask.DueDate);

            Assert.Equal(TestTask, result);
            MockPersistence.Verify(obj => obj.Save(TestTask), Times.Once());
        }

        [Fact]
        public void DeleteTask_ShouldCallPersistenceToDeleteTheTask()
        {
            Sut.DeleteTask(TestTask);

            MockPersistence.Verify(obj => obj.Delete(TestTask), Times.Once());
        }

        [Fact]
        public void UpdateTask_ShouldCallPersistenceToUpdateTheTask()
        {
            Sut.UpdateTask(TestTask);

            MockPersistence.Verify(obj => obj.Update(TestTask), Times.Once());
        }

        [Fact]
        public void GetAllTasks_ShouldCallPersistenceToGetAllTasks()
        {
            Sut.GetAllTasks();

            MockPersistence.Verify(obj => obj.Read(), Times.Once());
        }

        [Fact]
        public void ListTasksBy_ShouldReturnAllTasksListed_SpecificToListByCommand()
        {
            List<Task> result = Sut.ListTasksBy(ListByCommand.List_By_Due_Date);

            for (int i = 0; i < result.Count - 1; i++)
                    {
                        Assert.True(result[i].DueDate <= result[i + 1].DueDate);
                    }
        }
    }
}