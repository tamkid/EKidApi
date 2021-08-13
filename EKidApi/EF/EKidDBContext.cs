using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EKidApi.EF
{
    public partial class EKidDBContext : DbContext
    {
        public EKidDBContext()
        {
        }

        public EKidDBContext(DbContextOptions<EKidDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Vob> Vobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Vob>(entity =>
            {
                entity.ToTable("Vob");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Meaning).HasMaxLength(100);

                entity.Property(e => e.Spelling).HasMaxLength(50);

                entity.Property(e => e.Word)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
