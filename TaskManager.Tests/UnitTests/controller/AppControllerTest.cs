using TaskManager.src.controller;

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
    }
}