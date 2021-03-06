using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kayitsistemi2.Areas.Identity.Data;
using kayitsistemi2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kayitsistemi2.Data
{
    public class KayitdbContext : IdentityDbContext<ApplicationUser>
    {
        public KayitdbContext(DbContextOptions<KayitdbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            
        }
        public DbSet<TaskModel> TaskModels { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
