using Blog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository.Core
{
    public partial class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext()
            {
            }

            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }
            public virtual DbSet<BlogInfo>? BlogInfos { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<BlogInfo>(entity =>
                {
                    //??
                    //entity.ToTable("BlogInfo");
                    //entity.Property(e => e.BlogID).HasColumnName("EmployeeID");
                    //entity.Property(e => e.NationalIDNumber).HasMaxLength(15).IsUnicode(false);
                    //entity.Property(e => e.EmployeeName).HasMaxLength(100).IsUnicode(false);
                    //entity.Property(e => e.LoginID).HasMaxLength(256).IsUnicode(false);
                    //entity.Property(e => e.JobTitle).HasMaxLength(50).IsUnicode(false);
                    //entity.Property(e => e.BirthDate).IsUnicode(false);
                    //entity.Property(e => e.MaritalStatus).HasMaxLength(1).IsUnicode(false);
                    //entity.Property(e => e.Gender).HasMaxLength(1).IsUnicode(false);
                    //entity.Property(e => e.HireDate).IsUnicode(false);
                    //entity.Property(e => e.VacationHours).IsUnicode(false);
                    //entity.Property(e => e.SickLeaveHours).IsUnicode(false);
                    //entity.Property(e => e.RowGuid).HasMaxLength(50).IsUnicode(false);
                    //entity.Property(e => e.ModifiedDate).IsUnicode(false);
                });
                OnModelCreatingPartial(modelBuilder);
            }

            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        }
 
}