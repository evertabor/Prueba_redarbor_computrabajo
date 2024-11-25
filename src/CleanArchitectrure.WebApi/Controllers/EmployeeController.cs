using AutoMapper;
using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.UseCases.Employees.Commands.CreateEmployeeCommand;
using CleanArchitectrure.Application.UseCases.Employees.Commands.DeleteEmployeeCommand;
using CleanArchitectrure.Application.UseCases.Employees.Commands.UpdateEmployeeCommand;
using CleanArchitectrure.Application.UseCases.Employees.Queries.GetAllEmployeeQuery;
using CleanArchitectrure.Application.UseCases.Employees.Queries.GetByIdEmployeeQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EmployeeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
        public async Task<IActionResult> GetIdAsync(int id)
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
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> InsertAsync([FromBody] EmployeeDto employee)
        {
            if (employee is null) return BadRequest();
            return Ok(await _mediator.Send(_mapper.Map<CreateEmployeeCommand>(employee)));
        }

        /// <summary>
        /// Update an existingitem
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] EmployeeDto employee)
        {
            if (employee is null) return BadRequest();
            return Ok(await _mediator.Send(_mapper.Map<UpdateEmployeeCommand>(employee)));
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return Ok(await _mediator.Send(new DeleteEmployeeCommand() { EmployeeId = id }));
        }

    }
}
