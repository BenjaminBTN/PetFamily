using System.Data;

namespace PetFamily.Application.Database;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}
