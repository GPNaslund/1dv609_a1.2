using Moq;
using TaskManager.src.controller;
using TaskFactory = TaskManager.src.controller.TaskFactory;

namespace TaskManager.Tests.UnitTests.controller
{
    public class AppControllerTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                AppController Sut = new(null);
            });
        }

        [Fact]
        public void Run_ShouldInitialize_MainMenuController_ByDefault()
        {
            Mock<TaskFactory> MockFactory = new Mock<TaskFactory>();
            Mock<ExecutingController> MainMenuController = new Mock<ExecutingController>();
            MockFactory.Setup(obj => obj.Create_MainMenuController()).Returns(MainMenuController.Object);
            AppController Sut = new(MockFactory.Object);

            Sut.Run();

            MainMenuController.Verify(obj => obj.Initialize(), Times.Once());
        }
    }
}