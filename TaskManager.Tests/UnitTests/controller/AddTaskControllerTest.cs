using View = TaskManager.src.view.View;
using Moq;
using TaskManager.src.controller;
using TaskManager.src.model;

namespace TaskManager.Tests.UnitTests.controller
{
    public class AddTaskControllerTest
    {
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
            Mock<View> MockView = new Mock<View>();
            Mock<ITaskService> MockTaskService = new Mock<ITaskService>();
            MockView.Setup(obj => obj.GetInput("Enter the name: ")).Returns("A");
            MockView.Setup(obj => obj.GetInput("Enter the description: ")).Returns("B");
            MockView.Setup(obj => obj.GetInput("Enter due date (yymmdd): ")).Returns(DateTime.Now.ToString("yyMMdd"));


            AddTaskController Sut = new(MockView.Object, MockTaskService.Object);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.CreateTask("A", "B", DateTime.Now.Date), Times.AtLeastOnce());
        }
    }
}