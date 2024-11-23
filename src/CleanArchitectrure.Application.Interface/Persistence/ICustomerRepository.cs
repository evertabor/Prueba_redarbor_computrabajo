using CleanArchitectrure.Domain.Entities;

namespace CleanArchitectrure.Application.Interface.Persistence
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        Task<Employee?> GetByEmailAsync(string email);
    }
}
