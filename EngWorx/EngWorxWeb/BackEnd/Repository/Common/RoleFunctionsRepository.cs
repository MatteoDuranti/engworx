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
    public class RoleFunctionsRepository : Repository<TROLFNC>, IRoleFunctionsRepository
    {

        public RoleFunctionsRepository(dbEntities db)
            : base(db)
        {
        }

        public int AddFunctionToRole(TROLFNC roleFun)
        {
            int result = 0;
            TROLFNC trolfnc = new TROLFNC();
            trolfnc.CODROL = roleFun.CODROL;
            trolfnc.CODFNC = roleFun.CODFNC;
            trolfnc.DATASC = DateTime.Now;
            db.Set<TROLFNC>().Add(trolfnc);
            result = SaveChanges();
            return result;
        }

        public int DeleteFunctionToRole(TROLFNC roleFun)
        {
            int result = 0;
            TROLFNC trolfnc = GetByPrimaryKey(roleFun.CODROL, roleFun.CODFNC);
            db.Set<TROLFNC>().Remove(trolfnc);
            result = SaveChanges();
            return result;
        }

        public List<TROLFNC> GetFunctionsForRole(TROL role)
	    {
            List<TROLFNC> truofunlst = (from ruofun in db.Set<TROLFNC>()
                                        where ruofun.CODROL.Equals(role.CODROL)
                                        select ruofun).ToList();
		    return truofunlst;
	    }
    }
}