using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WASP.Models
{
    public partial class HiveContext : DbContext
    {
        public HiveContext(DbContextOptions<HiveContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Citizen> Citizens { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<IssueState> IssueStates { get; set; }
        public virtual DbSet<Municipality> Municipalities { get; set; }
        public virtual DbSet<MunicipalityResponse> MunicipalityResponses { get; set; }
        public virtual DbSet<MunicipalityUser> MunicipalityUsers { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<ReportCategory> ReportCategories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Verification> Verifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Hive;Trusted_Connection=True;", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Danish_Norwegian_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Citizen>(entity =>
            {
                entity.HasIndex(e => new { e.Email, e.PhoneNo }, "Citizens_LoginInformation_UQ")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Municipality)
                    .WithMany(p => p.Citizens)
                    .HasForeignKey(d => d.MunicipalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Citizens__Munici__276EDEB3");
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateEdited).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Location).IsRequired();

                entity.Property(e => e.Picture1).IsUnicode(false);

                entity.Property(e => e.Picture2).IsUnicode(false);

                entity.Property(e => e.Picture3).IsUnicode(false);

                entity.HasOne(d => d.Citizen)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.CitizenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Issues__CitizenI__35BCFE0A");

                entity.HasOne(d => d.IssueState)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.IssueStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Issues__IssueSta__37A5467C");

                entity.HasOne(d => d.Municipality)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.MunicipalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Issues__Municipa__36B12243");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => new { d.SubCategoryId, d.CategoryId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Issues__38996AB5");
            });

            modelBuilder.Entity<IssueState>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Municipality>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MunicipalityResponse>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateEdited).HasColumnType("datetime");

                entity.Property(e => e.Response)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.MunicipalityResponses)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Municipal__Issue__3B75D760");

                entity.HasOne(d => d.MunicipalityUser)
                    .WithMany(p => p.MunicipalityResponses)
                    .HasForeignKey(d => d.MunicipalityUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Municipal__Munic__3C69FB99");
            });

            modelBuilder.Entity<MunicipalityUser>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Municipa__A9D10534F65BA803")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Municipality)
                    .WithMany(p => p.MunicipalityUsers)
                    .HasForeignKey(d => d.MunicipalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Municipal__Munic__32E0915F");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reports__IssueId__412EB0B6");

                entity.HasOne(d => d.ReportCategory)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ReportCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reports__ReportC__4222D4EF");
            });

            modelBuilder.Entity<ReportCategory>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CategoryId })
                    .HasName("PK_IdCategoryId")
                    .IsClustered(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubCatego__Categ__2D27B809");
            });

            modelBuilder.Entity<Verification>(entity =>
            {
                entity.HasIndex(e => new { e.IssueId, e.CitizenId }, "IssueVerifications_UQ")
                    .IsUnique();

                entity.HasOne(d => d.Citizen)
                    .WithMany(p => p.Verifications)
                    .HasForeignKey(d => d.CitizenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Verificat__Citiz__46E78A0C");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.Verifications)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Verificat__Issue__45F365D3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
