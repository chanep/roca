using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.AutoPlant.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Cno.Roca.BackEnd.AutoPlant.BL
{
    public class MatPipingMigrationDal
    {
        private OracleConnection _sourceConnection;
        private OracleConnection _destConnection;

        public MatPipingMigrationDal()
        {
            
        }

        protected void OpenSourceConnection()
        {
            _sourceConnection = DbConnectionManager.GetAutoplantConnection();
            _sourceConnection.Open();
        }

        protected void OpenDestConnection()
        {
            _destConnection = DbConnectionManager.GetRocaConnection();
            _destConnection.Open();
        }

        protected void CloseConnection(OracleConnection connection)
        {
            if(connection != null)
                connection.Close();
        }

        public MatPipingMigration CreateDestMaterial(MatPipingMigration entity)
		{
			OracleCommand command = null;
			OpenDestConnection();
			try
			{
				command = BuildCreateCommand(entity);
				command.Connection = _destConnection;
				command.ExecuteNonQuery();
				if (command.Parameters.Contains("p_id") && (command.Parameters["p_id"].Direction == ParameterDirection.Output))
				{
                    entity.Id = ((OracleDecimal)command.Parameters["p_id"].Value).ToInt32(); ;
				}
				return entity;
			}
			finally
			{
				if (command != null)
				{
					command.Dispose();
				}
				CloseConnection(_destConnection);
			}
		}

        public int DeleteAllDestMaterial(string projectId)
        {
            OracleCommand command = null;
            try
            {
                OpenDestConnection();
                command = BuildDeleteCommand(projectId);
                command.Connection = _destConnection;
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                CloseConnection(_destConnection);
            }
        }

        protected OracleCommand BuildDeleteCommand(string projectId)
        {
            string sql = @"DELETE ROCA.MAT_PIPING_AP 
                            WHERE PROJ_ID = :p_PROJ_ID";
            var cmd = new OracleCommand(sql);
            cmd.BindByName = true;
            cmd.Parameters.Add(":p_PROJ_ID", OracleDbType.Char).Value = projectId;

            return cmd;
        }

        
        protected OracleCommand BuildCreateCommand(MatPipingMigration entity)
        {
            string sql = @"INSERT INTO ROCA.MAT_PIPING_AP (
						  ID,
                          PROJ_ID,
                          AREAID,
                          AREA,
                          LINE,
                          SERVICE,
                          TAG,
                          PIECE_MARK,
                          QUANTITY,
                          TOTALQUANTITY) 
						VALUES (
						MAT_PIPING_AP_SEQ.nextval,
						:p_PROJ_ID,
                        :p_AREAID,
                        :p_AREA,
                        :p_LINE,
                        :p_SERVICE,
                        :p_TAG,
                        :p_PIECE_MARK,
                        :p_QUANTITY,
                        :p_TOTALQUANTITY)
                        RETURNING ID INTO :p_id";
            var cmd = new OracleCommand(sql);
			cmd.BindByName = true;
			
            cmd.Parameters.Add("p_id", OracleDbType.Decimal).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("p_PROJ_ID", OracleDbType.Char).Value = entity.ProjectId;
            cmd.Parameters.Add("p_AREAID", OracleDbType.Char).Value = entity.AreaId;
            cmd.Parameters.Add("p_AREA", OracleDbType.Char).Value = entity.Area;
            cmd.Parameters.Add("p_LINE", OracleDbType.Char).Value = entity.Line;
            cmd.Parameters.Add("p_SERVICE", OracleDbType.Char).Value = entity.Service;
            cmd.Parameters.Add("p_TAG", OracleDbType.Char).Value = entity.Tag;
            cmd.Parameters.Add("p_PIECE_MARK", OracleDbType.Char).Value = entity.PieceMark;
            cmd.Parameters.Add("p_QUANTITY", OracleDbType.Decimal).Value = entity.Quantity;
            cmd.Parameters.Add("p_TOTALQUANTITY", OracleDbType.Decimal).Value = entity.TotalQuantity;
			
            return cmd;
        }

        public IList<MatPipingMigration> GetSourceMaterials(string projectId, int skip, int take)
        {
            string sql = string.Format(@"select * from
                                        (select rownum rn, x.* from
                                            (select *                                 
                                            from V_PIPING_FOREXPORT
                                            where PROJ_ID = :p_PROJ_ID) x) y
                                        where (:p_TAKE = 0) OR (rn > :p_SKIP AND rn <= (:p_SKIP + :p_TAKE))");

            var cmd = new OracleCommand(sql);
            cmd.BindByName = true;

            cmd.Parameters.Add("p_PROJ_ID", OracleDbType.Char).Value = projectId;
            cmd.Parameters.Add("p_SKIP", OracleDbType.Decimal).Value = skip;
            cmd.Parameters.Add("p_TAKE", OracleDbType.Decimal).Value = take;

            OracleDataReader dataReader = null;
            OpenSourceConnection();
            try
            {
                cmd.Connection = _sourceConnection;
                dataReader = cmd.ExecuteReader();
                var entities = new List<MatPipingMigration>();
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
                cmd.Dispose();

                CloseConnection(_sourceConnection);
            }
        }

        public IList<MatPipingMigration> GetSourceMaterials(string projectId)
        {
            return GetSourceMaterials(projectId, 0, 0);
        }

        protected MatPipingMigration BuildEntity(OracleDataReader entityReader)
        {
            var entity = new MatPipingMigration();

            int i;

            i = entityReader.GetOrdinal("PROJ_ID");
            if (!entityReader.IsDBNull(i))
                entity.ProjectId = entityReader.GetString(i).TrimEnd();

            i = entityReader.GetOrdinal("AREAID");
            if (!entityReader.IsDBNull(i))
                entity.AreaId = entityReader.GetString(i).TrimEnd();

            i = entityReader.GetOrdinal("AREA");
            if (!entityReader.IsDBNull(i))
                entity.Area = entityReader.GetString(i).TrimEnd();

            i = entityReader.GetOrdinal("SERVICE");
            if (!entityReader.IsDBNull(i))
                entity.Service = entityReader.GetString(i).TrimEnd();

            i = entityReader.GetOrdinal("LINE");
            if (!entityReader.IsDBNull(i))
                entity.Line = entityReader.GetString(i).TrimEnd();


            i = entityReader.GetOrdinal("TAG");
            if (!entityReader.IsDBNull(i))
                entity.Tag = entityReader.GetString(i).TrimEnd();

            i = entityReader.GetOrdinal("PIECE_MARK");
            if (!entityReader.IsDBNull(i))
                entity.PieceMark = entityReader.GetString(i).TrimEnd();

            i = entityReader.GetOrdinal("Quantity");
            if (!entityReader.IsDBNull(i))
                entity.Quantity = (double)entityReader.GetDecimal(i);

            i = entityReader.GetOrdinal("TotalQuantity");
            if (!entityReader.IsDBNull(i))
                entity.TotalQuantity = (double)entityReader.GetDecimal(i);



            return entity;
        }
    }
}
