using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using OfficeOpenXml;

namespace Cno.Roca.BackEnd.ExcelTools
{
    public class SisepcExport
    {
        public void Test(string input, string output)
        {
            var inputFile = new FileInfo(input);
            var outputFile = new FileInfo(output);
            using (var pck = new ExcelPackage(inputFile))
            {
                var hoja = pck.Workbook.Worksheets[1];
                hoja.Cells["A2"].Value = "Hola";
                pck.SaveAs(outputFile);
            }
        }

        public Stream GenerateCatalog(string templatePath, IEnumerable<SisepcElement> elements )
        {
            var stream = new MemoryStream();
            var file = new FileInfo(templatePath);
            using (var pck = new ExcelPackage(file))
            {
                var hoja = pck.Workbook.Worksheets[1];

                int i = 2;
                foreach (var e in elements)
                {
                    hoja.Cells["A" + i].Value = e.Code;

                    hoja.Cells["B" + i].Value = e.Dim1;
                    hoja.Cells["C" + i].Value = e.Unit1;
                    hoja.Cells["D" + i].Value = e.Dim2;
                    hoja.Cells["E" + i].Value = e.Unit2;
                    hoja.Cells["F" + i].Value = e.Dim3;
                    hoja.Cells["G" + i].Value = e.Unit3;
                    hoja.Cells["H" + i].Value = e.Weight;

                    hoja.Cells["K" + i].Value = e.ShortDescription;
                    hoja.Cells["L" + i].Value = e.Description;

                    hoja.Cells["M" + i].Value = e.PurchaseUnit;

                    i++;
                }

                pck.SaveAs(stream);
                stream.Position = 0;
                return stream;
            }
        }

    }
}
