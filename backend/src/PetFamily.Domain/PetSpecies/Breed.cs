using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.PetSpecies
{
    public class Breed
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = default!;
    }
}
