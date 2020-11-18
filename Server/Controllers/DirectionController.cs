using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    /// <summary>
    /// Controller for POST-request changing snake's moving direction
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult ChangeDirection([FromBody] ChangeDirection arg)
        {
            GameBoardHolder.GetGameBoardInstance().UpdateDirection(arg.Direction);
            return Ok();
        }
    }
}
