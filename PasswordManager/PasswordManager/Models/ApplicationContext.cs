using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Credential> Credentials { get; set; }
        
        public ApplicationContext()
        {
        }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string herokuConStr = "Host=ec2-54-216-185-51.eu-west-1.compute.amazonaws.com;Database=d83gfranh6pqjq;Username=njudcwfmbpluwi;Password=afec3844e82fed075f576e9e261907c929604204d4036cda2f581e3e40d17c3a;sslmode=Require;TrustServerCertificate=true";
            optionsBuilder.UseNpgsql(herokuConStr);
        }
    }
}
