using Domain;
using Repository;
using RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class UserRoleController : ApiController
    {
        public bool DeleteUserRole(IList<TUSRROL> userRoles)
        {
            dbEntities db = new dbEntities();
            IUserRolesRepository _repository = new UserRolesRepository(db);
            foreach(TUSRROL ur in userRoles)
            {
                _repository.GetUserRoles(ur).ForEach(u => _repository.DeleteOnSubmit(u));
            }
            _repository.SaveChanges();
            return true;
        }

        public bool InsertUserRole(IList<TUSRROL> userRoles)
        {
            dbEntities db = new dbEntities();
            IUserRolesRepository _repository = new UserRolesRepository(db);
            foreach (TUSRROL ur in userRoles)
            {
                //ur.DATASC = DateTime.Now;
                _repository.InsertOnSubmit(ur);
            }
            _repository.SaveChanges();
            return true;
        }
    }
}
