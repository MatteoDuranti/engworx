using Domain;
using Repository;
using RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class MenuController : ApiController
    {
        public IList<VMEN> GetMenuByUser(TUSR user)
        {
            dbEntities db = new dbEntities();
            IMenuRepository _repository = new MenuRepository(db);
            IList<VMEN> ret = _repository.GetMenuByUser(user);
            return ret;
        }
    }
}
