using Microsoft.EntityFrameworkCore;
namespace OpenIddicPractice.Server
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) { }
    }
}