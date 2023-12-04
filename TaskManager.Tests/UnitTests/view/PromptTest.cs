
using TaskManager.src.view;

namespace TaskManager.Tests.UnitTests.view
{
    public class PromptTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                Prompt Sut = new(null, null);
            });
        }
    }
}