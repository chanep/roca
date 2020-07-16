using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Microsoft.Office.Interop.Excel;

namespace Cno.Roca.BackEnd.ExcelTools
{
    public class ExcelCreator
    {
        public static void CreateMaterialListBook(MaterialList ml, string templateFileName, string outputFileName)
        {
            var replaceDict = new Dictionary<string, string>();
            replaceDict.Add("<Title>", ml.Title);
            replaceDict.Add("<DocNumber>", ml.DocNumber);
            replaceDict.Add("<Revision>", ml.Revision);
            replaceDict.Add("<Date>", ml.CreatedOn.ToShortDateString());
            //replaceDict.Add("<CreatorInitials>", ml.Purpose);
            replaceDict.Add("<CreatorInitials>", ml.Creator.Initials);
            replaceDict.Add("<RevisorInitials>", ml.Revisor.Initials);
            replaceDict.Add("<AproverInitials>", ml.Approver.Initials);

            Application xlApp = null;
            try
            {
                xlApp = new Application();

                xlApp.DefaultFilePath = Environment.CurrentDirectory;
                xlApp.Visible = false;

                var wb = xlApp.Workbooks.Open(templateFileName);

                var s = (Worksheet) wb.Worksheets[1];

                foreach (var element in replaceDict)
                {
                    var range = (s.UsedRange).Find(element.Key);
                    if (range == null)
                        throw new RocaException("No se encuentra en el documento template la clave " + element.Key);
                   //do
                    //{
                    //    if (range == null)
                    //        throw new RocaException("No se encuentra en el documento template la clave " + element.Key);
                    //    s.Cells[range.Row, range.Column] = element.Value;
                    //    range = (s.UsedRange).Find(element.Key);
                    //} while (range != null);
                    
                    
                    while (range != null)
                    {
                        s.Cells[range.Row, range.Column] = element.Value;
                        range = (s.UsedRange).Find(element.Key);      
                    }
                
                
                }

                //throw new Exception("prueba");

                wb.SaveAs(outputFileName);
                wb.Close();
                xlApp.Quit();
            }
            finally
            {
                Marshal.ReleaseComObject(xlApp);
            }
        }

    }
}
