using View = TaskManager.src.view.View;
using Task = TaskManager.src.model.Task;
using TaskManager.src.controller;
using Moq;
using TaskManager.src.model;

namespace TaskManager.Tests.UnitTests.controller
{
    public class EditTaskControllerTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                EditTaskController Sut = new(null, null);
            });
        }

        [Fact]
        public void Initialize_ShouldAllowUserToSelectATaskToEdit()
        {
            Mock<View> MockView = new Mock<View>();
            MockView.Setup(obj => obj.GetInput(It.IsAny<string>())).Returns("1");
            Mock<ITaskService> MockTaskService = new Mock<ITaskService>();
            MockTaskService.Setup(obj => obj.GetAllTasks()).Returns([new Task("A", "B", DateTime.Now)]);

            EditTaskController Sut = new EditTaskController(MockView.Object, MockTaskService.Object);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.GetAllTasks(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.GetInput("Select task: "), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldRepromptUserSelection_OnInvalidInput()
        {
            Mock<View> MockView = new Mock<View>();
            Queue<string> allInput = new Queue<string>(new[] {"a", "10", "1"});
            MockView.Setup(obj => obj.GetInput("Select task: ")).Returns(() => allInput.Dequeue());
            Mock<ITaskService> MockTaskService = new Mock<ITaskService>();
            MockTaskService.Setup(obj => obj.GetAllTasks()).Returns([new Task("A", "B", DateTime.Now)]);

            EditTaskController Sut = new EditTaskController(MockView.Object, MockTaskService.Object);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.GetAllTasks(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.GetInput("Select task: "), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldReturnUserCommandMainMenu_OnInputBackOption()
        {
            Mock<View> MockView = new Mock<View>();
            MockView.Setup(obj => obj.GetInput("Select task: ")).Returns("0");
            Mock<ITaskService> MockTaskService = new Mock<ITaskService>();
            MockTaskService.Setup(obj => obj.GetAllTasks()).Returns([]);

            EditTaskController Sut = new EditTaskController(MockView.Object, MockTaskService.Object);

            UserCommand result = Sut.Initialize();

            Assert.Equal(UserCommand.Main_Menu, result);
        }
    }
}