using ClassLibraryInfinity.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamenWeb
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

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Publication> Publications { get; set; }

        public virtual DbSet<Userweb> Userwebs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("name=DevConnection");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.IdComment);

                entity.ToTable("comment");

                entity.Property(e => e.IdComment).HasColumnName("ID_COMMENT");
                entity.Property(e => e.Comment1)
                    .HasMaxLength(500)
                    .IsFixedLength()
                    .HasColumnName("comment");
                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");
                entity.Property(e => e.FkIdPublication).HasColumnName("FK_ID_PUBLICATION");
                entity.Property(e => e.Nickname)
                    .HasMaxLength(50)
                    .IsFixedLength()
                    .HasColumnName("nickname");

                entity.HasOne(d => d.FkIdPublicationNavigation).WithMany(p => p.Comments)
                    .HasForeignKey(d => d.FkIdPublication)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_comment_publication");
            });

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.HasKey(e => e.IdPublication);

                entity.ToTable("publication");

                entity.Property(e => e.IdPublication).HasColumnName("ID_PUBLICATION");
                entity.Property(e => e.Body)
                    .HasMaxLength(500)
                    .IsFixedLength()
                    .HasColumnName("body");
                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");
                entity.Property(e => e.Nickname)
                    .HasMaxLength(50)
                    .IsFixedLength()
                    .HasColumnName("nickname");
                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsFixedLength()
                    .HasColumnName("title");

                entity.HasOne(d => d.NicknameNavigation).WithMany(p => p.Publications)
                    .HasForeignKey(d => d.Nickname)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired(false)
                    .HasConstraintName("FK_publication_userweb");
            });

            modelBuilder.Entity<Userweb>(entity =>
            {
                entity.HasKey(e => e.Nickname);

                entity.ToTable("userweb");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(50)
                    .IsFixedLength()
                    .HasColumnName("nickname");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength()
                    .HasColumnName("email");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsFixedLength()
                    .HasColumnName("name");
                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsFixedLength()
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
   
}
