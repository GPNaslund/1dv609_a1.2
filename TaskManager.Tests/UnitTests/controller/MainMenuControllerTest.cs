using TaskManager.src.controller;

namespace TaskManager.Tests.UnitTests.controller
{
    public class MainMenuControllerTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                MainMenuController Sut = new(null);
            });
        }
    }
}