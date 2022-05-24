using Connect4;
using System;
using Moq;

namespace Connect4.Tests
{
    [TestClass]
    public class StartScreenTests {
        [TestMethod]
        public void ColorSelection_T_TODO() {
            // Arrange
            Mock<IConsoleIO> mockConsoleIO = new();
            ColorSelection selection = new(mockConsoleIO.Object);
            Player p1 = new(mockConsoleIO.Object, "p1", ConsoleColor.Green);

            mockConsoleIO.Setup(t => t.KeyAvailable).Returns(true);
            mockConsoleIO.SetupSequence(t => t.ReadKey(true))
                .Returns(new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false))
                .Returns(new ConsoleKeyInfo(' ', ConsoleKey.Enter, false, false, false));

            // Act
            ConsoleColor color = selection.Play("PlayerName", p1);

            // Assert
            Assert.AreEqual(ConsoleColor.Yellow, color);
        }

        [TestMethod]
        public void ColorSelection_Double_T_TODO() {
            // Arrange
            Mock<IConsoleIO> mockConsoleIO = new();
            ColorSelection selection = new(mockConsoleIO.Object);
            Player p1 = new(mockConsoleIO.Object, "p1", ConsoleColor.Green);

            mockConsoleIO.Setup(t => t.KeyAvailable).Returns(true);
            mockConsoleIO.SetupSequence(t => t.ReadKey(true))
                .Returns(new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false))
                .Returns(new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false))
                .Returns(new ConsoleKeyInfo(' ', ConsoleKey.Enter, false, false, false))
                .Returns(new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false))
                .Returns(new ConsoleKeyInfo(' ', ConsoleKey.Enter, false, false, false));

            // Act
            ConsoleColor color = selection.Play("PlayerName", p1);

            // Assert
            Assert.AreEqual("Color cannot be the same", selection.error);
            Assert.AreEqual(ConsoleColor.Blue, color);
        }

        [TestMethod]
        public void ColorSelection_Draw_T_TODO() {
            // Arrange
            StringWriter sw = new();
            ConsoleIO consoleIO = new(sw);
            ColorSelection selection = new(consoleIO);

            // Act
            selection.Draw("PlayerName");

            // Assert
            Assert.AreEqual("PlayerName's color:> Red <  Yellow    Green    Blue  ", sw.ToString());
        }
    }
}