using Connect4;
using System;
using Moq;

namespace Connect4.Tests
{
    [TestClass]
    public class GameTimerTests
    {
        [TestMethod]
        public void GameTimer_Draw_NoStart_T_TODO() {
            // Arrange
            Mock<IConsoleIO> mockConsoleIO = new();
            GameTimer timer = new(mockConsoleIO.Object, 100, 5);

            // Act
            timer.Draw();

            // Assert
            Assert.IsTrue(timer.Stopped);
            Assert.IsNull(timer.GetTimer);
            mockConsoleIO.Verify(t => t.SetCursorPosition(100, 5), Times.Once());
            mockConsoleIO.Verify(t => t.Write("{0} ", 0));
        }
        
        [TestMethod]
        public void GameTimer_Draw_Start_T_TODO()
        {
            // Arrange
            Mock<IConsoleIO> mockConsoleIO = new();
            GameTimer timer = new(mockConsoleIO.Object, 10, 42);

            // Act
            timer.Start(10);
            timer.Draw();
            timer.UpdatePosition(20, 31);
            timer.Draw();

            // Assert
            Assert.IsFalse(timer.Stopped);
            Assert.IsNotNull(timer.GetTimer);
            mockConsoleIO.Verify(t => t.SetCursorPosition(10, 42), Times.Once());
            mockConsoleIO.Verify(t => t.SetCursorPosition(20, 31), Times.Once());
            mockConsoleIO.Verify(t => t.Write("{0} ", 10), Times.Exactly(2));
        }

        [TestMethod]
        public void GameTimer_Stop_T_TODO() {
            // Arrange
            Mock<IConsoleIO> mockConsoleIO = new();
            GameTimer timer = new(mockConsoleIO.Object, 0, 0);

            // Act and Assert
            Assert.IsTrue(timer.Stopped);
            Assert.IsNull(timer.GetTimer);
            timer.Start(20);
            Assert.IsFalse(timer.Stopped);
            Assert.IsNotNull(timer.GetTimer);
            timer.Stop();
            Assert.IsTrue(timer.Stopped);
            Assert.IsNull(timer.GetTimer);
        }

        [TestMethod]
        public void GameTimer_Time_T_TODO() {
            // Arrange
            Mock<IConsoleIO> mockConsoleIO = new();
            GameTimer timer = new(mockConsoleIO.Object, 0, 0);

            // Act
            timer.Start(20);
            timer.Draw();
            Thread.Sleep(1050);
            timer.Draw();

            // Assert
            mockConsoleIO.Verify(t => t.Write("{0} ", 20), Times.Once());
            mockConsoleIO.Verify(t => t.Write("{0} ", 19), Times.Once());
        }

        [TestMethod]
        public void GameTimer_AutoStop_T_TODO() {
            // Arrange
            Mock<IConsoleIO> mockConsoleIO = new();
            GameTimer timer = new(mockConsoleIO.Object, 0, 0);

            // Act
            timer.Start(1);
            timer.Draw();
            Thread.Sleep(1050);
            timer.Draw();

            // Assert
            mockConsoleIO.Verify(t => t.Write("{0} ", 1), Times.Once());
            mockConsoleIO.Verify(t => t.Write("{0} ", 0), Times.Once());
            Assert.IsTrue(timer.Stopped);
        }
    }
}