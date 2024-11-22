using Microsoft.AspNetCore.Mvc;
using MediatR;
using CleanArchitectrure.Application.UseCases.Employees.Queries.GetAllEmployeeQuery;
using CleanArchitectrure.Application.UseCases.Employees.Queries.GetByIdEmployeeQuery;
using CleanArchitectrure.Application.UseCases.Employees.Commands.CreateEmployeeCommand;
using CleanArchitectrure.Application.UseCases.Employees.Commands.UpdateEmployeeCommand;
using CleanArchitectrure.Application.UseCases.Employees.Commands.DeleteEmployeeCommand;
using CleanArchitectrure.Application.UseCases.Auth.Commands;

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
