using ActiveDirectoryEmulatorApi.Domain.Models;
using ActiveDirectoryEmulatorApi.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryEmulatorApi.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(u => u.UserId);
            builder.Entity<User>().Property(u => u.UserId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(u => u.Code).IsRequired();
            builder.Entity<User>().Property(u => u.Name).IsRequired();
            builder.Entity<User>().Property(u => u.Lastname).IsRequired();
            builder.Entity<User>().Property(u => u.Email).IsRequired();
            builder.Entity<User>().Property(u => u.Password).IsRequired();
            builder.Entity<User>().Property(u => u.Phone).IsRequired();
            builder.Entity<User>().Property(u => u.Image).HasColumnType("LONGBLOB");
            //builder.Entity<Vote>().Property(v => v.Image).IsRequired().HasMaxLength(250);
            //builder.Entity<Vote>().Property(v => v.Choise).IsRequired().HasMaxLength(250);


            // Apply Naming Convention
            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
