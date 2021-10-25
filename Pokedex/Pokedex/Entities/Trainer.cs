using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pokedex.Entities
{
    public class Trainer
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }

    }
}
