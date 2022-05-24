using Connect4;
using System;
using Moq;

namespace Connect4.Tests
{
    [TestClass]
    public class UtilTests
    {
        [TestMethod]
        public void ScreenOption_Draw_NoColor_T_TODO() {
            // Arrange
            Mock<IConsoleIO> mockConsoleIO = new();
            ScreenOption option = new(mockConsoleIO.Object, "Screen Option");

            // Act
            option.Draw();

            // Assert
            mockConsoleIO.VerifySet(t => t.ForegroundColor = Program.defaultForegroundColor, Times.Exactly(2));
            mockConsoleIO.Verify(t => t.Write("Screen Option"));
        }

        [TestMethod]
        public void ScreenOption_Draw_Color_T_TODO() {
            // Arrange
            Mock<IConsoleIO> mockConsoleIO = new();
            ScreenOption option = new(mockConsoleIO.Object, "Screen Option", ConsoleColor.Green);

            // Act
            option.Draw();

            // Assert
            mockConsoleIO.VerifySet(t => t.ForegroundColor = ConsoleColor.Green, Times.Once());
            mockConsoleIO.VerifySet(t => t.ForegroundColor = Program.defaultForegroundColor, Times.Once());
            mockConsoleIO.Verify(t => t.Write("Screen Option"));
        }
    }
}