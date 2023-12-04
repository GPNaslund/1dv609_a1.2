using Task = TaskManager.src.model.Task;
using TaskStatus = TaskManager.src.model.TaskStatus;

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

        [Theory]
        [InlineData(TaskStatus.Not_Completed)]
        [InlineData(TaskStatus.Completed)]
        [InlineData(TaskStatus.In_Progress)]
        public void SetStatus_ShouldBeAbleToChangeStatusOfTask(TaskStatus newStatus)
        {
            Task sut = new("A", "B", DateTime.Now);
            sut.Status = newStatus;
            Assert.Equal(newStatus, sut.Status);
        }

        [Fact]
        public void Id_ShouldExistAndBeAssignable_ForEFCoreUsage()
        {
            Task sut = new("A", "B", DateTime.Now);
            sut.Id = 1;
            Assert.Equal(1, sut.Id);
        }

        [Fact]
        public void Equals_ShouldCompareTasksCorrectly()
        {
            Task task1 = new("A", "B", DateTime.Now);
            Task task2 = new("A", "B", DateTime.Now);
            string notATask = "";

            Assert.True(task1.Equals(task2));
            Assert.False(task1.Equals(notATask));
        }
    }
}