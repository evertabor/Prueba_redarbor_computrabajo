using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using MediatR;

namespace CleanArchitectrure.Application.UseCases.Employees.Queries.GetByIdEmployeeQuery
{
    public class GetByIdEmployeeQuery: IRequest<BaseResponse<EmployeeDto>>
    {
        public int? EmployeeId { get; set; }
    }
}
