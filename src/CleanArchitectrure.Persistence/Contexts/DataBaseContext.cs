using CleanArchitectrure.Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace CleanArchitectrure.Persistence.Contexts
{
    public class DataBaseContext : DbContext
    {
        private readonly string _connectionString;

        public DataBaseContext(DbContextOptions<DataBaseContext> options, IConfiguration configuration) : base(options)
        {
            _connectionString = configuration.GetConnectionString("ConnectionRedarbor") ?? throw new ArgumentNullException("Connection string not found.");
        }

        // Crea conexion para ser utilizada por Dapper
        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

        public virtual DbSet<Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, new MySqlServerVersion(new Version(8, 0, 30)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
