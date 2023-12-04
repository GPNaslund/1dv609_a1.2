using TaskManager.src.view;

namespace TaskManager.Tests.UnitTests.view
{
    public class ViewDataManagerTest
    {
        [Fact]
        public void GetViewData_ShouldReturnViewDataForViewType_Successfully()
        {
            ViewDataManager Sut = new();

            ViewData result = Sut.GetViewData(ViewType.Main_Menu);

            Assert.NotNull(result);
        }
    }
}
