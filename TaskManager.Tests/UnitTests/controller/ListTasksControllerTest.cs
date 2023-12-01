using View = TaskManager.src.view.View;
using Moq;
using TaskManager.src.controller;
using TaskManager.src.model;

namespace TaskManager.Tests.UnitTests.controller
{
    public class ListTasksControllerTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                ListTasksController Sut = new(null, null);
            });
        }

        [Fact]
        public void Initialize_ShouldDisplayHeaderAndMenu()
        {
            Mock<View> MockView = new Mock<View>();
            Mock<ITaskService> MockTaskService = new Mock<ITaskService>();
            MockView.Setup(obj => obj.GetInput(It.IsAny<string>())).Returns("0");

            ListTasksController Sut = new(MockView.Object, MockTaskService.Object);

            Sut.Initialize();

            MockView.Verify(obj => obj.DisplayHeader(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.DisplayMenu(), Times.AtLeastOnce());
        }

        [Theory]
        [InlineData("1", ListByCommand.List_By_Due_Date)]
        [InlineData("2", ListByCommand.List_Incomplete_Tasks)]
        [InlineData("3", ListByCommand.List_Completed_Tasks)]
        [InlineData("4", ListByCommand.List_Expired_Tasks)]
        public void Initialize_ShouldCallServiceWithListByCommand_BasedOnUserInput(string input, ListByCommand command)
        {
            Mock<View> MockView = new Mock<View>();
            Mock<ITaskService> MockTaskService = new Mock<ITaskService>();
            MockView.Setup(obj => obj.GetInput(It.IsAny<string>())).Returns(input);

            ListTasksController Sut = new(MockView.Object, MockTaskService.Object);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.ListTasksBy(command));
        }
    }
}