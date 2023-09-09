using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class APIContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public APIContext(DbContextOptions options) : base(options) { }
    }
}
