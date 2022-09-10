using KodlamaDevs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KodlamaDevs.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Technology> Technologies { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.Entity<ProgrammingLanguage>(builder =>
            {
                builder.ToTable("ProgrammingLanguages").HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("Id");
                builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(50);

                builder.HasMany(x => x.Technologies);
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
                builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(50);
                builder.Property(x => x.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");

                builder.HasOne(x => x.ProgrammingLanguage);
            });
            modelBuilder.Entity<Technology>().HasData(new Technology[]
            {
                new () { Id = 1, Name = "ASP.NET", ProgrammingLanguageId = 1 },
                new () { Id = 2, Name = "WPF", ProgrammingLanguageId = 1 },
                new () { Id = 3, Name = "Spring", ProgrammingLanguageId = 2 },
                new () { Id = 4, Name = "JSP", ProgrammingLanguageId = 2 }
            });

            modelBuilder.Entity<Technology>().Navigation(x => x.ProgrammingLanguage).AutoInclude();
            modelBuilder.Entity<ProgrammingLanguage>().Navigation(x => x.Technologies).AutoInclude();
        }
    }
}