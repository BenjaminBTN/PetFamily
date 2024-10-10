using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Volunteer
{
    public class Requisites
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = default!;

        public string Description { get; private set; } = default!;
    }
}
