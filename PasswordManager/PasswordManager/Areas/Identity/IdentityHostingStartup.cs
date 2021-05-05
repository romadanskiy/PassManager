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
                const string localConStr = "Server=localhost;Port=5432;Database=PasswordManagerDB;User Id=postgres;Password=PAROLsekret777;";
                const string herokuConStr = "Host=ec2-54-216-185-51.eu-west-1.compute.amazonaws.com;Database=d83gfranh6pqjq;Username=njudcwfmbpluwi;Password=afec3844e82fed075f576e9e261907c929604204d4036cda2f581e3e40d17c3a;sslmode=Require;TrustServerCertificate=true";
            
                services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(herokuConStr));
                
                services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationContext>()
                    .AddDefaultTokenProviders();
            });
        }
    }
}