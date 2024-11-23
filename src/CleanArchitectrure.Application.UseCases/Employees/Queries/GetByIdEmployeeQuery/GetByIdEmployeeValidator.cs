using FluentValidation;

namespace CleanArchitectrure.Application.UseCases.Employees.Queries.GetByIdEmployeeQuery
{
    public class GetByIdEmployeeValidator : AbstractValidator<GetByIdEmployeeQuery>
    {
        public GetByIdEmployeeValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty()
                .NotNull();
        }
    }
}
