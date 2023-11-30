using Task = TaskManager.src.model.Task;

namespace TaskManager.Tests.UnitTests.model
{
    public class TaskTest
    {
        [Fact]
        public void SetName_ShouldThrowArgumentException_OnEmptyName()
        {
            Assert.Throws<ArgumentException>(() => {
                Task sut = new("", "Description", DateTime.Now);
            });
        }

        [Fact]
        public void SetName_ShouldThrowArgumentNullException_OnNull()
        {
            Assert.Throws<ArgumentNullException>(() => {
                Task sut = new(null, "Description", DateTime.Now);
            });
        }

        [Fact]
        public void SetDescription_ShouldThrowArgumentNullException_OnNull()
        {
            Assert.Throws<ArgumentNullException>(() => {
                Task sut = new ("Name", null, DateTime.Now);
            });
        }

        
    }
}