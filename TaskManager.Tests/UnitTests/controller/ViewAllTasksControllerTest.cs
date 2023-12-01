using View = TaskManager.src.view.View;
using Moq;
using TaskManager.src.controller;
using TaskManager.src.model;

namespace TaskManager.Tests.UnitTests.controller
{
    public class ViewAllTasksControllerTest
    {
        private readonly ViewAllTasksController Sut;
        private readonly Mock<View> MockView;
        private readonly Mock<ITaskService> MockTaskService;

        public ViewAllTasksControllerTest()
        {
            MockView = new Mock<View>();
            MockTaskService = new Mock<ITaskService>();
            Sut = new(MockView.Object, MockTaskService.Object);
        }
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                ViewAllTasksController Sut = new(null, null);
            });
        }

        [Fact]
        public void Initialize_ShouldDisplayHeader()
        {
            Sut.Initialize();

            MockView.Verify(obj => obj.DisplayHeader(), Times.AtLeastOnce());
        }

        [Fact]
        public void Initialize_ShouldCallTaskService()
        {
            Sut.Initialize();

            MockTaskService.Verify(obj => obj.GetAllTasks(), Times.AtLeastOnce());
        }
    }
}