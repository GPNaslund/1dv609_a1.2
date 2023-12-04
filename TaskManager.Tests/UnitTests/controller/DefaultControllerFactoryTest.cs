using Microsoft.EntityFrameworkCore;
using Moq;
using TaskManager.src.model;
using TaskManager.src.view;

namespace TaskManager.src.controller
{
    public class DefaultControllerFactoryTest
    {
        private readonly DefaultControllerFactory Sut;

        private readonly Mock<IViewManager> MockViewManager;

        public DefaultControllerFactoryTest()
        {
            var options = new DbContextOptionsBuilder<AppDatabaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
            MockViewManager = new Mock<IViewManager>();
            ViewData StubViewData = new("A", ["B"], []);
            MockViewManager.Setup(obj => obj.GetViewData(It.IsAny<ViewType>())).Returns(StubViewData);
            Sut = new DefaultControllerFactory(options, MockViewManager.Object);
        }

        [Fact]
        public void Create_MainMenuController_ShouldReturnAnInstanceOfMainMenuController()
        {

            ExecutingController result = Sut.Create_MainMenuController();

            Assert.IsType<MainMenuController>(result);
        }

        [Fact]
        public void Create_AddTaskMenuController_ShouldReturnAnInstanceOfAddTaskController()
        {
            ExecutingController result = Sut.Create_AddTaskController();

            Assert.IsType<AddTaskController>(result);
        }

        [Fact]
        public void Create_EditTaskMenuController_ShouldReturnAnInstanceOfEditTaskController()
        {

            ExecutingController result = Sut.Create_EditTaskController();

            Assert.IsType<EditTaskController>(result);
        }

        [Fact]
        public void Create_ViewAllTasksMenuController_ShouldReturnAnInstanceOfViewAllTasksController()
        {
            ExecutingController result = Sut.Create_ViewAllTasksController();

            Assert.IsType<ViewAllTasksController>(result);
        }

        [Fact]
        public void Create_ListTasksController_ShouldReturnAnInstanceOfListTasksController()
        {
            ExecutingController result = Sut.Create_ListTasksController();

            Assert.IsType<ListTasksController>(result);
        }
    }
}