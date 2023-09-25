using Film_API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Film_API.Data
{
    public class FilmDbContext : DbContext
    {
        // this is where we configure what classes in our application are going to be tables in the database. where the db is
        // (the connection string is here)
        // seeded data

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Franchise> Franshises { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = N-NO-01-01-5770\\SQLEXPRESS; Initial Catalog = Film; Integrated Security = true; Trust Server Certificate = true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Movie movie1 = new() { Id = 1, Title = "Planet of the Cakes", Genre = "Sci-fi, Cooking", ReleaseYear = 1999, Director = "Julius Cream" };
            Movie movie2 = new() { Id = 2, Title = "King Thong", Genre = "Adventure, Adult", ReleaseYear = 2020, Director = "Fran-Sisqó Ford Copulate" };
            Movie movie3 = new() { Id = 3, Title = "Codzilla", Genre = "Fishing, Action", ReleaseYear = 1988, Director = "Salmal Fisher" };

            Character character1 = new() { Id = 1, FullName = "Ethan Food" };
            Character character2 = new() { Id = 2, FullName = "Ace O. Bass", Alias = "Ass of Bass" };
            Character character3 = new() { Id = 3, FullName = "Ape Lincon" };

            modelBuilder.Entity<Character>().HasData(character1, character2, character3);
            modelBuilder.Entity<Movie>().HasData(movie1, movie2, movie3);

            modelBuilder.Entity<Movie>()
            .HasMany(std => std.Characters)
            .WithMany(sub => sub.Movies)
            .UsingEntity<Dictionary<string, object>>(
                "MovieCharacter",
                l => l.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
                r => r.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                je =>
                {
                    je.HasKey("MovieId", "CharacterId");
                    je.HasData(
                        new { MovieId = 1, CharacterId = 1 },
                        new { MovieId = 1, CharacterId = 3 },
                        new { MovieId = 2, CharacterId = 3 },
                        new { MovieId = 3, CharacterId = 2 }
                    );
                }
            );
        }
    }
}
