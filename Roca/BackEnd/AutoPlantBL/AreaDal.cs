using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Cno.Roca.BackEnd.AutoPlant.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Cno.Roca.BackEnd.AutoPlant.BL
{
    public class AreaDal
    {
        private OracleConnection _connection;
        protected Dictionary<string, string> MapeoDb;
        public AreaDal()
        {
            MapeoDb = new Dictionary<string, string>()
						{
							{ "AreaId", "ID"},
							{ "ProjectId", "PROJ_ID"},
							{ "Name", "NAME"}
						};
        }

        protected void OpenConnection()
        {
            _connection = DbConnectionManager.GetAutoplantConnection();
            _connection.Open();
        }

        protected void CloseConnection(OracleConnection connection)
        {
            if (connection != null)
                connection.Close();
        }

        public Area Get(AreaPk id)
        {
            OpenConnection();
            OracleDataReader dataReader = null;
            OracleCommand command = null;
            try
            {
                command = BuildGetCommand(id);
                command.Connection = _connection;
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    return BuildEntity(dataReader);
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                if ((dataReader != null) && !dataReader.IsClosed)
                {
                    dataReader.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                CloseConnection(_connection);
            }
        }

        public IList<Area> GetAll()
        {
            OpenConnection();
            OracleCommand command = null;
            OracleDataReader dataReader = null;
            try
            {
                command = BuildGetAllCommand();
                command.Connection = _connection;
                dataReader = command.ExecuteReader();
                var entities = new List<Area>();
                while (dataReader.Read())
                {
                    entities.Add(BuildEntity(dataReader));
                }
                return entities;
            }
            finally
            {
                if ((dataReader != null) && !dataReader.IsClosed)
                {
                    dataReader.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                CloseConnection(_connection);
            }
        }

        protected OracleCommand BuildGetCommand(AreaPk id)
        {
            string sql = @"SELECT * FROM AREA3D WHERE ID = :p_ID AND PROJ_ID = :p_PROJ_ID";
            var cmd = new OracleCommand(sql);
            cmd.BindByName = true;

            cmd.Parameters.Add("p_" + "ID", OracleDbType.Char).Value = id.AreaId;
            cmd.Parameters.Add("p_" + "PROJ_ID", OracleDbType.Char).Value = id.ProjectId;

            return cmd;

        }

        protected OracleCommand BuildGetAllCommand()
        {
            string sql = @"SELECT * FROM AREA3D";
            var cmd = new OracleCommand(sql);
            cmd.BindByName = true;
            return cmd;
        }

        protected Area BuildEntity(OracleDataReader dataReader)
        {
            var entity = new Area();

            int i;

            i = dataReader.GetOrdinal("ID");
            if (!dataReader.IsDBNull(i))
                entity.AreaId = dataReader.GetString(i).TrimEnd();

            i = dataReader.GetOrdinal("PROJ_ID");
            if (!dataReader.IsDBNull(i))
                entity.ProjectId = dataReader.GetString(i).TrimEnd();

            i = dataReader.GetOrdinal("NAME");
            if (!dataReader.IsDBNull(i))
                entity.Name = dataReader.GetString(i);


            return entity;

        }


    }
}
