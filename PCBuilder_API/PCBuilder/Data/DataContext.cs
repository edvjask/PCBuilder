using Microsoft.EntityFrameworkCore;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Specification> Specifications { get; set;}
        public DbSet<ProductSpecifications> ProductSpecifications { get; set; }

        public DbSet<SupportedChipsetsByFamilies> FamilyChipsets { get; set; }

        public DbSet<Advert> Adverts { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<AdvertPhotos> AdvertPhotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductSpecifications>()
                .HasKey(ps => new { ps.Id});

            modelBuilder.Entity<User>()
                .Property(user => user.Role).HasDefaultValue("Seller");
        }
    }
}
