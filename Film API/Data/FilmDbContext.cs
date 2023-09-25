using Film_API.Data.Entities;
using Microsoft.EntityFrameworkCore;

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
    }
}
