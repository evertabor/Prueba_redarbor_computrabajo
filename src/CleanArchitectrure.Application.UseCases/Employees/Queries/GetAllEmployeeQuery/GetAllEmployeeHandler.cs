using AutoMapper;
using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.Interface.Persistence;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using MediatR;

namespace CleanArchitectrure.Application.UseCases.Employees.Queries.GetAllEmployeeQuery
{
    public class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeQuery, BaseResponse<IEnumerable<EmployeeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllEmployeeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<IEnumerable<EmployeeDto>>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<EmployeeDto>>();

            try
            {
                var customers = await _unitOfWork.Employees.GetAllAsync();

                if(customers is not null)
                {
                    response.Data = _mapper.Map<IEnumerable<EmployeeDto>>(customers);
                    response.succcess = true;
                    response.Message = "Query succeed!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
