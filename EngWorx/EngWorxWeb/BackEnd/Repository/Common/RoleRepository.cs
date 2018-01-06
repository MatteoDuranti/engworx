using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.Entity;
using System.Linq;
using Domain;
using RepositoryInterface;

namespace Repository
{
    public class RoleRepository : Repository<TROL>, IRoleRepository
    {

        public RoleRepository(dbEntities db)
            : base(db)
        {
        }

        public IList<TROL> GetRolesByControllerAction(string controllerName, string actionName)
	    {
		    var roles = (from r in db.Set<TROL>()
                         join rf in db.Set<TROLFNC>() on r.CODROL equals rf.CODROL
                         join f in db.Set<TFNC>() on rf.CODFNC equals f.CODFNC
                         where f.DESCTL.Trim().ToUpper().Equals(controllerName.Trim().ToUpper()) && f.DESACTCTL.Trim().ToUpper().Equals(actionName.Trim().ToUpper())
                         select r).Distinct();
		    return roles.ToList();
	    }

        public IList<TROL> GetRolesAndAllowedFunctions()
        {
            var roles = (from r in db.Set<TROL>().Include("TROLFNC")
                         join rf in db.Set<TROLFNC>() on r.CODROL equals rf.CODROL
                         join f in db.Set<TFNC>() on rf.CODFNC equals f.CODFNC
                         select r).Distinct();
		    return roles.Include("TROLFNC").Include("TROLFNC.TFNC").ToList();
        }

        public IList<TROL> GetRolesAssociatedToUser(TUSR user)
        {
            var roles = (from ur in db.Set<TUSRROL>()
                         join r in db.Set<TROL>() on ur.CODROL equals r.CODROL
                         where (user.CODUSR.Equals(ur.CODUSR))
                         select r).Distinct();
            return roles.ToList();
        }

        public IList<TROL> GetRolesAssociableToUser(TUSR user)
        {
            var roles = (from ur in db.Set<TUSRROL>()
                     join r in db.Set<TROL>() on ur.CODROL equals r.CODROL
                     where (user.CODUSR.Equals(ur.CODUSR))
                     select r).Distinct();
            var roles1 = (from r in db.Set<TROL>()
                          where !(roles.Contains(r))
                          select r).Distinct();
            return roles1.ToList();
        }

        public TROL GetRoleUsersFuncs(TROL role)
        {
            TROL u = (from c in db.Set<TROL>().Include("TUSRROL").Include("TROLFNC")
                      where (role.CODROL.Equals(c.CODROL))
                      select c).FirstOrDefault();
            return u;
        }
    }
}