using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Cno.Roca.BackEnd.AutoPlant.BL
{
    public class DbConnectionManager
    {
        public static OracleConnection GetAutoplantConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["Autoplant"].ConnectionString;
            return new OracleConnection(connString);
        }

        public static OracleConnection GetRocaConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["Roca"].ConnectionString;
            return new OracleConnection(connString);
        }
    }
}
