using TaskManager.src.view;
using Moq;

namespace TaskManager.Tests.UnitTests.view
{
    public class ConsoleViewTest
    {

        private readonly Mock<ConsoleService> MockConsoleService;
        public ConsoleViewTest()
        {
            MockConsoleService = new Mock<ConsoleService>();
        }

        [Theory]
        [InlineData(ViewType.Add_Task_View)]
        [InlineData(ViewType.Edit_Task)]
        [InlineData(ViewType.List_Tasks)]
        [InlineData(ViewType.View_All_Tasks)]
        [InlineData(ViewType.Main_Menu)]
        public void Constructor_ShouldBeAbleToInitialize_HeaderAndMenu_BasedOnViewType(ViewType type)
        {
            ConsoleView Sut = new ConsoleView(type, MockConsoleService.Object);

            Assert.NotNull(Sut.Header);
            Assert.NotNull(Sut.Menu);
        }

        [Fact]
        public void PrintHeader_ShouldCallTheConsoleService_ToPrintHeader()
        {
            ConsoleView Sut = new ConsoleView(ViewType.Add_Task_View, MockConsoleService.Object);

            Sut.DisplayHeader();

            MockConsoleService.Verify(obj => obj.WriteLine(It.IsAny<string>()), Times.Once());

        }
    }
}