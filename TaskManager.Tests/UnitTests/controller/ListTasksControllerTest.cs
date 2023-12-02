using View = TaskManager.src.view.View;
using Task = TaskManager.src.model.Task;
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
            MockTaskService.Setup(obj => obj.ListTasksBy(It.IsAny<ListByCommand>())).Returns([]);
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

            Sut.Initialize();

            MockView.Verify(obj => obj.DisplayHeader(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.DisplayMenu(), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldReturnUserCommandMainMenu_OnSpecificChoice()
        {
            MockView.Setup(obj => obj.GetInput(It.IsAny<string>())).Returns("0");

            UserCommand result = Sut.Initialize();

            Assert.Equal(UserCommand.Main_Menu, result);
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
            MockView.Setup(obj => obj.GetInput("Your choice: ")).Returns(() => allChoices.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.ListTasksBy(It.IsAny<ListByCommand>()), Times.Once());
        }

        [Theory]
        [InlineData("1", 0)]
        [InlineData("1", 1)]
        [InlineData("1", 10)]
        [InlineData("2", 0)]
        [InlineData("2", 1)]
        [InlineData("2", 10)]
        [InlineData("3", 0)]
        [InlineData("3", 1)]
        [InlineData("3", 10)]
        [InlineData("4", 0)]
        [InlineData("4", 1)]
        [InlineData("4", 10)]
        public void Initialize_ShouldDisplayTheTasksReturnedByService_Correctly(string input, int amountOfTasks)
        {
            List<Task> allTasks = GenerateTasks(amountOfTasks);
            MockTaskService.Setup(obj => obj.ListTasksBy(It.IsAny<ListByCommand>())).Returns(allTasks);
            Queue<string> allInputs = new Queue<string>(new [] { input, "0" });
            MockView.Setup(obj => obj.GetInput(It.IsAny<string>())).Returns(() => allInputs.Dequeue());

            Sut.Initialize();
            
            int amountOfPrints = amountOfTasks == 0 ? 1 : amountOfTasks;
            MockView.Verify(obj => obj.DisplayMessage(It.IsAny<string>()), Times.AtLeast(amountOfPrints));
        }

        private List<Task> GenerateTasks(int amount)
        {
            List<Task> allTasks = [];
            for (int i = 0; i < amount; i++)
            {
                allTasks.Add(new Task("A", "B", DateTime.Now));
            }
            return allTasks;
        }

        [Fact]
        public void Initialize_ShouldDisplayMessage_OnTaskServiceException()
        {
            Queue<string> allInput = new Queue<string>(new[] { "1", "0" });
            MockView.Setup(obj => obj.GetInput(It.IsAny<string>())).Returns(() => allInput.Dequeue());
            MockTaskService.Setup(obj => obj.ListTasksBy(It.IsAny<ListByCommand>())).Throws<Exception>();

            Sut.Initialize();

            MockView.Verify(obj => obj.DisplayMessage(It.IsAny<string>()), Times.AtLeastOnce());
        }
    }
}