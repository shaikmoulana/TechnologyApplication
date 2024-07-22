using DataServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Data
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Technologies> TblTechnology { get; set; }
        public DbSet<Departments> TblDepartment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Technologies>().ToTable("TblTechnology");
            modelBuilder.Entity<Technologies>().Property(t => t.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Technologies>().Property(t => t.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Technologies>().Property(t => t.CreatedBy).HasDefaultValue("SYSTEM");
            modelBuilder.Entity<Technologies>().Property(t => t.CreatedDate).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Technologies>()
                        .HasOne(t => t.Departments)
                        .WithMany(d => d.Technologies)
                        .HasForeignKey(t => t.DepartmentId);

            modelBuilder.Entity<Departments>().ToTable("TblDepartment");
            modelBuilder.Entity<Departments>().Property(d => d.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Departments>().Property(d => d.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Departments>().Property(d => d.CreatedBy).HasDefaultValue("SYSTEM");
            modelBuilder.Entity<Departments>().Property(d => d.CreatedDate).HasDefaultValueSql("GETDATE()");
        }

    }
}
