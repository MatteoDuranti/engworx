using Domain;

namespace RepositoryInterface
{
    public interface IUserRepository : IRepository<TUSR>
    {
        TUSR GetUserRolesByCompanyAndUsername(TUSR user);
        TUSR GetUserRolesByPrimaryKey(TUSR user);
    }
}