using TaskManager.src.view;
using Moq;

namespace TaskManager.Tests.UnitTests.view
{
    public class ConsoleViewTest
    {

        private readonly Mock<ConsoleService> MockConsoleService;
        private readonly ViewData StubViewData;

        private readonly ConsoleView Sut;

        public ConsoleViewTest()
        {
            MockConsoleService = new Mock<ConsoleService>();
            StubViewData = new("A", ["B"], []);
            Sut = new(StubViewData, MockConsoleService.Object);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                ConsoleView Sut = new ConsoleView(null, null);
            });
        }

        [Fact]
        public void Constructor_ShouldInitialize_HeaderAndMenuFields()
        {
            Assert.NotNull(Sut.Header);
            Assert.NotNull(Sut.Menu);
        }


        [Fact]
        public void PrintHeader_ShouldCallConsoleService()
        {
            Sut.DisplayHeader();

            MockConsoleService.Verify(obj => obj.WriteLine(It.IsAny<string>()), Times.Once());

        }

        [Fact]
        public void PrintMenu_ShouldCallConsoleService()
        {
            Sut.DisplayMenu();

            MockConsoleService.Verify(obj => obj.WriteLine(It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Fact]
        public void DisplayMessage_ShouldCallConsoleService()
        {
            Sut.DisplayMessage("A");

            MockConsoleService.Verify(obj => obj.WriteLine("A"), Times.Once());
        }

        [Fact]
        public void GetInput_ShouldCallConsoleServic_AndReturnInput()
        {
            MockConsoleService.Setup(obj => obj.ReadLine("A")).Returns("B");

            string result = Sut.GetInput("A");

            MockConsoleService.Verify(obj => obj.ReadLine("A"), Times.Once());
            Assert.Equal("B", result);
        }
    }
}