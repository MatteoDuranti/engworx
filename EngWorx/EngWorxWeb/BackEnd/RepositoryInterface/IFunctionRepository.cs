using System.Collections;
using System.Collections.Generic;
using Domain;

namespace RepositoryInterface
{
    public interface IFunctionRepository : IRepository<TFNC>
    {
        IList GetMenuFunction(bool header = false);
        IList<TFNC> GetFunctionsWithPermissions(TROL role);
        IEnumerable<TFNC> GetFunctionAreas();
        IEnumerable<TFNC> GetAllFunctions();
    }
}