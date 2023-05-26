using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Blog.Entities
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
                modelBuilder.Entity<BlogInfo>()
                    .Property(f => f.ID)
                    .ValueGeneratedOnAdd();


            OnModelCreatingPartial(modelBuilder);
            }

            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        }
 
}