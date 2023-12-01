using Moq;
using TaskManager.src.controller;
using View = TaskManager.src.view.view;

namespace TaskManager.Tests.UnitTests.controller
{
    public class MainMenuControllerTest
    {
        private readonly MainMenuController Sut;
        private readonly Mock<View> MockView;

        public MainMenuControllerTest()
        {
            MockView = new Mock<View>();
            Sut = new(MockView.Object);
        }
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
            MockView.Setup(obj => obj.GetInput(It.IsAny<string>())).Returns("1");

            Sut.Initialize();

            MockView.Verify(obj => obj.DisplayHeader(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.DisplayMenu(), Times.AtLeastOnce());
            MockView.Verify(obj => obj.GetInput(It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Theory]
        [InlineData("1", UserCommand.Add_Task)]
        [InlineData("2", UserCommand.View_All_Tasks)]
        [InlineData("3", UserCommand.List_Tasks)]
        [InlineData("4", UserCommand.Edit_Task)]
        [InlineData("0", UserCommand.Quit_Application)]
        public void Initialize_ShouldReturnUserCommand_BasedOnUserInput(string input, UserCommand expected)
        {
            MockView.Setup(obj => obj.GetInput(It.IsAny<string>())).Returns(input);

            UserCommand result = Sut.Initialize();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Initialize_ShouldReprompt_OnFaultyInput()
        {
            Queue<string> allInputs = new Queue<string>(new[] { "a", "1" });
            MockView.Setup(obj => obj.GetInput(It.IsAny<string>())).Returns(() => allInputs.Dequeue());

            Sut.Initialize();

            MockView.Verify(obj => obj.GetInput(It.IsAny<string>()), Times.AtLeast(2));
        }

        [Fact]
        public void Initialize_ShouldDisplayMessage_OnFaultyInput()
        {
            Queue<string> allInputs = new Queue<string>(new[] { "a", "1" });
            MockView.Setup(obj => obj.GetInput(It.IsAny<string>())).Returns(() => allInputs.Dequeue());

            Sut.Initialize();

            MockView.Verify(obj => obj.DisplayMessage(It.IsAny<string>()), Times.AtLeastOnce());
        }
    }
}