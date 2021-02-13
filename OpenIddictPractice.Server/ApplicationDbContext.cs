using Microsoft.EntityFrameworkCore;
namespace OpenIddictPractice.Server
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) { }
    }
}