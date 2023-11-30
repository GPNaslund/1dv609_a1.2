using Task = TaskManager.src.model.Task;

namespace TaskManager.Tests.UnitTests.model
{
    public class TaskTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentException_OnEmptyName()
        {
            Assert.Throws<ArgumentException>(() => {
                Task sut = new("", "Description", DateTime.Now);
            });
        }
    }
}