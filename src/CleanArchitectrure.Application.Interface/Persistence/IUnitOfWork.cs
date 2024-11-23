namespace CleanArchitectrure.Application.Interface.Persistence
{
    public interface IUnitOfWork: IDisposable
    {
        IEmployeeRepository Employees { get; }
        void SaveChanges();
        Task SaveChangesAsync();


    }
}
