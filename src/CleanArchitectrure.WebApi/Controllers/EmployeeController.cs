using Microsoft.AspNetCore.Mvc;
using MediatR;
using CleanArchitectrure.Application.UseCases.Employees.Queries.GetAllEmployeeQuery;
using CleanArchitectrure.Application.UseCases.Employees.Queries.GetByIdEmployeeQuery;
using CleanArchitectrure.Application.UseCases.Employees.Commands.CreateEmployeeCommand;
using CleanArchitectrure.Application.UseCases.Employees.Commands.UpdateEmployeeCommand;
using CleanArchitectrure.Application.UseCases.Employees.Commands.DeleteEmployeeCommand;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitectrure.WebApi.Controllers
{
    /// <summary>
    /// Controller to manage employees
    /// </summary>
    [Authorize]
    [Route("api/redarbor")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Get all employees items
        /// </summary>
        /// <returns>Array of employee items</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _mediator.Send(new GetAllEmployeeQuery());
            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        /// <summary>
        /// Get an item by ID
        /// </summary>
        /// <param name="id">id employee</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIdAsync([FromQuery] int id)
        {
            var response = await _mediator.Send(new GetByIdEmployeeQuery() { EmployeeId = id });
            if (response.succcess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Add a new item
        /// </summary>
        /// <param name="command">Comando</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> InsertAsync([FromBody] CreateEmployeeCommand command)
        {
            if (command is null) return BadRequest();
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Update an existingitem
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateEmployeeCommand command)
        {
            if (command is null) return BadRequest();
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromQuery] int id)
        {
            return Ok(await _mediator.Send(new DeleteEmployeeCommand() { EmployeeId = id }));
        }

    }
}
