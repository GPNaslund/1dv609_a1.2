namespace TaskManager.src.view
{
    public class ViewDataManager
    {
        public ViewData GetViewData(ViewType type)
        {
            return new ViewData("A", ["B"], []);
        }
    }
}