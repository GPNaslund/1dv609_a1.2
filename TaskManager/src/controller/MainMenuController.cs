using View = TaskManager.src.view.view;

namespace TaskManager.src.controller
{
    public class MainMenuController
    {
        public MainMenuController(View view)
        {
            ArgumentNullException.ThrowIfNull(view);
        }
    }
}