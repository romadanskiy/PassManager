using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Feature> Features { get; set; }
        
        public ApplicationContext()
        {
        }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string herokuConStr = "Host=ec2-34-255-134-200.eu-west-1.compute.amazonaws.com;Database=d2b96u1o0ronld;Username=ueqchrjdtfjtha;Password=e46556b3ad071962b613ac3183af25788f1ab6b5a1e4ede47f5dd903ef3025cf;sslmode=Require;TrustServerCertificate=true";
            optionsBuilder.UseNpgsql(herokuConStr);
        }
    }
}
