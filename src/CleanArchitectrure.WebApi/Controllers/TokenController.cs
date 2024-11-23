using CleanArchitectrure.Application.UseCases.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectrure.WebApi.Controllers
{
    /// <summary>
    /// Controller to manage Token
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public TokenController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        /// <summary>
        /// Generate token
        /// </summary>
        /// <returns>Token</returns>
        [HttpPost]
        public async Task<IActionResult> GenerateToken()
        {
            var response = await _mediator.Send(new TokenCommand() { });
            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

    }
}
