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
    public class FunctionController : ApiController
    {
        public IList<TFNC> GetFunctionsWithPermissions(TROL role)
        {
            dbEntities db = new dbEntities();
            IFunctionRepository _repository = new FunctionRepository(db);
            IList<TFNC> ret = _repository.GetFunctionsWithPermissions(role);
            return ret;
        }

        public TROLFNC InsertDeleteAssociationFunctToRole(TROLFNC roleFunc)
        {
            dbEntities db = new dbEntities();
            IRoleFunctionsRepository _repository = new RoleFunctionsRepository(db);
            // CONTROLLARE SE SIAMO IN CANCELLAZIONE O INSERIMENTO
            TROLFNC perm = _repository.GetByPrimaryKey(roleFunc.CODROL, roleFunc.CODFNC);
            if (perm != null)
            {
                // CANCELLAZIONE
                _repository.DeleteOnSubmit(perm);
            }
            else
            {
                // INSERIMENTO
                perm = new TROLFNC();
                perm.CODFNC = roleFunc.CODFNC;
                perm.CODROL = roleFunc.CODROL;
                perm.DATASC = DateTime.Now;
                _repository.InsertOnSubmit(perm);
            }
            _repository.SaveChanges();
            return perm;
        }
    }
}
