using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Models;

namespace PetFamily.Application.VolunteersManagement.Queries.GetVolunteerById;

public class GetVolunteerByIdHandlerDapper : IQueryHandler<PagedList<VolunteerDto>, GetVolunteerByIdQuery>
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly ILogger<GetVolunteerByIdHandlerDapper> _logger;

    public GetVolunteerByIdHandlerDapper(
        ILogger<GetVolunteerByIdHandlerDapper> logger,
        ISqlConnectionFactory connectionFactory)
    {
        _logger = logger;
        _connectionFactory = connectionFactory;
    }


    public async Task<PagedList<VolunteerDto>> Handle(GetVolunteerByIdQuery query, CancellationToken ct)
    {
        var connection = _connectionFactory.Create();

        var parameters = new DynamicParameters();
        parameters.Add("@VolunteerId", query.VolunteerId);

        var sql =
        """
        SELECT id, description, email, experience, phone_number, name, surname, patronymic, requisites, networks FROM volunteers WHERE id = @VolunteerId
        """;

        var volunteerDto = await connection.QueryAsync<
            VolunteerDto, string, string, string, string, string, VolunteerDto>(
                sql,

                (volunteerDto, name, surname, patronymic, jsonRequisites, jsonNetworks) =>
                {
                    var fullName = new FullNameDto(name, surname, patronymic);

                    var requisites = JsonSerializer
                        .Deserialize<VolunteerRequisiteDto[]>(jsonRequisites) ?? [];

                    var networks = JsonSerializer
                        .Deserialize<SocialNetworkDto[]>(jsonNetworks) ?? [];

                    volunteerDto.FullName = fullName;
                    volunteerDto.Requisites = requisites;
                    volunteerDto.Networks = networks;

                    return volunteerDto;
                },

                splitOn: "name,surname,patronymic,requisites,networks",

                param: parameters);

        return new PagedList<VolunteerDto>
        {
            Items = [.. volunteerDto],
            TotalCount = volunteerDto.Count()
        };
    }
}
