using System.Collections.Generic;
using Domain;

namespace RepositoryInterface
{
    public interface IMenuRepository : IReadOnlyRepository<VMEN>
    {
        IList<VMEN> GetMenuByUser(TUSR user);
    }
}