using FluentValidation;

namespace CleanArchitectrure.Application.UseCases.Employees.Commands.CreateEmployeeCommand
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
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
