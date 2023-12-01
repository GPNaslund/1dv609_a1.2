using View = TaskManager.src.view.View;
using Task = TaskManager.src.model.Task;
using Moq;
using TaskManager.src.controller;
using TaskManager.src.model;

namespace TaskManager.Tests.UnitTests.controller
{
    public class ViewAllTasksControllerTest
    {
        private readonly ViewAllTasksController Sut;
        private readonly Mock<View> MockView;
        private readonly Mock<ITaskService> MockTaskService;

        public ViewAllTasksControllerTest()
        {
            MockView = new Mock<View>();
            MockTaskService = new Mock<ITaskService>();
            Sut = new(MockView.Object, MockTaskService.Object);
        }
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                ViewAllTasksController Sut = new(null, null);
            });
        }

        [Fact]
        public void Initialize_ShouldDisplayHeader()
        {
            MockTaskService.Setup(obj => obj.GetAllTasks()).Returns([]);

            Sut.Initialize();

            MockView.Verify(obj => obj.DisplayHeader(), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldCallTaskService()
        {
            MockTaskService.Setup(obj => obj.GetAllTasks()).Returns([]);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.GetAllTasks(), Times.AtLeastOnce());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        public void Initialize_ShouldDisplayTasksReturnedByService(int amountOfTasks)
        {
            List<Task> tasksToReturn = GenerateTasks(amountOfTasks);
            MockTaskService.Setup(obj => obj.GetAllTasks()).Returns(tasksToReturn);

            Sut.Initialize();

            MockView.Verify(obj => obj.DisplayMessage(It.IsAny<string>()), Times.Exactly(amountOfTasks));
        }

        private List<Task> GenerateTasks(int amountOfTasks)
        {
            List<Task> tasksToReturn = [];
            for (int i = 0; i < amountOfTasks; i++)
            {
                tasksToReturn.Add(new Task("A", "B", DateTime.Now));
            }
            return tasksToReturn;
        }

        [Fact]
        public void Initialize_ShouldReturnMainMenuUserCommand_WhenDone()
        {
            MockTaskService.Setup(obj => obj.GetAllTasks()).Returns([]);

            UserCommand result = Sut.Initialize();

            Assert.Equal(UserCommand.Main_Menu, result);
        }
    }
}