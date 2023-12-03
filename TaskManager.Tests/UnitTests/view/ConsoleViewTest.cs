using TaskManager.src.view;

namespace TaskManager.Tests.UnitTests.view
{
    public class ConsoleViewTest
    {
        [Theory]
        [InlineData(ViewType.Add_Task_View)]
        [InlineData(ViewType.Edit_Task)]
        [InlineData(ViewType.List_Tasks)]
        [InlineData(ViewType.View_All_Tasks)]
        [InlineData(ViewType.Main_Menu)]
        public void Constructor_ShouldBeAbleToInitialize_HeaderAndMenu_BasedOnViewType(ViewType type)
        {
            ConsoleView Sut = new ConsoleView(type);

            Assert.NotNull(Sut.Header);
            Assert.NotNull(Sut.Menu);
        }
    }
}