using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.AutoPlant.Data;
using Cno.Roca.CoreData;
using Oracle.DataAccess.Client;

namespace Cno.Roca.BackEnd.AutoPlant.BL
{
    public class MaterialPipingDal
    {
        private OracleConnection _connection;
        protected Dictionary<string, string> MapeoDb;
            public MaterialPipingDal()
            {
                MapeoDb = new Dictionary<string, string>()
						{
							{ "Id" , "PARTNO"},
							{ "Service" , "SERVICE"},
							{ "Line" , "LINE"},
                            { "Tag" , "TAG"},
							{ "NominalDiam" , "DiamNom"},
							{ "Class" , "CLASS"},
                            { "ShortDescription" , "SHORT_DESC"},
							{ "LongDescription" , "LONG_DESCR"},
							{ "Material" , "MATERIAL"},
                            { "Rating" , "RATING"},
							{ "Schedule" , "SCHEDULE"},
							{ "PaintCode" , "PAINT_CODE"},
                            { "Insulation" , "INSULATION"},
							{ "PieceMark" , "PIECE_MARK"},
                            { "Spool" , "SPOOL"},
							{ "Quantity" , "Quantity"},
                            { "TotalQuantity" , "TotalQuantity"}
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


            public IList<MaterialPiping> GetMaterials(string projId, string areaId, MaterialOptionalFields optFields, MaterialPipingOrder order)
            {
                string optCols = GetOptionalColumns(optFields);
                string orderStr = GetOrderString(order, optFields);

                string sql = string.Format(@" select rownum PartNo, x.* from
                                    (select {0} ROCA.GETCOMPONENTSIZE(P.COMP_ID, P.PROJ_ID) DiamNom, P.SPEC Class,
                                        P.SHORT_DESC , P.LONG_DESCR, P.MATERIAL, P.RATING, P.SCHEDULE, P.PAINT_CODE, P.PIECE_MARK, P.SPOOL,
                                        round(SUM(roca.GetCantidad(P.CUT_LENGTH, P.QTY_PERSET) * (-1*(P.EXISTING - 1)) ), 2)  Quantity, 
                                        round(SUM(roca.GetCantidad(P.CUT_LENGTH, P.QTY_PERSET)), 2)  TotalQuantity                                   
                                     from V_PIPING_FULL p
                                     where (P.PROJ_ID = :p_PROJ_ID)  and (:p_AREAID = '*'  OR  p.AREAID = :p_AREAID)
 
                                 group by {0} ROCA.GETCOMPONENTSIZE(P.COMP_ID, P.PROJ_ID), P.SPEC,
                                        P.SHORT_DESC, P.LONG_DESCR, P.MATERIAL, P.RATING, P.SCHEDULE, P.PAINT_CODE, P.PIECE_MARK, P.SPOOL, P.SORT_CODE
                                 order by {1}) x", optCols, orderStr);


                var cmd = new OracleCommand(sql);
                cmd.BindByName = true;

                cmd.Parameters.Add("p_PROJ_ID", OracleDbType.Char).Value = projId;
                cmd.Parameters.Add("p_AREAID", OracleDbType.Char).Value = areaId;

                OracleDataReader dataReader = null;
                OpenConnection();
                try
                {
                    cmd.Connection = _connection;
                    dataReader = cmd.ExecuteReader();
                    var entities = new List<MaterialPiping>();
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

                    CloseConnection(_connection);
                }
            }

        private string GetOrderString(MaterialPipingOrder order, MaterialOptionalFields optFields)
        {
            var orderStr = ", P.Line, P.Service, P.SORT_CODE, P.PIECE_MARK,";
            if (order == MaterialPipingOrder.SortOrder)
                orderStr = ", P.PIECE_MARK, P.SORT_CODE, P.Line, P.Service,";
            if(!optFields.HasFlag(MaterialOptionalFields.Line))
                orderStr = orderStr.Remove(orderStr.IndexOf(", P.Line"), ", P.Line".Length);
            if (!optFields.HasFlag(MaterialOptionalFields.Service))
                orderStr = orderStr.Remove(orderStr.IndexOf(", P.Service"), ", P.Service".Length);
            return orderStr.Trim(',');
        }

        private string GetOptionalColumns(MaterialOptionalFields optFields)
        {
            var optFieldsStr = optFields.GetStrings();
            var cols = string.Join(", ", optFieldsStr.Select(f => "P." + MapeoDb[f]));
            if (cols.Length > 0)
                cols = cols + ", ";
            return cols;
        }
            

            protected MaterialPiping BuildEntity(OracleDataReader entityReader)
            {
                var entity = new MaterialPiping();

                int i;


                i = entityReader.GetOrdinal("PARTNO");
                if (!entityReader.IsDBNull(i))
                    entity.Id = (int)entityReader.GetDecimal(i);

                if (HasColumn(entityReader, "SERVICE"))
                {
                    i = entityReader.GetOrdinal("SERVICE");
                    if (!entityReader.IsDBNull(i))
                        entity.Service = entityReader.GetString(i).TrimEnd();
                }

                if (HasColumn(entityReader, "LINE"))
                {
                    i = entityReader.GetOrdinal("LINE");
                    if (!entityReader.IsDBNull(i))
                        entity.Line = entityReader.GetString(i).TrimEnd();
                }

                if (HasColumn(entityReader, "TAG"))
                {
                    i = entityReader.GetOrdinal("TAG");
                    if (!entityReader.IsDBNull(i))
                        entity.Tag = entityReader.GetString(i).TrimEnd();
                }

                if (HasColumn(entityReader, "INSULATION"))
                {
                    i = entityReader.GetOrdinal("INSULATION");
                    if (!entityReader.IsDBNull(i))
                        entity.Insulation = entityReader.GetString(i).TrimEnd();
                }


                i = entityReader.GetOrdinal("DiamNom");
                if (!entityReader.IsDBNull(i))
                    entity.NominalDiam = entityReader.GetString(i).TrimEnd();

                i = entityReader.GetOrdinal("CLASS");
                if (!entityReader.IsDBNull(i))
                    entity.Class = entityReader.GetString(i).TrimEnd();

                i = entityReader.GetOrdinal("SHORT_DESC");
                if (!entityReader.IsDBNull(i))
                    entity.ShortDescription = entityReader.GetString(i).TrimEnd();

                i = entityReader.GetOrdinal("LONG_DESCR");
                if (!entityReader.IsDBNull(i))
                    entity.LongDescription = entityReader.GetString(i).TrimEnd();

                i = entityReader.GetOrdinal("MATERIAL");
                if (!entityReader.IsDBNull(i))
                    entity.Material = entityReader.GetString(i).TrimEnd();

                i = entityReader.GetOrdinal("RATING");
                if (!entityReader.IsDBNull(i))
                    entity.Rating = entityReader.GetString(i).TrimEnd();

                i = entityReader.GetOrdinal("SCHEDULE");
                if (!entityReader.IsDBNull(i))
                    entity.Schedule = entityReader.GetString(i).TrimEnd();

                i = entityReader.GetOrdinal("PAINT_CODE");
                if (!entityReader.IsDBNull(i))
                    entity.PaintCode = entityReader.GetString(i).TrimEnd();


                i = entityReader.GetOrdinal("PIECE_MARK");
                if (!entityReader.IsDBNull(i))
                    entity.PieceMark = entityReader.GetString(i).TrimEnd();

                i = entityReader.GetOrdinal("SPOOL");
                if (!entityReader.IsDBNull(i))
                    entity.Spool = entityReader.GetString(i).TrimEnd();

                i = entityReader.GetOrdinal("Quantity");
                if (!entityReader.IsDBNull(i))
                    entity.Quantity = (double)entityReader.GetDecimal(i);

                i = entityReader.GetOrdinal("TotalQuantity");
                if (!entityReader.IsDBNull(i))
                    entity.TotalQuantity = (double)entityReader.GetDecimal(i);
                


                return entity;

            }

            protected bool HasColumn(IDataRecord dr, string columnName)
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                }
                return false;
            }



    }
}
