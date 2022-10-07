using KodlamaDevs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OniCore.Security.Entities;

#nullable disable

namespace KodlamaDevs.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Technology> Technologies { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Developer> Developers { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(builder =>
            {
                builder.ToTable("ProgrammingLanguages").HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("Id");
                builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();
            });
            modelBuilder.Entity<ProgrammingLanguage>().HasData(new ProgrammingLanguage[]
            {
                new () { Id = 1, Name = "C#" },
                new () { Id = 2, Name = "Java" }
            });

            modelBuilder.Entity<Technology>(builder =>
            {
                builder.ToTable("Technologies").HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("Id");
                builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();
                builder.Property(x => x.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
            });
            modelBuilder.Entity<Technology>().HasData(new Technology[]
            {
                new () { Id = 1, Name = "ASP.NET", ProgrammingLanguageId = 1 },
                new () { Id = 2, Name = "WPF", ProgrammingLanguageId = 1 },
                new () { Id = 3, Name = "Spring", ProgrammingLanguageId = 2 },
                new () { Id = 4, Name = "JSP", ProgrammingLanguageId = 2 }
            });

            modelBuilder.Entity<ProgrammingLanguage>()
                        .HasMany(x => x.Technologies)
                        .WithOne(x => x.ProgrammingLanguage);


            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("Id");
                builder.Property(x => x.FirstName).HasColumnName("FirstName").HasMaxLength(50).IsRequired();
                builder.Property(x => x.LastName).HasColumnName("LastName").HasMaxLength(50).IsRequired();
                builder.Property(x => x.Email).HasColumnName("Email").HasMaxLength(100).IsRequired();
                builder.Property(x => x.PasswordHash).HasColumnName("PasswordHash").HasColumnType("varbinary(200)").IsRequired();
                builder.Property(x => x.PasswordSalt).HasColumnName("PasswordSalt").HasColumnType("varbinary(200)").IsRequired();
                builder.Property(x => x.Status).HasColumnName("Status").IsRequired();
            });

            modelBuilder.Entity<OperationClaim>(builder =>
            {
                builder.ToTable("OperationClaims").HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("Id");
                builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Developer>(builder =>
            {
                builder.ToTable("Developers");
                builder.Property(x => x.GithubAddress).HasColumnName("GithubAddress").HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<RefreshToken>(builder =>
            {
                builder.ToTable("RefreshTokens").HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("Id");
                builder.Property(x => x.UserId).HasColumnName("UserId");
                builder.Property(x => x.Token).HasColumnName("Token").HasMaxLength(50);
                builder.Property(x => x.Created).HasColumnName("Created");
                builder.Property(x => x.CreatedByIp).HasColumnName("CreatedByIp").HasMaxLength(20);
                builder.Property(x => x.Expiration).HasColumnName("Expiration");
                builder.Property(x => x.ReplacedToken).HasColumnName("ReplacedToken").HasMaxLength(50);
                builder.Property(x => x.Revoked).HasColumnName("Revoked");
                builder.Property(x => x.RevokedByIp).HasColumnName("RevokedByIp").HasMaxLength(20);
                builder.Property(x => x.RevokeReason).HasColumnName("RevokeReason").HasMaxLength(100);
            });

            modelBuilder.Entity<User>()
                        .HasMany(x => x.OperationClaims)
                        .WithMany(x => x.Users)
                        .UsingEntity(b =>
                        {
                            b.ToTable("UserOperationClaims");
                            b.Property("UsersId").HasColumnName("UserId");
                            b.Property("OperationClaimsId").HasColumnName("OperationClaimId");
                        });

            modelBuilder.Entity<User>()
                        .HasMany(x => x.RefreshTokens)
                        .WithOne(x => x.User);
        }
    }
}