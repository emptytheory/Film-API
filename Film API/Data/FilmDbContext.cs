using Film_API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Film_API.Data
{
    public class FilmDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public FilmDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Franchise franchise1 = new() { Id = 1, Name = "HATE CRIME Apes", Description = "Hero Apes Try to End Crime. Criminals and minorities alike fear the 'HATE CRIME Apes'." };
            Franchise franchise2 = new() { Id = 2, Name = "Disaster Fish" };

            Movie movie1 = new() { Id = 1, Title = "Planet of the Cakes", Genre = "Sci-fi, Cooking", ReleaseYear = 1999, Director = "Julius Cream", FranchiseId = franchise1.Id};
            Movie movie2 = new() { Id = 2, Title = "King Thong", Genre = "Adventure, Adult", ReleaseYear = 2020, Director = "Fran-Sisqó Ford Copulate", FranchiseId = franchise1.Id};
            Movie movie3 = new() { Id = 3, Title = "Codzilla", Genre = "Fishing, Action", ReleaseYear = 1988, Director = "Salman Fisher", FranchiseId = franchise2.Id };

            Character character1 = new() { Id = 1, FullName = "Ethan Food" };
            Character character2 = new() { Id = 2, FullName = "Ace O. Bass", Alias = "Ass of Bass" };
            Character character3 = new() { Id = 3, FullName = "Ape Lincon" };

            modelBuilder.Entity<Character>().HasData(character1, character2, character3);
            modelBuilder.Entity<Movie>().HasData(movie1, movie2, movie3);
            modelBuilder.Entity<Franchise>().HasData(franchise1, franchise2);

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
                        new { MovieId = movie1.Id, CharacterId = character1.Id },
                        new { MovieId = movie1.Id, CharacterId = character3.Id },
                        new { MovieId = movie2.Id, CharacterId = character3.Id },
                        new { MovieId = movie3.Id, CharacterId = character2.Id }
                    );
                }
            );
        }
    }
}
