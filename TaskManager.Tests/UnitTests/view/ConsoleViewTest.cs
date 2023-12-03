using TaskManager.src.view;

namespace TaskManager.Tests.UnitTests.view
{
    public class ConsoleViewTest
    {
        [Fact]
        public void Constructor_ShouldBeAbleToInitialize_AddTaskViewData()
        {
            ConsoleView Sut = new ConsoleView(ViewType.Add_Task_View);

            Assert.NotNull(Sut.Header);
            Assert.NotNull(Sut.Menu);
        }

        [Fact]
        public void Constructor_ShouldBeAbleToInitialize_EditTaskViewData()
        {
            ConsoleView Sut = new ConsoleView(ViewType.Edit_Task);

            Assert.NotNull(Sut.Header);
            Assert.NotNull(Sut.Menu);
        }
    }
}