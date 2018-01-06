using System.Collections.Generic;
using Domain;

namespace RepositoryInterface
{
    public interface IRoleRepository : IRepository<TROL>
    {
        IList<TROL> GetRolesByControllerAction(string controllerName, string actionName);
        IList<TROL> GetRolesAssociatedToUser(TUSR user);
        IList<TROL> GetRolesAssociableToUser(TUSR user);
        TROL GetRoleUsersFuncs(TROL role);
        IList<TROL> GetRolesAndAllowedFunctions();
    }
}