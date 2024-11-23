using AutoMapper;
using CleanArchitectrure.Application.Interface.Persistence;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CleanArchitectrure.Application.UseCases.Commons.Exceptions;
using CleanArchitectrure.Domain.Entities;
using MediatR;

namespace CleanArchitectrure.Application.UseCases.Employees.Commands.UpdateEmployeeCommand
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmployeeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<bool>> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            var employee = await _unitOfWork.Employees.GetByIdAsync(command.Id);
            if (employee == null)
            {
                throw new NotFoundExceptionCustom($"El empleado con el Id ({command.Id}) no existe");
            }

            var customer = _mapper.Map<Employee>(command);
            _unitOfWork.Employees.Update(customer);

            await _unitOfWork.SaveChangesAsync();
            response.Data = true;

            if (response.Data)
            {
                response.succcess = true;
                response.Message = "Update succeed!";
            }

            return response;
        }
    }
}
