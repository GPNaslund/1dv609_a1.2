using Task = TaskManager.src.model.Task;

namespace TaskManager.Tests.UnitTests.model
{
    public class TaskTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Constructor_ShouldThrowArgumentException_OnInvalidName(string name)
        {
            Assert.Throws<ArgumentException>(() => {
                Task sut = new(name, "Description", DateTime.Now);
            });
        }

    }
}