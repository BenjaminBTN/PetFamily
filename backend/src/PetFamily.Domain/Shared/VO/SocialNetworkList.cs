using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Shared.VO
{
    public record SocialNetworkList
    {
        public IReadOnlyList<SocialNetwork> Networks { get; } = [];

        public SocialNetworkList() { }
        public SocialNetworkList(IEnumerable<SocialNetwork> networks)
        {
            Networks = networks.ToList();
        }
    }
}