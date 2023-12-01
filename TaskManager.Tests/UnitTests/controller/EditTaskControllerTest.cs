using View = TaskManager.src.view.View;
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
            MockTaskService.Setup(obj => obj.GetAllTasks()).Returns([]);

            EditTaskController Sut = new EditTaskController(MockView.Object, MockTaskService.Object);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.GetAllTasks(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.GetInput("Select task: "), Times.AtLeastOnce());
        }
    }
}