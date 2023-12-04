using TaskManager.src.view;
using TaskManager.src.view.exceptions;

namespace TaskManager.Tests.UnitTests.view
{
    public class ViewDataTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                ViewData Sut = new(null, null, null);
            });
        }

        [Fact]
        public void GetPromptContent_ShouldReturnContensOfSpecifiedPrompt()
        {
            ViewData Sut = new("A", ["B"], [new Prompt("C", "D")]);

            string result = Sut.GetPromptContent("C");

            Assert.Equal("D", result);
        }

        [Fact]
        public void GetPromptContent_ShouldThrowNotImplementedException_IfPromptIsNotPresent()
        {
            ViewData Sut = new ViewData("A", ["B"], [new Prompt("C", "D")]);

            Assert.Throws<PromptNotFoundException>(() => {
                Sut.GetPromptContent("X");
            });
        }
    }
}