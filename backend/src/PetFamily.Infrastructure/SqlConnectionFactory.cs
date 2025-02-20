using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using PetFamily.Application.Database;

namespace PetFamily.Infrastructure;


public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection Create()
    {
        return new NpgsqlConnection(_configuration.GetConnectionString(Constants.DATABASE));
    }
}
