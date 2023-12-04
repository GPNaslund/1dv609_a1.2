using View = TaskManager.src.view.View;
using Moq;
using TaskManager.src.controller;
using TaskManager.src.model;
using TaskManager.src.view;

namespace TaskManager.Tests.UnitTests.controller
{
    public class AddTaskControllerTest
    {
        private readonly AddTaskController Sut;
        private readonly Mock<View> MockView;
        private readonly Mock<ITaskService> MockTaskService;

        private readonly ViewData ViewData;

        public AddTaskControllerTest()
        {
            MockView = new Mock<View>();
            MockTaskService = new Mock<ITaskService>();
            ViewData = new ViewManager().GetViewData(ViewType.Add_Task);
            Sut = new(MockView.Object, MockTaskService.Object, ViewData);
        }
        
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                AddTaskController Sut = new(null, null, null);
            });
        }

        [Fact]
        public void Initialize_ShouldDisplayHeaderAndMenu()
        {
            MockViewStandardSetUp();

            Sut.Initialize();

            MockView.Verify(obj => obj.DisplayHeader(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.DisplayMenu(), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldCollectDataForNewTask_Successfully()
        {
            MockViewStandardSetUp();

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.CreateTask("A", "B", DateTime.Now.Date), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldReprompt_OnInvalidTaskData()
        {
            TestInvalidInput();

            MockTaskService.Verify(obj => obj.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.AtLeast(2));
        }

        [Fact]
        public void Initialize_ShouldDisplayMessage_OnInvalidTaskData()
        {
            TestInvalidInput();

            MockView.Verify(obj => obj.DisplayMessage(It.IsAny<string>()), Times.AtLeastOnce());
        }

        private void TestInvalidInput()
        {
            MockTaskService.Setup(obj => obj.CreateTask("", "B", It.IsAny<DateTime>())).Throws(new ArgumentException());

            Queue<string> nameInputs = new Queue<string>(new[] { "", "A" });
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("Name"))).Returns(() => nameInputs.Dequeue());
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("Description"))).Returns("B");
            Queue<string> dateInputs = new Queue<string>(new[] { DateTime.Now.AddDays(-1).ToString("yyMMdd"), DateTime.Now.ToString("yyMMdd") });
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("Due date"))).Returns(() => dateInputs.Dequeue());

            Sut.Initialize();
        }

        [Fact]
        public void Initialize_ShouldReturnMainMenuUserCommand_WhenDone()
        {
            MockViewStandardSetUp();

            UserCommand result = Sut.Initialize();

            Assert.Equal(UserCommand.Main_Menu, result);
        }

        private void MockViewStandardSetUp()
        {
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("Name"))).Returns("A");
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("Description"))).Returns("B");
            MockView.Setup(obj => obj.GetInput(ViewData.GetPromptContent("Due date"))).Returns(DateTime.Now.ToString("yyMMdd"));
        }
    }
}