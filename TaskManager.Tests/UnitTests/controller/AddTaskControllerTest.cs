using View = TaskManager.src.view.View;
using Moq;
using TaskManager.src.controller;

namespace TaskManager.Tests.UnitTests.controller
{
    public class AddTaskControllerTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                AddTaskController Sut = new(null);
            });
        }

        [Fact]
        public void Initialize_ShouldCollectDataForNewTask_Successfully()
        {
            Mock<View> MockView = new Mock<View>();
            MockView.Setup(obj => obj.GetInput("Enter the name: ")).Returns("Test_Name");
            MockView.Setup(obj => obj.GetInput("Enter the description: ")).Returns("Test_Description");
            MockView.Setup(obj => obj.GetInput("Enter due date (yymmdd): ")).Returns(DateTime.Now.ToString("yyMMdd"));


            AddTaskController Sut = new(MockView.Object);

            Sut.Initialize();

            MockView.Verify(obj => obj.GetInput(It.IsAny<string>()), Times.AtLeastOnce());
        }
    }
}