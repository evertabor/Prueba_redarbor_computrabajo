using CleanArchitectrure.Application.UseCases.Commons.Bases;
using MediatR;

namespace CleanArchitectrure.Application.UseCases.Employees.Commands.DeleteEmployeeCommand
{
    public class DeleteEmployeeCommand : IRequest<BaseResponse<bool>>
    {
        public int EmployeeId { get; set; }
    }
}
