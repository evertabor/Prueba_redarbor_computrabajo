using FluentValidation;

namespace CleanArchitectrure.Application.UseCases.Employees.Commands.UpdateEmployeeCommand
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.CompanyId).NotEmpty().NotNull();
            RuleFor(x => x.Email).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
            RuleFor(x => x.PortalId).NotEmpty().NotNull();
            RuleFor(x => x.RoleId).NotEmpty().NotNull();
            RuleFor(x => x.StatusId).NotEmpty().NotNull();
            RuleFor(x => x.Username).NotEmpty().NotNull();
        }
    }
}
