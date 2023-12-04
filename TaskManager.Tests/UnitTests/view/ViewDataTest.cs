using TaskManager.src.view;
using TaskManager.src.view.exceptions;

namespace TaskManager.Tests.UnitTests.view
{
    public class ViewDataTest
    {
        private readonly ViewData Sut;

        public ViewDataTest()
        {
            Sut = new("A", ["B"], [new Prompt("C", "D")]);
        }
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

            string result = Sut.GetPromptContent("C");

            Assert.Equal("D", result);
        }

        [Fact]
        public void GetPromptContent_ShouldThrowNotImplementedException_IfPromptIsNotPresent()
        {
            Assert.Throws<PromptNotFoundException>(() => {
                Sut.GetPromptContent("X");
            });
        }

        [Fact]
        public void GetPromptContent_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                Sut.GetPromptContent(null);
            });
        }
    }
}