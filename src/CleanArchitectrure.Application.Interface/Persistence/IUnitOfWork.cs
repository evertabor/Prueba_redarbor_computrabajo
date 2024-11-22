namespace CleanArchitectrure.Application.Interface.Persistence
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        ICustomerRepository Employees { get; }
        void SaveChanges();
        Task SaveChangesAsync();


    }
}
