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
    public class FunctionRepository : Repository<TFNC>, IFunctionRepository
    {
        public FunctionRepository(dbEntities db)
            : base(db)
        {
        }

        public IList GetMenuFunction(bool header = false)
	    {
            var lista = (from m in db.Set<TFNC>()
                         select m);
            if ((header))
            {
                lista = lista.Where(x => string.IsNullOrEmpty(x.DESCTL) && string.IsNullOrEmpty(x.DESACTCTL));
            }
            return lista.ToList();
	    }

        public IList<TFNC> GetFunctionsWithPermissions(TROL role)
	    {
            // TODO da mettere a posto
            var lista = (from f in db.Set<TFNC>().Include("TROLFNC")
                         from rf in db.Set<TROLFNC>().Where(x => x.CODFNC == f.CODFNC && role.CODROL.Equals(x.CODROL)).DefaultIfEmpty()
                         where (!string.IsNullOrEmpty(f.DESCTL) && !string.IsNullOrEmpty(f.DESACTCTL))
                         orderby f.DESFNC
                         select f);
            return lista.ToList();
	    }

        public IEnumerable<TFNC> GetFunctionAreas()
        {
            var funList = (from funs in db.Set<TFNC>()
                               where funs.DESACTCTL.Equals(null)
                               select funs).Distinct();
            return funList.ToList();
        }

        public IEnumerable<TFNC> GetAllFunctions()
	    {
            dynamic funList = (from funs in db.Set<TFNC>()
                               where !funs.DESACTCTL.Equals(null)
                               select funs).Distinct();
            return funList.ToList();
	    }
    }
}


