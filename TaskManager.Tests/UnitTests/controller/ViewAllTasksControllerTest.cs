using View = TaskManager.src.view.View;
using Moq;
using TaskManager.src.controller;
using TaskManager.src.model;

namespace TaskManager.Tests.UnitTests.controller
{
    public class ViewAllTasksControllerTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                ViewAllTasksController Sut = new(null, null);
            });
        }

        [Fact]
        public void Initialize_ShouldCallTaskService()
        {
            Mock<View> MockView = new Mock<View>();
            Mock<ITaskService> MockTaskService = new Mock<ITaskService>();
            ViewAllTasksController Sut = new (MockView.Object, MockTaskService.Object);

            Sut.Initialize();

            MockTaskService.Verify(obj => obj.GetAllTasks(), Times.AtLeastOnce());
        }
    }
}