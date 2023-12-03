using TaskManager.src.view;
using Moq;

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
            Mock<ConsoleService> MockConsoleService = new Mock<ConsoleService>();
            ConsoleView Sut = new ConsoleView(type, MockConsoleService.Object);

            Assert.NotNull(Sut.Header);
            Assert.NotNull(Sut.Menu);
        }

        [Fact]
        public void PrintHeader_ShouldCallTheConsoleService_ToPrintHeader()
        {
            Mock<ConsoleService> MockConsoleService = new Mock<ConsoleService>();
            ConsoleView Sut = new ConsoleView(ViewType.Add_Task_View, MockConsoleService.Object);

            Sut.DisplayHeader();

            MockConsoleService.Verify(obj => obj.ReadLine(It.IsAny<string>()), Times.Once());

        }
    }
}