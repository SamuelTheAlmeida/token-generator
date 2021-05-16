using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TokenGenerator.Domain.Command.Commands.SaveCard;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;
using TokenGenerator.Domain.Command.Result;

namespace TokenGenerator.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class SaveCardController : ControllerBase
    {
        private readonly ISaveCardCommandHandler _handler;

        public SaveCardController(ISaveCardCommandHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Saves the card and generates a token
        /// </summary>
        /// <returns>The generated Token, creationDate and CardId</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">Validation error</response>
        /// <response code="500">Unexpected error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationResult<SaveCardCommandResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApplicationResult<dynamic>))]
        public async Task<IActionResult> Post([FromBody] SaveCardCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _handler.HandleAsync(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
