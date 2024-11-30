using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Shared.VO
{
    public record SocialNetworksList
    {
        public IReadOnlyList<SocialNetwork> Networks { get; }

        private SocialNetworksList() { }
        private SocialNetworksList(List<SocialNetwork> networks)
        {
            Networks = networks;
        }


        public static Result<SocialNetworksList, Error> Create(IEnumerable<SocialNetwork> networks)
        {
            if(networks == null)
                return Errors.General.NullValue("Networks");

            return new SocialNetworksList(networks.ToList());
        }
    }
}