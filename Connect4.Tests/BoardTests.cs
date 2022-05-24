using Connect4;
using System;
using Moq;

namespace Connect4.Tests
{
    [TestClass]
    public class BoardTests {
        [TestMethod]
        public void Column_DetectsFull_T_TODO() {
            // Arrange
            Mock<IConsoleIO> mockConsoleIO = new();
            Column column = new(3);
            Player p1 = new(mockConsoleIO.Object, "p1", ConsoleColor.Green);
            Player p2 = new(mockConsoleIO.Object, "p2", ConsoleColor.Yellow);

            // Act and Assert
            Assert.IsNull(column[0]);
            Assert.IsNull(column[1]);
            Assert.IsNull(column[2]);
            Assert.IsFalse(column.isFull);
            column.AddPiece(p1);
            Assert.IsFalse(column.isFull);
            column.AddPiece(p2);
            Assert.IsFalse(column.isFull);
            column.AddPiece(p1);
            Assert.IsTrue(column.isFull);
            OverflowException ex = Assert.ThrowsException<OverflowException>(() => column.AddPiece(p2));
            Assert.AreEqual("Column is full", ex.Message);
            Assert.AreEqual(p1.name, column[0]!.name);
        }

        [TestMethod]
        public void Board_MovesAndDetectsFull_T_TODO() {
            // Arrange
            Mock<IConsoleIO> mockConsoleIO = new();
            Board board = new(mockConsoleIO.Object, 3, 1);
            Player p1 = new(mockConsoleIO.Object, "p1", ConsoleColor.Green);
            Player p2 = new(mockConsoleIO.Object, "p2", ConsoleColor.Yellow);

            // Act and Assert
            Assert.IsFalse(board.isFull());

            board.SelectLeft();
            board.SelectLeft();
            board.SelectRight();
            (int, int) coords1 = board.Move(p1);
            Assert.AreEqual(2, coords1.Item1);
            Assert.AreEqual(0, coords1.Item2);
            Assert.IsFalse(board.isFull());

            board.SelectRight();
            board.SelectRight();
            board.SelectLeft();
            board.SelectLeft();
            (int, int) coords2 = board.Move(p2);
            Assert.AreEqual(0, coords2.Item1);
            Assert.AreEqual(0, coords2.Item2);

            (int, int) coords3 = board.Move(p2);
            Assert.AreEqual(1, coords3.Item1);
            Assert.AreEqual(0, coords3.Item2);
            Assert.IsTrue(board.isFull());
        }

        [TestMethod]
        public void Board_DrawEmpty_NoSelection_T_TODO() {
            // Arrange
            StringWriter sw = new();
            ConsoleIO consoleIO = new(sw);
            Board board = new(consoleIO, 2, 1);

            // Act
            board.Draw(0, 0, false);

            // Assert
            Assert.AreEqual("╔═════╗║ ○ ○ ║╚═════╝", sw.ToString());
        }

        [TestMethod]
        public void Board_DrawEmpty_WithSelection_T_TODO() {
            // Arrange
            StringWriter sw = new();
            ConsoleIO consoleIO = new(sw);
            Board board = new(consoleIO, 2, 1);

            // Act
            board.Draw(0, 0, true);

            // Assert
            Assert.AreEqual("  v  ╔═════╗║ ○ ○ ║╚═════╝", sw.ToString());
        }

        [TestMethod]
        public void Board_DrawEmpty_SetCursor_T_TODO() {
            // Arrange
            Mock<IConsoleIO> mockConsoleIO = new();
            Board board = new(mockConsoleIO.Object, 1, 2);

            // Act
            board.Draw(0, 0, false);
            board.Draw(0, 0, true);

            // Assert
            mockConsoleIO.Verify(t => t.SetCursorPosition(0, 0), Times.Once());
            mockConsoleIO.Verify(t => t.SetCursorPosition(0, 1), Times.Exactly(2));
            mockConsoleIO.Verify(t => t.SetCursorPosition(0, 2), Times.Exactly(2));
            mockConsoleIO.Verify(t => t.SetCursorPosition(0, 3), Times.Exactly(2));
            mockConsoleIO.Verify(t => t.SetCursorPosition(0, 4), Times.Exactly(2));
        }

        [TestMethod]
        public void Board_DrawState_T_TODO() {
            // Arrange
            Mock<IConsoleIO> mockConsoleIO = new();
            Board board = new(mockConsoleIO.Object, 1, 2);
            Player p1 = new(mockConsoleIO.Object, "p1", ConsoleColor.Green);

            // Act
            board.Move(p1);
            board.Draw(0, 0, false);

            // Assert
            mockConsoleIO.VerifySet(t => t.ForegroundColor = ConsoleColor.Green, Times.Once());
            mockConsoleIO.VerifySet(t => t.ForegroundColor = Program.defaultForegroundColor, Times.Once());
            mockConsoleIO.Verify(t => t.Write("●"));
        }
    }
}