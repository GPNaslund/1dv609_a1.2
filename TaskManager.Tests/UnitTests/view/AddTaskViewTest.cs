using TaskManager.src.view;

namespace TaskManager.Tests.UnitTests.view
{
    public class AddTaskViewTest
    {
        [Fact]
        public void Constructor_ShouldInitializeView_WithHeaderAndMenu()
        {
            AddTaskView Sut = new AddTaskView();

            Assert.NotNull(Sut.Header);
            Assert.NotNull(Sut.Menu);
        }
    }
}