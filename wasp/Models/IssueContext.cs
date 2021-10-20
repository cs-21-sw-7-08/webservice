using Microsoft.EntityFrameworkCore;

namespace wasp.Models
{
    public class IssueContext : DbContext
    {
        public IssueContext(DbContextOptions<IssueContext> options)
            : base(options)
        {
        }

        public DbSet<Issue> TodoItems { get; set; }
    }
}