using TaskManager.src.view;

namespace TaskManager.Tests.UnitTests.view
{
    public class ViewManagerTest
    {
        [Theory]
        [InlineData(ViewType.Edit_Task)]
        [InlineData(ViewType.Add_Task)]
        [InlineData(ViewType.List_Tasks)]
        [InlineData(ViewType.View_All_Tasks)]
        [InlineData(ViewType.Main_Menu)]
        public void GetViewData_ShouldReturnViewDataForViewType_Successfully(ViewType type)
        {
            ViewManager Sut = new();

            ViewData result = Sut.GetViewData(type);

            Assert.NotNull(result);
        }
    }
}
