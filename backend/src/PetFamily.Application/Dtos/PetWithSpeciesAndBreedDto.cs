using System;

namespace PetFamily.Application.Dtos;

public class PetWithSpeciesAndBreedDto
{
    // public Guid Id { get; set;}
    public Guid SpeciesId { get; set; }
    public Guid BreedId { get; set; }
}
