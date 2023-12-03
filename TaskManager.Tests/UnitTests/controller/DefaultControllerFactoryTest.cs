using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskManager.src.model;

namespace TaskManager.src.controller
{
    public class DefaultControllerFactoryTest
    {
        private readonly DefaultControllerFactory Sut;

        public DefaultControllerFactoryTest()
        {
            var options = new DbContextOptionsBuilder<AppDatabaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
            Sut = new DefaultControllerFactory(options);
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