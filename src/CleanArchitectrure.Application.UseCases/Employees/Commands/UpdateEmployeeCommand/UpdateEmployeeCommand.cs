using CleanArchitectrure.Application.UseCases.Commons.Bases;
using MediatR;

namespace CleanArchitectrure.Application.UseCases.Employees.Commands.UpdateEmployeeCommand
{
    public class UpdateEmployeeCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PortalId { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public string Username { get; set; }

        public DateTime? LastLogin { get; set; }
        public string? Fax { get; set; }
        public string? Name { get; set; }
        public string? Telephone { get; set; }
    }
}
