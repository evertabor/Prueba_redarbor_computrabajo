using CleanArchitectrure.Application.Interface.Persistence;
using CleanArchitectrure.Persistence.Contexts;

namespace CleanArchitectrure.Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository Employees { get; }
        private readonly DataBaseContext _context;

        public UnitOfWork(DataBaseContext context, IEmployeeRepository customers)
        {
            Employees = customers ?? throw new ArgumentNullException(nameof(customers));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync();
        }
    }
}
