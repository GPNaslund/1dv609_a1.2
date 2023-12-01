using TaskManager.src.controller;

namespace TaskManager.Tests.UnitTests.controller
{
    public class EditTaskControllerTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                EditTaskController Sut = new(null, null);
            });
        }
    }
}