using System.Collections.Generic;
using Domain;

namespace RepositoryInterface
{
    public interface IUserRolesRepository : IRepository<TUSRROL>
    {
        int AddRoleToUser(List<TUSRROL> lTuteRuo);
        int DeleteRoleToUser(List<TUSRROL> lTuteRuo);
        List<TUSRROL> GetUserRoles(TUSRROL userRoles);
    }
}