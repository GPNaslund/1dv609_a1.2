using View = TaskManager.src.view.View;
using Task = TaskManager.src.model.Task;
using TaskManager.src.controller;
using Moq;
using TaskManager.src.model;

namespace TaskManager.Tests.UnitTests.controller
{
    public class EditTaskControllerTest
    {
        private readonly EditTaskController Sut;
        private readonly Mock<View> MockView;
        private readonly Mock<ITaskService> MockTaskService;

        public EditTaskControllerTest()
        {
            MockView = new Mock<View>();
            MockTaskService = new Mock<ITaskService>();
            Sut = new(MockView.Object, MockTaskService.Object);
        }
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
            SetupViewSelectTaskInput(["1"]);
            SetupServiceGetTasks_ReturnAmountOfTasks(1);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.GetAllTasks(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.GetInput("Select task: "), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldRepromptUserSelection_OnInvalidInput()
        {
            SetupViewSelectTaskInput(["a", "10", "1"]);
            SetupServiceGetTasks_ReturnAmountOfTasks(1);

            EditTaskController Sut = new EditTaskController(MockView.Object, MockTaskService.Object);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.GetAllTasks(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.GetInput("Select task: "), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldReturnUserCommandMainMenu_OnInputBackOption()
        {
            SetupViewSelectTaskInput(["0"]);
            SetupServiceGetTasks_ReturnAmountOfTasks(0);

            UserCommand result = Sut.Initialize();

            Assert.Equal(UserCommand.Main_Menu, result);
        }

        [Fact]
        public void Initialize_ShouldAllowUserTo_EditName_Successfully()
        {
            SetupViewSelectTaskInput(["1"]);
            SetupServiceGetTasks_ReturnAmountOfTasks(1);

            MockView.Setup(obj => obj.GetInput("Your choice: ")).Returns("1");
            MockView.Setup(obj => obj.GetInput("New name: ")).Returns("C");

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.UpdateTask(It.IsAny<Task>()), Times.Once());
        }

        [Fact]
        public void Initialize_ShouldRepromptUserForName_IfInvalidInput()
        {
            SetupViewSelectTaskInput(["1"]);
            SetupServiceGetTasks_ReturnAmountOfTasks(1);

            MockView.Setup(obj => obj.GetInput("Your choice: ")).Returns("1");
            Queue<string> nameInputs = new Queue<string>(new[] { "", "C" });
            MockView.Setup(obj => obj.GetInput("New name: ")).Returns(() => nameInputs.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.UpdateTask(It.IsAny<Task>()), Times.Once());
        }

        private void SetupViewSelectTaskInput(string[] inputs)
        {
            Queue<string> allInputs = new Queue<string>();
            foreach (string input in inputs)
            {
                allInputs.Enqueue(input);
            }
            MockView.Setup(obj => obj.GetInput("Select task: ")).Returns(() => allInputs.Dequeue());
        }

        private void SetupServiceGetTasks_ReturnAmountOfTasks(int amountOfTasks)
        {
            List<Task> allTasks = [];
            for (int i = 0; i < amountOfTasks; i++)
            {
                allTasks.Add(new Task("A", "B", DateTime.Now));
            }

            MockTaskService.Setup(obj => obj.GetAllTasks()).Returns(allTasks);
        }


    }
}