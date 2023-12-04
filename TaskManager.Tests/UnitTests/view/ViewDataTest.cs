using TaskManager.src.view;

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
    }
}