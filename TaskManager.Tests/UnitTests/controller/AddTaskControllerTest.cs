using TaskManager.src.controller;

namespace TaskManager.Tests.UnitTests.controller
{
    public class AddTaskControllerTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                AddTaskController Sut = new(null);
            });
        }
    }
}