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
            Mock<ExecutingController> MockMainMenuController = new Mock<ExecutingController>();
            MockMainMenuController.Setup(obj => obj.Initialize()).Returns(UserCommand.Main_Menu);
            MockFactory.Setup(obj => obj.Create_MainMenuController()).Returns(MockMainMenuController.Object);
            AppController Sut = new(MockFactory.Object);

            Sut.Run();

            MockMainMenuController.Verify(obj => obj.Initialize(), Times.Once());
        }

        [Fact]
        public void Run_ShouldInstansiateAndRunControllersBasedOnUserCommand()
        {
            Mock<TaskFactory> MockFactory = new Mock<TaskFactory>();
            Mock<ExecutingController> MockMainMenuController = new Mock<ExecutingController>();
            Mock<ExecutingController> MockAddTaskController = new Mock<ExecutingController>();
            MockMainMenuController.Setup(obj => obj.Initialize()).Returns(UserCommand.Add_Task);
            MockFactory.Setup(obj => obj.Create_MainMenuController()).Returns(MockMainMenuController.Object);
            MockFactory.Setup(obj => obj.Create_AddTaskMenuController()).Returns(MockAddTaskController.Object);
            AppController Sut = new(MockFactory.Object);

            Sut.Run();

            MockAddTaskController.Verify(obj => obj.Initialize(), Times.Once());
        }
    }
}