using View = TaskManager.src.view.View;
using Moq;
using TaskManager.src.controller;
using TaskManager.src.model;

namespace TaskManager.Tests.UnitTests.controller
{
    public class ListTasksControllerTest
    {
        private readonly ListTasksController Sut;
        private readonly Mock<View> MockView;
        private readonly Mock<ITaskService> MockTaskService;

        public ListTasksControllerTest()
        {
            MockView = new Mock<View>();
            MockTaskService = new Mock<ITaskService>();
            Sut = new(MockView.Object, MockTaskService.Object);
        }
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ListTasksController Sut = new(null, null);
            });
        }

        [Fact]
        public void Initialize_ShouldDisplayHeaderAndMenu()
        {
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
            Queue<string> allInputs = new Queue<string>(new[] { input, "0" });
            MockView.Setup(obj => obj.GetInput(It.IsAny<string>())).Returns(() => allInputs.Dequeue());

            ListTasksController Sut = new(MockView.Object, MockTaskService.Object);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.ListTasksBy(command));

        }

        [Fact]
        public void Initialize_ShouldRepromptForTypeOfListing_OnInvalidValue()
        {
            Queue<string> allChoices = new Queue<string>(new[] { "ö", "1", "0" });
            MockView.Setup(m => m.GetInput("Your choice: ")).Returns(() => allChoices.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.ListTasksBy(It.IsAny<ListByCommand>()), Times.Once());
        }
    }
}