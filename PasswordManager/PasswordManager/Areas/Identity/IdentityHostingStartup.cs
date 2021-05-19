using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Models;

[assembly: HostingStartup(typeof(PasswordManager.Areas.Identity.IdentityHostingStartup))]
namespace PasswordManager.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                //const string localConStr = "Server=localhost;Port=5432;Database=PasswordManagerDB;User Id=postgres;Password=PAROLsekret777;";
                const string herokuConStr = "Host=ec2-34-255-134-200.eu-west-1.compute.amazonaws.com;Database=d2b96u1o0ronld;Username=ueqchrjdtfjtha;Password=e46556b3ad071962b613ac3183af25788f1ab6b5a1e4ede47f5dd903ef3025cf;sslmode=Require;TrustServerCertificate=true";
            
                services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(herokuConStr));
                
                services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationContext>()
                    .AddDefaultTokenProviders();
            });
        }
    }
}