using KodlamaDevs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KodlamaDevs.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(builder =>
            {
                builder.ToTable("ProgrammingLanguages").HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("Id");
                builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(50);
            });

            modelBuilder.Entity<ProgrammingLanguage>().HasData(new ProgrammingLanguage[]
            {
                new () { Id = 1, Name = "C#" },
                new () { Id = 2, Name = "Java" }
            });
        }
    }
}
