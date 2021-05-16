using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TokenGenerator.Domain.Command.Commands.ValidateToken;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;
using TokenGenerator.Domain.Command.Result;
using TokenGenerator.Domain.Command.ValidateToken;

namespace TokenGenerator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateTokenController : ControllerBase
    {
        private readonly IValidateTokenCommandHandler _handler;

        public ValidateTokenController(IValidateTokenCommandHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Validates a token based on data provided in the request.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Request performed with no errors</response>
        /// <response code="400">Validation error</response>
        /// <response code="500">Unexpected error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationResult<ValidateTokenCommandResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApplicationResult<dynamic>))]
        public async Task<IActionResult> Post([FromBody] ValidateTokenCommand command)
        {
            var result = await _handler.HandleAsync(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
