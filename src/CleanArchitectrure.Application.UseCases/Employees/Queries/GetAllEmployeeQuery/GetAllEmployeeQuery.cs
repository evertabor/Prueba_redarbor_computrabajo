using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using MediatR;

namespace CleanArchitectrure.Application.UseCases.Employees.Queries.GetAllEmployeeQuery
{
    public class GetAllEmployeeQuery: IRequest<BaseResponse<IEnumerable<EmployeeDto>>>
    {
    }
}
