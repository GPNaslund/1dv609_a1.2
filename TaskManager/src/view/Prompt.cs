namespace TaskManager.src.view
{
    public class Prompt
    {
        public string Name {get; set;}
        public string Content {get; set;}
        public Prompt(string name, string content)
        {
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(content);

            Name = name;
            Content = content;
        }
    }
}