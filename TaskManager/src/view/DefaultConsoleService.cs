namespace TaskManager.src.view
{
    public class DefaultConsoleService : ConsoleService
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadLine(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
    }
}