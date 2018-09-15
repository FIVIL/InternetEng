using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetProject.Models
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CarBrand> Brands { get; set; }
        public DbSet<CarColor> Colors { get; set; }
        public DbSet<CarType> Cars { get; set; }
        public DbSet<CityName> Cities { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(E =>
            {
                E.HasIndex(p => p.PhoneNumber).IsUnique();
                E.HasIndex(p => p.Email).IsUnique();
                E.HasIndex(p => p.UserName).IsUnique();
            });
            modelBuilder.Entity<CityName>(E =>
            {
                E.HasIndex(p => p.Name).IsUnique();
            });
            modelBuilder.Entity<CarColor>(E =>
            {
                E.HasIndex(p => p.Color).IsUnique();
            });
            modelBuilder.Entity<CarColor>(E =>
            {
                E.HasIndex(p => p.Code).IsUnique();
            });
        }
    }
}
