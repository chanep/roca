using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using Cno.Roca.Core.Data;

namespace Cno.Roca.BackEnd.AutoPlant.BL
{
    public class DbConnManagerFactory
    {
        private static DbConnectionManager _dbConnManager;

        public static DbConnectionManager GetDbConnectionManager()
        {
            if (_dbConnManager != null)
                return _dbConnManager;
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Autoplant"].ConnectionString;
                _dbConnManager = new DbConnectionManager(connectionString);
                return _dbConnManager;
            }
        }
    }
}
