using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Cno.Roca.BackEnd.AutoPlant.Data;
using Cno.Roca.Core.Entity;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Cno.Roca.BackEnd.AutoPlant.BL
{
    public class AreaDal : DalBasic<AreaPk, Area>
    {
        public AreaDal()
            : base(DbConnManagerFactory.GetDbConnectionManager())
        {
            MapeoDb = new Dictionary<string, string>()
						{
							{ "AreaId", "ID"},
							{ "ProjectId", "PROJ_ID"},
							{ "Name", "NAME"}
						};
        }

        protected override OracleCommand BuildGetCommand(AreaPk id)
        {
            string sql = @"SELECT * FROM AREA3D WHERE ID = :p_ID AND PROJ_ID = :p_PROJ_ID";
            var cmd = new OracleCommand(sql);
            cmd.BindByName = true;

            cmd.Parameters.Add("p_" + "ID", OracleDbType.Char).Value = id.AreaId;
            cmd.Parameters.Add("p_" + "PROJ_ID", OracleDbType.Char).Value = id.ProjectId;

            return cmd;

        }

        protected override OracleCommand BuildGetAllCommand()
        {
            string sql = @"SELECT * FROM AREA3D";
            var cmd = new OracleCommand(sql);
            cmd.BindByName = true;
            return cmd;
        }

        protected override OracleCommand BuildCreateCommand(Area entity)
        {
            throw new NotImplementedException();
        }

        protected override OracleCommand BuildUpdateCommand(Area entity)
        {
            throw new NotImplementedException();
        }

        protected override OracleCommand BuildDeleteCommand(Area entity)
        {
            throw new NotImplementedException();
        }

        protected override Area BuildEntity(OracleDataReader dataReader)
        {
            Area entity = GetEntityInstance();

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
