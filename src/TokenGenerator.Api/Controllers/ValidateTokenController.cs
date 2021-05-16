using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ValidateTokenCommand command)
        {
            var result = await _handler.HandleAsync(command);
            return Ok(result);
        }
    }
}
