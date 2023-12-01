using TaskManager.src.controller;

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
    }
}