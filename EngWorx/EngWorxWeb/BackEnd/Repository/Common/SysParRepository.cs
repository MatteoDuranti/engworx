using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Domain;
using RepositoryInterface;

namespace Repository
{
    public class SysParRepository : Repository<TSYSPAR>, ISysParRepository
    {

        public SysParRepository(dbEntities db)
            : base(db)
        {
        }
    }
}