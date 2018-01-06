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
    public class UserRolesRepository : Repository<TUSRROL>, IUserRolesRepository
    {
        public UserRolesRepository(dbEntities db)
            : base(db)
        {
        }

        public int AddRoleToUser(List<TUSRROL> lTuteRuo)
        {
            int result = 0;
            foreach (TUSRROL oT in lTuteRuo)
            {
                oT.DATASC = System.DateTime.Now;
                db.Set<TUSRROL>().Add(oT);
                result = SaveChanges();
            }
            return result;
        }

        public int DeleteRoleToUser(List<TUSRROL> lTuteRuo)
        {
            int result = 0;
            TUSRROL tuteruo = new TUSRROL();
            foreach (TUSRROL oT in lTuteRuo)
            {
                db.Set<TUSRROL>().Remove(oT);
            }
            result = SaveChanges();
            return result;
        }

        public List<TUSRROL> GetUserRoles(TUSRROL userRoles)
	    {
		    var lista = from ur in db.Set<TUSRROL>()
                        where userRoles.CODROL.Equals(ur.CODROL) && userRoles.CODUSR.Equals(ur.CODUSR)
                        select ur;

		    if (!string.IsNullOrEmpty(userRoles.CODGRPARE)) {
			    lista = lista.Where(x => userRoles.CODGRPARE.Equals(x.CODGRPARE));
		    }
		    return lista.ToList();
	    }
    }
}