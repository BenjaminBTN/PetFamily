using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteersManagement.Queries.GetVolunteerById;

public record class GetVolunteerByIdQuery(Guid VolunteerId) : IQuery;
