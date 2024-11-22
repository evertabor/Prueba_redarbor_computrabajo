using AutoMapper;
using CleanArchitectrure.Application.Interface.Persistence;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
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

            try
            {
                var employee = _mapper.Map<Employee>(command);
                _unitOfWork.Employees.Insert(employee);

                await _unitOfWork.SaveChangesAsync();
                response.Data = true;

                if (response.Data)
                {
                    response.succcess = true;
                    response.Message = "Create succeed!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = false;
            }

            return response;
        }
    }
}
