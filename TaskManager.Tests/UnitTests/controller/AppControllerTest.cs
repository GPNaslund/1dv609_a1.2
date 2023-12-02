using Moq;
using TaskManager.src.controller;

namespace TaskManager.Tests.UnitTests.controller
{
    public class AppControllerTest
    {
        private readonly AppController Sut;
        private readonly Mock<ControllerFactory> Factory;
        private readonly Mock<ExecutingController> MockAddTaskController;
        private readonly Mock<ExecutingController> MockMainMenuController;

        private readonly Mock<ExecutingController> MockListTasksController;
        private readonly Mock<ExecutingController> MockEditTaskController;
        private readonly Mock<ExecutingController> MockViewAllTasksController;

        public AppControllerTest()
        {
            MockMainMenuController = new Mock<ExecutingController>();
            MockAddTaskController = new Mock<ExecutingController>();
            MockListTasksController = new Mock<ExecutingController>();
            MockEditTaskController = new Mock<ExecutingController>();
            MockViewAllTasksController = new Mock<ExecutingController>();

            Factory = new Mock<ControllerFactory>();
            SetupFactory();
            Sut = new(Factory.Object);
        }

        private void SetupFactory()
        {
            Factory.Setup(m => m.Create_AddTaskMenuController()).Returns(MockAddTaskController.Object);
            Factory.Setup(m => m.Create_ViewAllTasksMenuController()).Returns(MockViewAllTasksController.Object);
            Factory.Setup(m => m.Create_ListTasksMenuController()).Returns(MockListTasksController.Object);
            Factory.Setup(m => m.Create_MainMenuController()).Returns(MockMainMenuController.Object);
            Factory.Setup(m => m.Create_EditTaskMenuController()).Returns(MockEditTaskController.Object);

        }


        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                AppController Sut = new(null);
            });
        }

        [Fact]
        public void Run_ShouldInitialize_MainMenuController_ByDefault()
        {
            Factory.Setup(m => m.Create_MainMenuController()).Returns(MockMainMenuController.Object);
            MockMainMenuController.Setup(m => m.Initialize()).Returns(UserCommand.Quit_Application);

            Sut.Run();

            Factory.Verify(m => m.Create_MainMenuController(), Times.Once);
            MockMainMenuController.Verify(m => m.Initialize(), Times.Once);
        }

       [Theory]
        [InlineData(UserCommand.Add_Task)]
        [InlineData(UserCommand.List_Tasks)]
        [InlineData(UserCommand.Edit_Task)]
        [InlineData(UserCommand.View_All_Tasks)]
        [InlineData(UserCommand.Main_Menu)]
        public void Run_ShouldInstansiateAndRunControllersBasedOnUserCommand(UserCommand commandToActOn)
        {
            MockMainMenuController.Setup(m => m.Initialize()).Returns(commandToActOn);

            TestRunWithUserCommand(commandToActOn);

        }

        private void TestRunWithUserCommand(UserCommand commandToActOn)
        {
            switch (commandToActOn)
            {
                case UserCommand.Add_Task:
                    MockAddTaskController.Setup(m => m.Initialize()).Returns(UserCommand.Quit_Application);
                    Sut.Run();
                    MockAddTaskController.Verify(m => m.Initialize(), Times.Once());
                    break;
                case UserCommand.View_All_Tasks:
                    MockViewAllTasksController.Setup(m => m.Initialize()).Returns(UserCommand.Quit_Application);
                    Sut.Run();
                    MockViewAllTasksController.Verify(m => m.Initialize(), Times.Once());
                    break;
                case UserCommand.List_Tasks:
                    MockListTasksController.Setup(m => m.Initialize()).Returns(UserCommand.Quit_Application);
                    Sut.Run();
                    MockListTasksController.Verify(m => m.Initialize(), Times.Once());
                    break;
                case UserCommand.Main_Menu:
                    MockMainMenuController.Setup(m => m.Initialize()).Returns(UserCommand.Quit_Application);
                    Sut.Run();
                    MockMainMenuController.Verify(m => m.Initialize(), Times.Once());
                    break;
                case UserCommand.Edit_Task:
                    MockEditTaskController.Setup(m => m.Initialize()).Returns(UserCommand.Quit_Application);
                    Sut.Run();
                    MockEditTaskController.Verify(m => m.Initialize(), Times.Once());
                    break;
            }
        }

        
        [Fact]
        public void Run_ShouldThrowArgumentException_OnFaultyValue()
        {
            MockMainMenuController.Setup(m => m.Initialize()).Returns(UserCommand.Unkown);
            
            Assert.Throws<ArgumentException>(() => Sut.Run());
        }
    }
}