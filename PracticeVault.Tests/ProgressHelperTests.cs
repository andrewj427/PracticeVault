using PracticeVault.Helpers;

namespace PracticeVault.Tests
{
    public class ProgressHelperTests
    {
        [Fact]
        public void Progress100_ReturnsComplete()
        {
            // Arrange
            var helper = new ProgressHelper();

            // Act
            var result = helper.GetProgressLabel(100);

            // Assert
            Assert.Equal("Complete", result);
        }

        [Fact]
        public void Progress75_ReturnsInProgress()
        {
            var helper = new ProgressHelper();

            var result = helper.GetProgressLabel(75);

            Assert.Equal("In Progress", result);
        }

        [Fact]
        public void Progress20_ReturnsJustStarted()
        {
            var helper = new ProgressHelper();

            var result = helper.GetProgressLabel(20);

            Assert.Equal("Just Started", result);
        }
    }
}