using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Volunteers
{
    public class SocialNetwork
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = default!;

        public string Url { get; private set; } = default!;
    }
}
