using Connect4;
using System;
using Moq;

namespace Connect4.Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Player_Draw_IsTurn_T_3_2() {
            // Arrange
            StringWriter sw = new();
            ConsoleIO consoleIO = new(sw);
            Player player = new(consoleIO, "playerName", ConsoleColor.White);

            // Act
            player.Draw(true);

            // Assert
            Assert.AreEqual(sw.ToString(), "> playerName <");
        }

        [TestMethod]
        public void Player_Draw_IsNotTurn_T_3_3()
        {
            // Arrange
            StringWriter sw = new();
            ConsoleIO consoleIO = new(sw);
            Player player = new(consoleIO, "playerName", ConsoleColor.White);

            // Act
            player.Draw(false);

            // Assert
            Assert.AreEqual(sw.ToString(), "  playerName  ");
        }
    }
}