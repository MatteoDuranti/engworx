using System;

namespace RepositoryInterface
{
    public interface IDbFunctionRepository
    {
        bool CheckDbConnection();
        string GetConnectionString();
    }
}
