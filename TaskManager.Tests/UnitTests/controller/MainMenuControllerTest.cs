using Moq;
using TaskManager.src.controller;
using View = TaskManager.src.view.view;

namespace TaskManager.Tests.UnitTests.controller
{
    public class MainMenuControllerTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_OnNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => {
                MainMenuController Sut = new(null);
            });
        }

        [Fact]
        public void Initialize_ShouldCallViewMethods()
        {
            Mock<View> MockView = new Mock<View>();
            MainMenuController Sut = new(MockView.Object);

            Sut.Initialize();

            MockView.Verify(obj => obj.DisplayMenu(), Times.AtLeastOnce());
        }
    }
}