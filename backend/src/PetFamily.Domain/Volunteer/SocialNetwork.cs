using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Volunteer
{
    public class SocialNetwork
    {
        public Guid Guid { get; private set; }

        public string Name { get; private set; } = default!;

        public string Url { get; private set; } = default!;
    }
}
