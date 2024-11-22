using AutoMapper;
using CleanArchitectrure.Application.Interface.Persistence;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CleanArchitectrure.Application.UseCases.Commons.Exceptions;
using MediatR;

namespace CleanArchitectrure.Application.UseCases.Employees.Commands.DeleteEmployeeCommand
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmployeeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<bool>> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            var employee = await _unitOfWork.Employees.GetAsync(command.EmployeeId);

            if (employee == null)
            {
                throw new NotFoundExceptionCustom($"El empleado con el Id ({command.EmployeeId}) no existe");
            }

            _unitOfWork.Employees.Delete(x => x.Id == command.EmployeeId);

            await _unitOfWork.SaveChangesAsync();
            response.Data = true;

            if (response.Data)
            {
                response.succcess = true;
                response.Message = "Delete succeed!";
            }

            return response;
        }
    }
}
