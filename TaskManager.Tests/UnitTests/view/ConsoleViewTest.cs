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

        [Theory]
        [InlineData(ViewType.Add_Task_View)]
        [InlineData(ViewType.Edit_Task)]
        [InlineData(ViewType.List_Tasks)]
        [InlineData(ViewType.View_All_Tasks)]
        [InlineData(ViewType.Main_Menu)]
        public void PrintHeader_ShouldCallConsoleService(ViewType type)
        {
            ConsoleView Sut = new ConsoleView(type, MockConsoleService.Object);

            Sut.DisplayHeader();

            MockConsoleService.Verify(obj => obj.WriteLine(It.IsAny<string>()), Times.Once());

        }

        [Theory]
        [InlineData(ViewType.Add_Task_View)]
        [InlineData(ViewType.Edit_Task)]
        [InlineData(ViewType.List_Tasks)]
        [InlineData(ViewType.View_All_Tasks)]
        [InlineData(ViewType.Main_Menu)]
        public void PrintMenu_ShouldCallConsoleService(ViewType type)
        {
            ConsoleView Sut = new ConsoleView(type, MockConsoleService.Object);

            Sut.DisplayMenu();

            MockConsoleService.Verify(obj => obj.WriteLine(It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Fact]
        public void DisplayMessage_ShouldCallConsoleService()
        {
            ConsoleView Sut = new ConsoleView(ViewType.Add_Task_View, MockConsoleService.Object);

            Sut.DisplayMessage("A");

            MockConsoleService.Verify(obj => obj.WriteLine("A"), Times.Once());
        }

        [Fact]
        public void GetInput_ShouldCallConsoleServic_AndReturnInput()
        {
            ConsoleView Sut = new ConsoleView(ViewType.Add_Task_View, MockConsoleService.Object);
            MockConsoleService.Setup(obj => obj.ReadLine("A")).Returns("B");

            string result = Sut.GetInput("A");

            MockConsoleService.Verify(obj => obj.ReadLine("A"), Times.Once());
            Assert.Equal("B", result);
        }
    }
}