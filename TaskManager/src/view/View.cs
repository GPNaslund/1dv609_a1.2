namespace TaskManager.src.view
{
    public interface view
    {
        public void DisplayMenu();

        public void DisplayHeader();

        public string GetInput(string prompt);
    }
}