using Xunit;
using Server.Models;
using Server.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace SnakeGame.Tests
{
    public class GameBoardControllerTests
    {
        [Fact]
        public void Should_GetData_WhenRequest()
        {
            GameBoardController gameBoardController = new GameBoardController();
            var result = gameBoardController.GetData();

            Assert.IsType<OkObjectResult>(result);


            var okObjectResult = result as OkObjectResult;

            var actual = okObjectResult.Value;

            Assert.IsType<GameBoard>(actual);
        }

        [Fact]
        public void Should_NotNull_WhenGetData()
        {
            GameBoardController gameBoardController = new GameBoardController();         
            var result = gameBoardController.GetData();

            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            Assert.NotNull(okObjectResult.Value);
        }

        
    }
}
