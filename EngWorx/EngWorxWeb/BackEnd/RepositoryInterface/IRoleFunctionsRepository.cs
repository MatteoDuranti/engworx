using Domain;
using System.Collections.Generic;

namespace RepositoryInterface
{
    public interface IRoleFunctionsRepository : IRepository<TROLFNC>
    {
        int AddFunctionToRole(TROLFNC roleFun);
        int DeleteFunctionToRole(TROLFNC roleFun);
        List<TROLFNC> GetFunctionsForRole(TROL role);
    }
}