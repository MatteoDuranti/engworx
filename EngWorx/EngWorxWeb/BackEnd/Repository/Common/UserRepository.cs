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
    public class UserRepository : Repository<TUSR>, IUserRepository
    {
        public UserRepository(dbEntities db)
            : base(db)
        {
        }

        protected override bool DeleteOnCascade(TUSR entity)
        {
            foreach (TUSRROL role in entity.TUSRROL.ToList())
            {
                entity.TUSRROL.Remove(role);
            }
            return true;
        }
        
    	public TUSR GetUserRolesByCompanyAndUsername(TUSR user)
	    {
		    TUSR u = (from c in db.Set<TUSR>().Include("TUSRROL.TROL")
                      where (c.CODUSR.Trim().Equals(user.CODUSR))
                      select c).FirstOrDefault();
		    return u;
	    }

    	public TUSR GetUserRolesByPrimaryKey(TUSR user)
	    {
		    TUSR u = (from c in db.Set<TUSR>().Include("TUSRROL.TROL")
                      where (user.CODUSR.Equals(c.CODUSR))
                      select c).FirstOrDefault();
		    return u;
	    }
    }
}