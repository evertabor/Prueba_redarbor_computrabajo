using AutoMapper;
using CleanArchitectrure.Application.Interface.Persistence;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CleanArchitectrure.Application.UseCases.Commons.Exceptions;
using CleanArchitectrure.Domain.Entities;
using MediatR;

namespace CleanArchitectrure.Application.UseCases.Employees.Commands.CreateEmployeeCommand
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<bool>> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            var employee = _mapper.Map<Employee>(command);

            var employeeResult = await _unitOfWork.Employees.GetByEmailAsync(command.Email);
            if (employeeResult != null)
            {
                throw new AlreadyExistsExceptionCustom($"Ya existe un empleado con el Email ({command.Email})");
            }

            _unitOfWork.Employees.Insert(employee);

            await _unitOfWork.SaveChangesAsync();
            response.Data = true;

            if (response.Data)
            {
                response.succcess = true;
                response.Message = "Create succeed!";
            }

            return response;
        }
    }
}
