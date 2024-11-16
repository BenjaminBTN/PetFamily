using System.Collections.Generic;

namespace PetFamily.Domain.Volunteers
{
    public record VolunteerDetails
    {
        //private readonly List<SocialNetwork> _networks = [];
        //private readonly List<VolunteerRequisite> _requisites = [];

        //private VolunteerDetails(List<SocialNetwork> networks, List<VolunteerRequisite> requisites)
        //{
        //    _networks = networks;
        //    _requisites = requisites;
        //}

        public List<SocialNetwork> Networks { get; private set; } = [];
        public List<VolunteerRequisite> Requisites { get; private set; } = [];

        //public static Result<VolunteerDetails> Create(List<SocialNetwork> networks, List<VolunteerRequisite> requisites)
        //{
        //    return new VolunteerDetails(networks, requisites);
        //}
    }
}