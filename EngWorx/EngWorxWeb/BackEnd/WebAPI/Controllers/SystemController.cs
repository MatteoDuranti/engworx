using Domain;
using Repository;
using RepositoryInterface;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class SystemController : ApiController
    {
        [HttpGet]
        public bool CheckDbConnection()
        {
            dbEntities db = new dbEntities();
            IDbFunctionRepository _repository = new DbFunctionRepository(db);
            bool ret = _repository.CheckDbConnection();
            return ret;
        }

        public TSYSPAR GetSysParByPrimaryKey(TSYSPAR syspar)
        {
            dbEntities db = new dbEntities();
            ISysParRepository _repository = new SysParRepository(db);
            TSYSPAR ret = _repository.GetByPrimaryKey(syspar.CODSYSPAR, syspar.NUMSYSPARIDX);
            return ret;
        }
    }
}
