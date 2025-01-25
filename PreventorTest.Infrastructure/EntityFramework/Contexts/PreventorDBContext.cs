using Microsoft.EntityFrameworkCore;
using PreventorTest.Application.Students;

namespace PreventorTest.Infrastructure.EntityFramework.Contexts
{
    public class PreventorDBContext : DbContext
    {
        public PreventorDBContext(DbContextOptions<PreventorDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Student> Student { get; set; }
    }
}
