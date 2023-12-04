using View = TaskManager.src.view.View;
using Task = TaskManager.src.model.Task;
using TaskManager.src.controller;
using Moq;
using TaskManager.src.model;
using TaskManager.src.view;

namespace TaskManager.Tests.UnitTests.controller
{
    public class EditTaskControllerTest
    {
        private readonly EditTaskController Sut;
        private readonly Mock<View> MockView;
        private readonly Mock<ITaskService> MockTaskService;

        private readonly ViewData ViewData;

        public EditTaskControllerTest()
        {
            MockView = new Mock<View>();
            MockTaskService = new Mock<ITaskService>();
            ViewData = new ViewManager().GetViewData(ViewType.Edit_Task);
            Sut = new(MockView.Object, MockTaskService.Object, ViewData);
        }
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                EditTaskController Sut = new(null, null, null);
            });
        }

        [Fact]
        public void Initialize_ShouldAllowUserToSelectATaskToEdit()
        {
            SelectTask_Input(["1"]);
            SetupTaskService_ReturnTasks(1);
            NavigateEditActionMenu(["0"]);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.GetAllTasks(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.GetInput(ViewData.GetPromptContent("Select task")), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldRepromptUserSelection_OnInvalidInput()
        {
            SelectTask_Input(["a", "10", "1"]);
            SetupTaskService_ReturnTasks(1);
            NavigateEditActionMenu(["0"]);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.GetAllTasks(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.GetInput(ViewData.GetPromptContent("Select task")), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldReturnUserCommandMainMenu_OnInputBackOption()
        {
            SelectTask_Input(["0"]);
            SetupTaskService_ReturnTasks(0);

            UserCommand result = Sut.Initialize();

            Assert.Equal(UserCommand.Main_Menu, result);
        }

        [Fact]
        public void Initialize_ShouldReturnUserCommandMainMenu_OnBackInput_FromEditActionMenu()
        {
            SelectTask_Input(["1"]);
            SetupTaskService_ReturnTasks(1);
            NavigateEditActionMenu(["0"]);

            UserCommand result = Sut.Initialize();

            Assert.Equal(UserCommand.Main_Menu, result);
        }

        [Fact]
        public void Initialize_ShouldRepromptForEditActionInput_Until_UserCommandMainMenu()
        {
            SelectTask_Input(["1"]);
            SetupTaskService_ReturnTasks(1);
            NavigateEditActionMenu(["a", "5", "0"]);
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("Delete confirmation"))).Returns("n");

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

            MockView.Verify(obj => obj.GetInput(ViewData.GetPromptContent("New due date")), Times.Once());
        }

        [Fact]
        public void Initialize_EditDueDate_ShouldRepromptForDueDate_OnInvalidValue()
        {
            string dateInput = DateTime.Now.ToString("yyMMdd");
            TestEditDueDate([null, dateInput]);
            
            MockView.Verify(obj => obj.GetInput(ViewData.GetPromptContent("New due date")), Times.Exactly(2));
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        public void Initialize_ShouldAllowUserTo_EditStatus_Successfully(string selectStatusInput)
        {
            TestEditStatus([selectStatusInput]);
        }

        [Fact]
        public void Initialize_EditStatus_ShouldReprompt_OnInvalidInput()
        {
            TestEditStatus([null, "1"]);
        }

        [Fact]
        public void Initialize_ShouldAllowUserTo_DeleteTask_Successfully()
        {
            SelectTask_Input(["1"]);
            SetupTaskService_ReturnTasks(1);

            NavigateEditActionMenu(["5", "0"]);
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("Delete confirmation"))).Returns("y");

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.DeleteTask(It.IsAny<Task>()), Times.Once());
        }

        [Fact]
        public void Initialize_DeleteTask_ShouldReprompt_OnInvalidValue()
        {
            SelectTask_Input(["1"]);
            SetupTaskService_ReturnTasks(1);

            NavigateEditActionMenu(["5", "0"]);
            Queue<string> deletionInputs = new Queue<string>(new[] {null, "y"});
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("Delete confirmation"))).Returns(() => deletionInputs.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.DeleteTask(It.IsAny<Task>()), Times.Once());
        }

        private void TestEditStatus(string[] statusInputs)
        {
            SelectTask_Input(["1"]);
            SetupTaskService_ReturnTasks(1);
            
            NavigateEditActionMenu(["4", "0"]);
            Queue<string> allStatusInputs = new Queue<string>();
            foreach (string input in statusInputs)
            {
                allStatusInputs.Enqueue(input);
            }
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("New status"))).Returns(() => allStatusInputs.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.UpdateTask(It.IsAny<Task>()), Times.Once());
        }

        private void TestEditDueDate(string[] descriptionInputs)
        {
            SelectTask_Input(["1"]);
            SetupTaskService_ReturnTasks(1);
            NavigateEditActionMenu(["3", "0"]);
            Queue<string> allDateInputs = new Queue<string>();
            foreach (string description in descriptionInputs)
            {
                allDateInputs.Enqueue(description);
            }
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("New due date"))).Returns(() => allDateInputs.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.UpdateTask(It.IsAny<Task>()), Times.Once());
        }

        private void TestEditDescription(string[] descriptionInputs)
        {
            SelectTask_Input(["1"]);
            SetupTaskService_ReturnTasks(1);

            NavigateEditActionMenu(["2", "0"]);
            Queue<string> allDescriptionInputs = new Queue<string>();
            foreach (string description in descriptionInputs)
            {
                allDescriptionInputs.Enqueue(description);
            }
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("New description"))).Returns(() => allDescriptionInputs.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.UpdateTask(It.IsAny<Task>()), Times.Once());
        }

        private void TestEditName(string[] nameInputs)
        {
            SelectTask_Input(["1"]);
            SetupTaskService_ReturnTasks(1);

            NavigateEditActionMenu(["1", "0"]);
            Queue<string> allNameInputs = new Queue<string>();
            foreach (string nameInput in nameInputs)
            {
                allNameInputs.Enqueue(nameInput);
            }
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("New name"))).Returns(() => allNameInputs.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.UpdateTask(It.IsAny<Task>()), Times.Once());
        }

        private void SelectTask_Input(string[] inputs)
        {
            Queue<string> allInputs = new Queue<string>();
            foreach (string input in inputs)
            {
                allInputs.Enqueue(input);
            }
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("Select task"))).Returns(() => allInputs.Dequeue());
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

        private void NavigateEditActionMenu(string[] menuInputs)
        {
            Queue<string> allInputs = new Queue<string>();
            foreach (string input in menuInputs)
            {
                allInputs.Enqueue(input);
            }
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("Your choice"))).Returns(() => allInputs.Dequeue());
        }


    }
}