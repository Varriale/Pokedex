using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Pokedex.Entities
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public virtual DbSet<Trainer> Trainers { get; set; }
        public virtual DbSet<PCBoxPokemon> PCBoxPokemons { get; set; }


        public static readonly LoggerFactory DbCommandDebugLoggerFactory
            = new();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                          .EnableSensitiveDataLogging(true)
                          .UseLoggerFactory(DbCommandDebugLoggerFactory);
        }

        #region Overridden method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainer>().HasData(GetTrainers());
            modelBuilder.Entity<PCBoxPokemon>().HasData(GetPC());
            base.OnModelCreating(modelBuilder);
        }
        #endregion
        #region Private method
        private List<Trainer> GetTrainers()
        {
            return new List<Trainer>
        {
            new Trainer{Id=1, Name="Ash", Gender=Gender.Boy },
            new Trainer{Id=2, Name="Misty", Gender=Gender.Girl},
            new Trainer{Id=3, Name="Brock", Gender=Gender.Boy},
        };
        }
        private List<PCBoxPokemon> GetPC()
        {
            return new List<PCBoxPokemon>
        {
            new PCBoxPokemon{Id = 1,TrainerId = 1,Name= "bulbasaur",Url = "https://pokeapi.co/api/v2/pokemon/1" }
        };
        }
        #endregion
    }
}