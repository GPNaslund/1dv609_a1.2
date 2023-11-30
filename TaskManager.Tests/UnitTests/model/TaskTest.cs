using Task = TaskManager.src.model.Task;

namespace TaskManager.Tests.UnitTests.model
{
    public class TaskTest
    {
        [Fact]
        public void SetName_ShouldThrowArgumentException_OnEmptyName()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Task sut = new("", "Description", DateTime.Now);
            });
        }

        [Fact]
        public void SetName_ShouldThrowArgumentNullException_OnNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Task sut = new(null, "Description", DateTime.Now);
            });
        }

        [Fact]
        public void SetDescription_ShouldThrowArgumentNullException_OnNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Task sut = new("Name", null, DateTime.Now);
            });
        }

        [Fact]
        public void SetDueDate_ShouldThrowArgumentException_WhenDueDateIsBeforeCreationDate()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Task sut = new("Name", "Description", DateTime.Now.AddDays(-1));
            });
        }

        [Fact]
        public void SetDueDate_ShouldNotThrowArgumentException_WhenDueDateIsSameAsCreationDate()
        {

            Task sut = new("Name", "Description", DateTime.Now);
            Assert.NotNull(sut);
        }

        [Fact]
        public void ToString_ShouldReturnAStringRepresentationOfTheTask()
        {
            Task sut = new Task("A", "B", DateTime.Now.AddDays(1));
            Assert.IsType<string>(sut.ToString());
        }

    }
}