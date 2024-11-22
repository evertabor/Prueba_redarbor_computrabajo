using System.ComponentModel.DataAnnotations;

namespace CleanArchitectrure.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int PortalId { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public required string Username { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? Fax { get; set; }
        public string? Name { get; set; }
        public string? Telephone { get; set; }
    }
}
