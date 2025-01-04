using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BlackSquares.Models;

namespace BlackSquares.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Meme> Memes { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(b => b
                    .AddConsole()
                    .AddFilter(level => level >= LogLevel.Warning)))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}
