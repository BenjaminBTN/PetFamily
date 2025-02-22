using System;

namespace PetFamily.Application.Dtos;

public class BreedDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
