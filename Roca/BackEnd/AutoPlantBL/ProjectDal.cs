using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.AutoPlant.Data;
using Cno.Roca.Core.Data;
using Cno.Roca.Core.Entity;
using Oracle.DataAccess.Client;

namespace Cno.Roca.BackEnd.AutoPlant.BL
{
    public class ProjectDal : Dal<string, Project>
    {

        public ProjectDal() : base(DbConnManagerFactory.GetDbConnectionManager())
        {
            MapeoDb = new Dictionary<string, string>()
						{
							{ "Id" , "PROJ_ID"},
							{ "Item" , "ITEM"},
							{ "Description" , "DESCRIPT"},
                            { "Name" , "ProjectName"},
                            { "Number" , "ProjectNumber"}
						};
        }


        public override Project Get(string id)
        {
            throw new NotImplementedException();
        }

        public override IList<Project> GetAll()
        {
            OpenConnection();
            OracleCommand command = null;
            OracleDataReader dataReader = null;
            try
            {
                command = BuildGetAllCommand();
                command.Connection = _connection;
                LogCommand(command);
                dataReader = command.ExecuteReader();
                LogEndCommand(command);
                var entities = BuildEntities(dataReader);
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
                CloseConnection();
            }
        }

        public override Project Create(Project entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Project entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Project entity)
        {
            throw new NotImplementedException();
        }

        private OracleCommand BuildGetAllCommand()
        {
            string sql = string.Format(@" select p.{0}, p.{1}, dbms_lob.substr( p.{2},100,1) {2} from PROJECT p", "PROJ_ID", "ITEM",
                                       "DESCRIPT");


            var cmd = new OracleCommand(sql);
            cmd.BindByName = true;
            return cmd;
        }

        private IList<Project> BuildEntities(OracleDataReader entityReader)
        {
            var entities = new Dictionary<string, Project>();

            while (entityReader.Read())
            {
                int i;


                i = entityReader.GetOrdinal("PROJ_ID");
                Project entity = null;

                if (!entityReader.IsDBNull(i))
                {
                    string id = entityReader.GetString(i).TrimEnd();
                    if (!entities.ContainsKey(id))
                    {
                        entity = new Project();
                        entity.Id = id;
                        entities.Add(id, entity);
                    }
                    else
                    {
                        entity = entities[id];
                    }
                }

                i = entityReader.GetOrdinal("ITEM");
                if (!entityReader.IsDBNull(i))
                {
                    string property = entityReader.GetString(i).TrimEnd();
                    if (property == "ProjectName")
                    {
                        i = entityReader.GetOrdinal("DESCRIPT");
                        if (!entityReader.IsDBNull(i))
                        {
                            entity.Name = entityReader.GetString(i).TrimEnd();
                        }
                        
                    }
                        
                }



            }

            return entities.Values.ToList();
        }
    }
}
