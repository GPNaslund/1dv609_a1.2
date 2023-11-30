using Moq;
using TaskManager.src.model;
using Task = TaskManager.src.model.Task;
using TaskStatus = TaskManager.src.model.TaskStatus;

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
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                TaskService Sut = new(null);
            });
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

        [Theory]
        [InlineData(ListByCommand.List_Completed_Tasks)]
        [InlineData(ListByCommand.List_Incomplete_Tasks)]
        [InlineData(ListByCommand.List_Expired_Tasks)]
        [InlineData(ListByCommand.List_By_Due_Date)]
        public void ListTasksBy_ShouldReturnAllTasksListed_SpecificToListByCommand(ListByCommand command)
        {
            TestListTasksBy(command);
        }

        private void TestListTasksBy(ListByCommand listingCommand)
        {
            List<Task> unsortedTasks = new List<Task>{
                new ("2 days after tomorrow", "2 days after tomorrow", DateTime.Today.AddDays(3)),
                new ("Today", "Today", DateTime.Today),
                new ("1 day after tomorrow", "1 day after tomorrow", DateTime.Today.AddDays(2)),
                new ("Tomorrow", "Tomorrow", DateTime.Today.AddDays(1))
            };
            unsortedTasks[1].Status = TaskStatus.Completed;
            unsortedTasks[3].Status = TaskStatus.Completed;
            MockPersistence.Setup(m => m.Read()).Returns(unsortedTasks);

            List<Task> result = Sut.ListTasksBy(listingCommand);

            switch (listingCommand)
            {
                case ListByCommand.List_By_Due_Date:
                    for (int i = 0; i < result.Count - 1; i++)
                    {
                        Assert.True(result[i].DueDate <= result[i + 1].DueDate);
                    }
                    break;
                case ListByCommand.List_Incomplete_Tasks:
                    Assert.True(result.All(task => task.Status == TaskStatus.Not_Completed));
                    break;
                case ListByCommand.List_Completed_Tasks:
                    Assert.True(result.All(task => task.Status == TaskStatus.Completed));
                    break;
                case ListByCommand.List_Expired_Tasks:
                    Assert.True(result.All(task => task.DueDate.CompareTo(DateTime.Now) <= 0 ));
                    break;
            }
        }
    }
}