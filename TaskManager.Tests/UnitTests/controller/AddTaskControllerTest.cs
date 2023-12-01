using View = TaskManager.src.view.View;
using Moq;
using TaskManager.src.controller;
using TaskManager.src.model;

namespace TaskManager.Tests.UnitTests.controller
{
    public class AddTaskControllerTest
    {
        private readonly AddTaskController Sut;
        private readonly Mock<View> MockView;
        private readonly Mock<ITaskService> MockTaskService;

        public AddTaskControllerTest()
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
                AddTaskController Sut = new(null, null);
            });
        }

        [Fact]
        public void Initialize_ShouldCollectDataForNewTask_Successfully()
        {
            MockView.Setup(obj => obj.GetInput("Enter the name: ")).Returns("A");
            MockView.Setup(obj => obj.GetInput("Enter the description: ")).Returns("B");
            MockView.Setup(obj => obj.GetInput("Enter due date (yymmdd): ")).Returns(DateTime.Now.ToString("yyMMdd"));

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.CreateTask("A", "B", DateTime.Now.Date), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldReprompt_OnInvalidTaskData()
        {
            MockTaskService.Setup(obj => obj.CreateTask("", "B", It.IsAny<DateTime>())).Throws(new ArgumentException());

            Queue<string> nameInputs = new Queue<string>(new[] { "", "A" });
            MockView.Setup(obj => obj.GetInput("Enter the name: ")).Returns(() => nameInputs.Dequeue());
            MockView.Setup(obj => obj.GetInput("Enter the description: ")).Returns("B");
            Queue<string> dateInputs = new Queue<string>(new[] {DateTime.Now.AddDays(-1).ToString("yyMMdd"), DateTime.Now.ToString("yyMMdd")});
            MockView.Setup(obj => obj.GetInput("Enter due date (yymmdd): ")).Returns(() => dateInputs.Dequeue());

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.AtLeast(2));
        }

        [Fact]
        public void Initialize_ShouldDisplayMessage_OnInvalidTaskData()
        {
            MockTaskService.Setup(obj => obj.CreateTask("", "B", It.IsAny<DateTime>())).Throws(new ArgumentException());

            Queue<string> nameInputs = new Queue<string>(new[] { "", "A" });
            MockView.Setup(obj => obj.GetInput("Enter the name: ")).Returns(() => nameInputs.Dequeue());
            MockView.Setup(obj => obj.GetInput("Enter the description: ")).Returns("B");
            Queue<string> dateInputs = new Queue<string>(new[] {DateTime.Now.AddDays(-1).ToString("yyMMdd"), DateTime.Now.ToString("yyMMdd")});
            MockView.Setup(obj => obj.GetInput("Enter due date (yymmdd): ")).Returns(() => dateInputs.Dequeue());

            Sut.Initialize();

            MockView.Verify(obj => obj.DisplayMessage(It.IsAny<string>()), Times.AtLeastOnce());
        }
    }
}