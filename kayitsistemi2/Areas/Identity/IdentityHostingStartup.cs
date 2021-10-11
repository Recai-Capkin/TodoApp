using System;
using kayitsistemi2.Areas.Identity.Data;
using kayitsistemi2.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(kayitsistemi2.Areas.Identity.IdentityHostingStartup))]
namespace kayitsistemi2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<KayitdbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("kayitdbContextConnection")));
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("RequireAdministratorRole",
                         policy => policy.RequireRole("Administrator"));
                });
                services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;

                    }
                )
                    //Rol hizmeti eklendi.
                    .AddRoles<IdentityRole>()

                    .AddEntityFrameworkStores<KayitdbContext>();
            });
        }
    }
}