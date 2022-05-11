using Connect4;
using System;

namespace connect4.test
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Player_Draw_IsTurn_3_2() {
            // Arrange
            Player player = new("playerName", ConsoleColor.White);
            StringWriter sw = new();
            Console.SetOut(sw);

            // Act
            player.Draw(true);

            // Assert
            Assert.AreEqual(sw.ToString(), "> playerName <");
        }

        [TestMethod]
        public void Player_Draw_IsNotTurn_3_2()
        {
            // Arrange
            Player player = new("playerName", ConsoleColor.White);
            StringWriter sw = new();
            Console.SetOut(sw);

            // Act
            player.Draw(false);

            // Assert
            Assert.AreEqual(sw.ToString(), "  playerName  ");
        }
    }
}