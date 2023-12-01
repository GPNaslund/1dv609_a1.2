using View = TaskManager.src.view.View;
using Moq;
using TaskManager.src.controller;
using TaskManager.src.model;

namespace TaskManager.Tests.UnitTests.controller
{
    public class ListTasksControllerTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                ListTasksController Sut = new(null, null);
            });
        }

        [Fact]
        public void Initialize_ShouldDisplayHeaderAndMenu()
        {
            Mock<View> MockView = new Mock<View>();
            Mock<ITaskService> MockTaskService = new Mock<ITaskService>();

            ListTasksController Sut = new(MockView.Object, MockTaskService.Object);

            Sut.Initialize();

            MockView.Verify(obj => obj.DisplayHeader(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.DisplayMenu(), Times.AtLeastOnce());

        }
    }
}