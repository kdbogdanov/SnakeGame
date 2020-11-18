using Xunit;
using Server.Entities;
using Server.Models;
using Server;

namespace SnakeGame.Tests
{
    public class GameBoardTests
    {
        [Theory]
        [InlineData(20, 20, 600)]
        public void Should_Restarts_WhenHitsTheWall(int x, int y, long time)
        {
            GameBoard gameBoard = GameBoardHolder.GetGameBoardInstance(x, y, time, false);
            var obj = new object();
            var startHeadPos = new Cell(gameBoard.GameBoardSize.Width / 2,
                gameBoard.GameBoardSize.Height / 2);

            for (int i = gameBoard.Snake.Head.Y; i <= gameBoard.GameBoardSize.Height; i++)
                gameBoard.Update(obj);

            Assert.Equal(startHeadPos, gameBoard.Snake.Head);
        }
    }
}
