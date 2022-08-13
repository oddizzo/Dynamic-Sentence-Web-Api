using Microsoft.EntityFrameworkCore;
using Dynamic_Sentence_Web_Api.Models;

namespace Dynamic_Sentence_Web_Api.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<WordType> WordTypes { get; set; }
        public DbSet<WordUnit> WordUnits { get; set; }
        public DbSet<Sentence> Sentences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordType>().ToTable("WordType");
            modelBuilder.Entity<WordUnit>().ToTable("WordUnit");
            modelBuilder.Entity<Sentence>().ToTable("Sentence");
        }
    }
}
