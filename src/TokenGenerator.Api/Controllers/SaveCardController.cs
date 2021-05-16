using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TokenGenerator.Domain.Command.Commands.SaveCard;
using TokenGenerator.Domain.Command.Interfaces;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;

namespace TokenGenerator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveCardController : ControllerBase
    {
        private readonly ISaveCardCommandHandler _handler;

        public SaveCardController(ISaveCardCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveCardCommand command)
        {
            var result = await _handler.HandleAsync(command);
            return Ok(result);
        }

    }
}
