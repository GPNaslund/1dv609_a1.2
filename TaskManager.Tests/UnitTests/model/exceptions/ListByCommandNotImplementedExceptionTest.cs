using TaskManager.src.model.exceptions;

namespace TaskManager.Tests.UnitTests.model.exceptions
{
    public class ListByCommandNotImplementedExceptionTest
    {
        
        [Fact]
        public void Constructor_ShouldInitialize_WithMessage()
        {
            string message = "Test message";
            ListByCommandNotImplementedException sut = new(message);
            Assert.Equal(message, sut.Message);
        }

    }
}