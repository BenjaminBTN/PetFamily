﻿using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Volunteers.VO
{
    public record VolunteerRequisiteList
    {
        public IReadOnlyList<VolunteerRequisite> Requisites { get; } = new List<VolunteerRequisite>();

        public VolunteerRequisiteList() { }
        public VolunteerRequisiteList(IEnumerable<VolunteerRequisite> requisites)
        {
            Requisites = requisites.ToList();
        }
    }
}
