using Xunit;
using Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Server.Entities;
using Server.Models;

namespace Tests.ControllersTests
{

    public class DirectionControllerTests
    {
        [Fact]
        public void TestSimpleDirectionChange()
        {
            DirectionController directionController = new DirectionController();
            ChangeDirection direction = new ChangeDirection
            {
                Direction = Direction.Left
            };

            var result = directionController.ChangeDirection(direction);

            Assert.IsType<OkResult>(result);
        }
    }
}