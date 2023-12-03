namespace TaskManager.src.controller
{
    public class DefaultControllerFactoryTest
    {
        [Fact]
        public void Create_MainMenuController_ShouldReturnAnInstanceOfMainMenuController()
        {
            DefaultControllerFactory Sut = new DefaultControllerFactory();

            ExecutingController result = Sut.Create_MainMenuController();

            Assert.IsType<MainMenuController>(result);
        }

        [Fact]
        public void Create_AddTaskMenuController_ShouldReturnAnInstanceOfAddTaskController()
        {
            DefaultControllerFactory Sut = new DefaultControllerFactory();

            ExecutingController result = Sut.Create_AddTaskController();

            Assert.IsType<AddTaskController>(result);
        }
    }
}