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
    public class UserController : ApiController
    {
        public TUSR GetUserRolesByCompanyAndUsername(TUSR user)
        {
            dbEntities db = new dbEntities();
            IUserRepository _repository = new UserRepository(db);
            TUSR ret = _repository.GetUserRolesByCompanyAndUsername(user);
            return ret;
        }

        // USARE ODATA
        public IList<TUSR> GetFilteredUsers(TUSR user)
        {
            dbEntities db = new dbEntities();
            IUserRepository _repository = new UserRepository(db);
            IList<TUSR> ret = _repository.GetFiltered(user);
            return ret;
        }

        public TUSR GetUserByPrimaryKey(TUSR user)
        {
            dbEntities db = new dbEntities();
            IUserRepository _repository = new UserRepository(db);
            TUSR ret = _repository.GetByPrimaryKey(user.CODUSR);
            return ret;
        }

        public int DeleteUser(TUSR user)
        {
            dbEntities db = new dbEntities();
            IUserRepository _repository = new UserRepository(db);
            TUSR u = _repository.GetUserRolesByPrimaryKey(user);
            int ret = _repository.DeleteOnSubmit(u, true);
            _repository.SaveChanges();
            return ret;
        }

        public bool UpdateUser(TUSR user)
        {
            dbEntities db = new dbEntities();
            IUserRepository _repository = new UserRepository(db);
            _repository.Update(user);
            _repository.SaveChanges();
            return true;
        }

        public int InsertUser(TUSR user)
        {
            dbEntities db = new dbEntities();
            IUserRepository _repository = new UserRepository(db);
            int ret = _repository.InsertOnSubmit(user);
            _repository.SaveChanges();
            return ret;
        }
    }
}
