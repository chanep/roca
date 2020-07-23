using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cno.Roca.BackEnd.ExcelTools;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.ExcelToolsTest
{
    [TestClass]
    public class SisepcExportTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var sisepc = new SisepcExport();
            var stream = (MemoryStream)sisepc.GenerateCatalog("Sisepc.xlsx", new List<SisepcElement>());
            FileStream file = new FileStream("file.xlsx", FileMode.Create, FileAccess.Write);
            stream.WriteTo(file);
            file.Close();
            stream.Close();
        }
    }
}
