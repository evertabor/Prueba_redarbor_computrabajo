using AutoMapper;
using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.Interface.Persistence;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CleanArchitectrure.Application.UseCases.Commons.Exceptions;
using MediatR;

namespace CleanArchitectrure.Application.UseCases.Employees.Queries.GetByIdEmployeeQuery
{
    public class GetByIdEmployeeHandler : IRequestHandler<GetByIdEmployeeQuery, BaseResponse<EmployeeDto>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdEmployeeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<EmployeeDto>> Handle(GetByIdEmployeeQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<EmployeeDto>();

            var employee = await _unitOfWork.Employees.GetByIdAsync(request.EmployeeId);
            if (employee == null)
            {
                throw new NotFoundExceptionCustom($"El empleado con el Id ({request.EmployeeId}) no existe");
            }

            if (employee is not null)
            {
                response.Data = _mapper.Map<EmployeeDto>(employee);
                response.succcess = true;
                response.Message = "Query succeed!";
            }

            return response;
        }
    }
}
