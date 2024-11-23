using CleanArchitectrure.Application.Interface.Persistence;
using CleanArchitectrure.Domain.Entities;
using CleanArchitectrure.Persistence.Contexts;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace CleanArchitectrure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Private Fields
        private readonly DataBaseContext _context;
        private readonly DbSet<Employee> _entitySet;
        #endregion

        #region Constructor
        public EmployeeRepository(DataBaseContext applicationContext)
        {
            _context = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this._entitySet = _context.Set<Employee>();
        }
        #endregion

        #region Queries
        /*Queries*/
        public async Task<int> CountAsync()
        {
            using var connection = _context.CreateConnection();
            var query = "Select Count(*) From Customers";

            var count = await connection.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);
            return count;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var query = "SELECT * FROM Employee";

            using (var connection = this._context.CreateConnection())
            {
                connection.Open();
                return await connection.QueryAsync<Employee>(query);
            }
        }

        public async Task<Employee?> GetByIdAsync(int? id)
        {
            var query = "SELECT * FROM Employee WHERE id = @Id";

            using (var connection = this._context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Employee>(query, new { Id = id });

                return result;
            }
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            var query = "SELECT * FROM Employee WHERE email = @Email";

            using (var connection = this._context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Employee>(query, new { Email = email });

                return result;
            }
        }
        #endregion

        #region Commands
        /*Commands*/
        public void Insert(Employee entity) => this._context.Entry<Employee>(entity).State = EntityState.Added;

        public void Update(Employee entity) => this._context.Entry<Employee>(entity).State = EntityState.Modified;

        public void Delete(Expression<Func<Employee, bool>> where)
        {
            foreach (Employee t in this._entitySet.Where<Employee>(where).AsEnumerable<Employee>())
            {
                this._entitySet.Remove(t);
            }
        }
        #endregion
    }
}
