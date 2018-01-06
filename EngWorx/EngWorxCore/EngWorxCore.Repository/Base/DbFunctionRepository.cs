using System;
using log4net;
using System.Data.Common;
using System.Data.Entity;
using RepositoryInterface;

namespace Repository
{
    public class DbFunctionRepository : IDbFunctionRepository
    {
        protected static ILog log = LogManager.GetLogger((System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));
        private DbContext db = null;

        public DbFunctionRepository(DbContext database)
        {
            this.db = database;
        }

        public bool CheckDbConnection()
        {
            bool result = true;
            try
            {
                DbConnection conn = db.Database.Connection;
                conn.Open();
                conn.Close();
                result = true;
            }
            catch (Exception ex)
            {
                log.Error("ERROR: " + ex.Message, ex);
                result = false;
            }
            return result;
        }

        public string GetConnectionString()
        {
            return db.Database.Connection.ConnectionString;
        }
    }
}