using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Domain;
using RepositoryInterface;

namespace Repository
{
    public class MenuRepository : ReadOnlyRepository<VMEN>, IMenuRepository
    {
        public MenuRepository(dbEntities db)
            : base(db)
        {
        }

        public IList<VMEN> GetMenuByUser(TUSR user)
	    {
		    var menuQuery = (from m in db.Set<VMEN>()
                                 where m.CODUSR.Equals(user.CODUSR)
                                 select m).Distinct();
            var orderedList = menuQuery.OrderBy(x => x.SORPAT).ToList();
		    return orderedList;
	    }
    }
}