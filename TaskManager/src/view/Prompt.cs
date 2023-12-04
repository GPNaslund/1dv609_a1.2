namespace TaskManager.src.view
{
    public class Prompt
    {
        public Prompt(string name, string content)
        {
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(content);
        }
    }
}