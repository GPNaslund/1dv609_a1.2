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
    }
}