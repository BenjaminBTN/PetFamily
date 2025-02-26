﻿using PetFamily.Application.Abstractions;

namespace PetFamily.Application.SpeciesManagement.Commands.Create;

public record CreateSpeciesCommand(string Name) : ICommand;
