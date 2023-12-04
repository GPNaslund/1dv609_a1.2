using TaskManager.src.view;

namespace TaskManager.Tests.UnitTests.view
{
    public class DefaultConsoleServiceTests : IDisposable
    {
        private readonly StringWriter stringWriter;
        private readonly StringReader stringReader;
        private readonly TextWriter originalOutput;
        private readonly TextReader originalInput;

        public DefaultConsoleServiceTests()
        {
            originalOutput = Console.Out;
            originalInput = Console.In;
            stringWriter = new StringWriter();
            stringReader = new StringReader("A");
        }

        public void Dispose()
        {
            // Restores the original output and input streams, to
            // not affect other parts of testing.s
            Console.SetOut(originalOutput);
            Console.SetIn(originalInput);

            stringWriter?.Dispose();
            stringReader?.Dispose();
        }

        [Fact]
        public void WriteLine_ShouldWriteToConsole()
        {
            Console.SetOut(stringWriter);
            DefaultConsoleService Sut = new();
            
            Sut.WriteLine("A");
            Assert.Equal("A", stringWriter.ToString().Trim());
        }

        [Fact]
        public void ReadLine_ShouldReadFromConsole()
        {
            Console.SetIn(stringReader);
            DefaultConsoleService Sut = new();

            string result = Sut.ReadLine("prompt");

            Assert.Equal("A", result);
        }
    }
}