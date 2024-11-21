using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Shared.VO
{
    public record SocialNetworksList
    {
        public IReadOnlyList<SocialNetwork> Networks { get; }


        private SocialNetworksList(IEnumerable<SocialNetwork> networks)
        {
            Networks = networks.ToList();
        }


        public static Result<SocialNetworksList, Error> Create(IEnumerable<SocialNetwork> networks)
        {
            if(networks == null)
                return Errors.General.NullValue("Networks");

            return new SocialNetworksList(networks);
        }
    }
}