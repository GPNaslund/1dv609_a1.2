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
            Assert.Throws<ArgumentNullException>(() =>
            {
                EditTaskController Sut = new(null, null);
            });
        }

        [Fact]
        public void Initialize_ShouldAllowUserToSelectATaskToEdit()
        {
            SelectTaskInput(["1"]);
            SetupTaskService_ReturnTasks(1);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.GetAllTasks(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.GetInput("Select task: "), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldRepromptUserSelection_OnInvalidInput()
        {
            SelectTaskInput(["a", "10", "1"]);
            SetupTaskService_ReturnTasks(1);

            EditTaskController Sut = new EditTaskController(MockView.Object, MockTaskService.Object);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.GetAllTasks(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.GetInput("Select task: "), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldReturnUserCommandMainMenu_OnInputBackOption()
        {
            SelectTaskInput(["0"]);
            SetupTaskService_ReturnTasks(0);

            UserCommand result = Sut.Initialize();

            Assert.Equal(UserCommand.Main_Menu, result);
        }

        [Fact]
        public void Initialize_ShouldReturnUserCommandMainMenu_OnBackInput_FromEditActionMenu()
        {
            SelectTaskInput(["1"]);
            SetupTaskService_ReturnTasks(1);
            MockView.Setup(obj => obj.GetInput("Your choice: ")).Returns("0");

            UserCommand result = Sut.Initialize();

            Assert.Equal(UserCommand.Main_Menu, result);
        }

        [Fact]
        public void Initialize_ShouldAllowUserTo_EditName_Successfully()
        {
            TestEditName(["C"]);
        }

        [Fact]
        public void Initialize_EditName_ShouldRepromptUserForName_IfInvalidInput()
        {
            TestEditName(["", "C"]);
        }

        [Fact]
        public void Initialize_ShouldAllowUserTo_EditDescription_Successfully()
        {
            TestEditDescription(["D"]);
        }

        [Fact]
        public void Initialize_EditDescription_ShouldRepromptForDescription_OnInvalidValue()
        {
            TestEditDescription([null, "D"]);
        }

        [Fact]
        public void Initialize_ShouldAllowUserTo_EditDueDate_Successfully()
        {
            string dateInput = DateTime.Now.ToString("yyMMdd");
            TestEditDueDate([dateInput]);

            MockView.Verify(obj => obj.GetInput("New due date (yymmdd): "), Times.Once());
        }

        [Fact]
        public void Initialize_EditDueDate_ShouldRepromptForDueDate_OnInvalidValue()
        {
            string dateInput = DateTime.Now.ToString("yyMMdd");
            TestEditDueDate([null, dateInput]);
            
            MockView.Verify(obj => obj.GetInput("New due date (yymmdd): "), Times.Exactly(2));
        }

        [Fact]
        public void Initialize_ShouldAllowUserTo_EditStatus_Successfully()
        {
            TestEditStatus(["1"]);
        }

        [Fact]
        public void Initialize_EditStatus_ShouldReprompt_OnInvalidInput()
        {
            TestEditStatus([null, "1"]);
        }

        [Fact]
        public void Initialize_ShouldAllowUserTo_DeleteTask_Successfully()
        {
            SelectTaskInput(["1"]);
            SetupTaskService_ReturnTasks(1);

            MockView.Setup(obj => obj.GetInput("Your choice: ")).Returns("5");
            MockView.Setup(obj => obj.GetInput("Are you sure? y/n")).Returns("y");

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.DeleteTask(It.IsAny<Task>()), Times.Once());
        }

        [Fact]
        public void Initialize_DeleteTask_ShouldReprompt_OnInvalidValue()
        {
            SelectTaskInput(["1"]);
            SetupTaskService_ReturnTasks(1);

            MockView.Setup(obj => obj.GetInput("Your choice: ")).Returns("5");
            Queue<string> deletionInputs = new Queue<string>(new[] {null, "y"});
            MockView.Setup(obj => obj.GetInput("Are you sure? y/n")).Returns(() => deletionInputs.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.DeleteTask(It.IsAny<Task>()), Times.Once());
        }

        private void TestEditStatus(string[] statusInputs)
        {
            SelectTaskInput(["1"]);
            SetupTaskService_ReturnTasks(1);
            
            MockView.Setup(obj => obj.GetInput("Your choice: ")).Returns("4");
            Queue<string> allStatusInputs = new Queue<string>();
            foreach (string input in statusInputs)
            {
                allStatusInputs.Enqueue(input);
            }
            MockView.Setup(obj => obj.GetInput("Select new status: ")).Returns(() => allStatusInputs.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.UpdateTask(It.IsAny<Task>()), Times.Once());
        }

        private void TestEditDueDate(string[] descriptionInputs)
        {
            SelectTaskInput(["1"]);
            SetupTaskService_ReturnTasks(1);

            MockView.Setup(obj => obj.GetInput("Your choice: ")).Returns("3");
            Queue<string> allDateInputs = new Queue<string>();
            foreach (string description in descriptionInputs)
            {
                allDateInputs.Enqueue(description);
            }
            MockView.Setup(obj => obj.GetInput("New due date (yymmdd): ")).Returns(() => allDateInputs.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.UpdateTask(It.IsAny<Task>()), Times.Once());
        }

        private void TestEditDescription(string[] descriptionInputs)
        {
            SelectTaskInput(["1"]);
            SetupTaskService_ReturnTasks(1);

            MockView.Setup(obj => obj.GetInput("Your choice: ")).Returns("2");
            Queue<string> allDescriptionInputs = new Queue<string>();
            foreach (string description in descriptionInputs)
            {
                allDescriptionInputs.Enqueue(description);
            }
            MockView.Setup(obj => obj.GetInput("New description: ")).Returns(() => allDescriptionInputs.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.UpdateTask(It.IsAny<Task>()), Times.Once());
        }

        private void TestEditName(string[] nameInputs)
        {
            SelectTaskInput(["1"]);
            SetupTaskService_ReturnTasks(1);

            MockView.Setup(obj => obj.GetInput("Your choice: ")).Returns("1");
            Queue<string> allNameInputs = new Queue<string>();
            foreach (string nameInput in nameInputs)
            {
                allNameInputs.Enqueue(nameInput);
            }
            MockView.Setup(obj => obj.GetInput("New name: ")).Returns(() => allNameInputs.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.UpdateTask(It.IsAny<Task>()), Times.Once());

        }

        private void SelectTaskInput(string[] inputs)
        {
            Queue<string> allInputs = new Queue<string>();
            foreach (string input in inputs)
            {
                allInputs.Enqueue(input);
            }
            MockView.Setup(obj => obj.GetInput("Select task: ")).Returns(() => allInputs.Dequeue());
        }

        private void SetupTaskService_ReturnTasks(int amountOfTasks)
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