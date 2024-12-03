using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Shared.VO
{
    public record SocialNetworksList
    {
        public IReadOnlyList<SocialNetwork> Networks { get; } = [];

        public SocialNetworksList() { }
        public SocialNetworksList(IEnumerable<SocialNetwork> networks)
        {
            Networks = networks.ToList();
        }
    }
}