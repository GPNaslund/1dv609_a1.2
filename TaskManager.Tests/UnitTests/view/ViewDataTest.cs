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
    }
}