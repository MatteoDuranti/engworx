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
    public class RoleController : ApiController
    {
        public TUSR GetUserRolesByCompanyAndUsername(TUSR user)
        {
            dbEntities db = new dbEntities();
            IUserRepository _repository = new UserRepository(db);
            TUSR ret = _repository.GetUserRolesByCompanyAndUsername(user);
            return ret;
        }

        // USARE ODATA
        public IList<TROL> GetAllRoles()
        {
            dbEntities db = new dbEntities();
            IRoleRepository _repository = new RoleRepository(db);
            IList<TROL> ret = _repository.GetAll();
            return ret;
        }

        public IList<TROL> GetFilteredRoles(TROL role)
        {
            dbEntities db = new dbEntities();
            IRoleRepository _repository = new RoleRepository(db);
            IList<TROL> ret = _repository.GetFiltered(role);
            return ret;
        }

        public TROL GetRoleByPrimaryKey(TROL role)
        {
            dbEntities db = new dbEntities();
            IRoleRepository _repository = new RoleRepository(db);
            TROL ret = _repository.GetByPrimaryKey(role.CODROL);
            return ret;
        }

        public IList<TROL> GetRolesAssociableToUser(TUSR user)
        {
            dbEntities db = new dbEntities();
            IRoleRepository _repository = new RoleRepository(db);
            IList<TROL> ret = _repository.GetRolesAssociableToUser(user);
            return ret;
        }

        public IList<TROL> GetRolesAssociatedToUser(TUSR user)
        {
            dbEntities db = new dbEntities();
            IRoleRepository _repository = new RoleRepository(db);
            IList<TROL> ret = _repository.GetRolesAssociatedToUser(user);
            return ret;
        }

        public IList<TROL> GetRolesAndAllowedFunctions()
        {
            dbEntities db = new dbEntities();
            IRoleRepository _repository = new RoleRepository(db);
            IList<TROL> ret = _repository.GetRolesAndAllowedFunctions();
            return ret;
        }

        public IList<TROL> getRolesByControllerAction(TFNC func)
        {
            dbEntities db = new dbEntities();
            IRoleRepository _repository = new RoleRepository(db);
            IList<TROL> ret = _repository.GetRolesByControllerAction(func.DESCTL, func.DESACTCTL);
            return ret;
        }

        public int DeleteRole(TROL role)
        {
            dbEntities db = new dbEntities();
            IRoleRepository _repository = new RoleRepository(db);
            TROL r = _repository.GetRoleUsersFuncs(role);
            int ret = _repository.DeleteOnSubmit(r, true);
            _repository.SaveChanges();
            return ret;
        }

        public bool UpdateRole(TROL role)
        {
            dbEntities db = new dbEntities();
            IRoleRepository _repository = new RoleRepository(db);
            _repository.Update(role);
            _repository.SaveChanges();
            return true;
        }

        public int InsertRole(TROL role)
        {
            dbEntities db = new dbEntities();
            IRoleRepository _repository = new RoleRepository(db);
            int ret = _repository.InsertOnSubmit(role);
            _repository.SaveChanges();
            return ret;
        }
    }
}
