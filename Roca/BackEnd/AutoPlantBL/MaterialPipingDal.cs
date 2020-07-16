using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.AutoPlant.Data;
using Cno.Roca.Core.Data;
using Cno.Roca.Core.Entity;
using Cno.Roca.CoreData;
using Oracle.DataAccess.Client;

namespace Cno.Roca.BackEnd.AutoPlant.BL
{
    public class MaterialPipingDal : DalBasic<int, MaterialPiping>
    {
            public MaterialPipingDal()
                : base(DbConnManagerFactory.GetDbConnectionManager())
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
							{ "Quantity" , "Quantity"},
                            { "TotalQuantity" , "TotalQuantity"}
						};
            }


            protected override OracleCommand BuildGetCommand(int id)
            {
                throw new NotImplementedException();

            }

            protected override OracleCommand BuildGetAllCommand()
            {
                throw new NotImplementedException();
            }

            protected override OracleCommand BuildCreateCommand(MaterialPiping entity)
            {
                throw new NotImplementedException();
            }

            protected override OracleCommand BuildUpdateCommand(MaterialPiping entity)
            {
                throw new NotImplementedException();
            }

            protected override OracleCommand BuildDeleteCommand(MaterialPiping entity)
            {
                throw new NotImplementedException();
            }

            public IList<MaterialPiping> GetMaterials(string projId, string areaId, MaterialOptionalFields optFields, MaterialPipingOrder order)
            {
                string optCols = GetOptionalColumns(optFields);
                string orderStr = GetOrderString(order, optFields);

                string sql = string.Format(@" select rownum PartNo, x.* from
                                    (select {0} ROCA.GETCOMPONENTSIZE(P.COMP_ID, P.PROJ_ID) DiamNom, P.SPEC Class,
                                        P.INSULATION , P.SHORT_DESC , P.LONG_DESCR, P.MATERIAL, P.RATING, P.SCHEDULE, P.PAINT_CODE, P.PIECE_MARK,
                                        round(SUM(roca.GetCantidad(P.CUT_LENGTH, P.QTY_PERSET) * (-1*(P.EXISTING - 1)) ), 2)  Quantity, 
                                        round(SUM(roca.GetCantidad(P.CUT_LENGTH, P.QTY_PERSET)), 2)  TotalQuantity                                   
                                     from V_PIPING_FULL p
                                     where (P.PROJ_ID = :p_PROJ_ID)  and (:p_AREAID = '*'  OR  p.AREAID = :p_AREAID)
 
                                 group by {0} ROCA.GETCOMPONENTSIZE(P.COMP_ID, P.PROJ_ID), P.SPEC,
                                        P.INSULATION, P.SHORT_DESC, P.LONG_DESCR, P.MATERIAL, P.RATING, P.SCHEDULE, P.PAINT_CODE, P.PIECE_MARK,P.SORT_CODE
                                 order by {1}) x", optCols, orderStr);


                var cmd = new OracleCommand(sql);
                cmd.BindByName = true;

                cmd.Parameters.Add("p_PROJ_ID", OracleDbType.Char).Value = projId;
                cmd.Parameters.Add("p_AREAID", OracleDbType.Char).Value = areaId;

                return GetAll(cmd);
            }

        private string GetOrderString(MaterialPipingOrder order, MaterialOptionalFields optFields)
        {
            var orderStr = ", P.Line, P.Service, P.SORT_CODE, P.PIECE_MARK,";
            if (order == MaterialPipingOrder.SortOrder)
                orderStr = ", P.SORT_CODE, P.PIECE_MARK, P.Line, P.Service,";
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
            

            protected override MaterialPiping BuildEntity(OracleDataReader entityReader)
            {
                MaterialPiping entity = GetEntityInstance();

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

                i = entityReader.GetOrdinal("INSULATION");
                if (!entityReader.IsDBNull(i))
                    entity.Insulation = entityReader.GetString(i).TrimEnd();

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
