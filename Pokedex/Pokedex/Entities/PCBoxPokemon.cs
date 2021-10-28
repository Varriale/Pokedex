using System.ComponentModel.DataAnnotations;

namespace Pokedex.Entities
{
    public class PCBoxPokemon:NamedAPIResource<PokemonApiModel>
    {
        [Key]
        public long Id { get; set; }
        public long TrainerId { get; set; }
        public virtual Trainer Trainer{ get; set; }

    }
}
