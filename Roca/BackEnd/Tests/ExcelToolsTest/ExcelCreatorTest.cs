using System;
using Cno.Roca.BackEnd.ExcelTools;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.ExcelToolsTest
{
    [TestClass]
    public class ExcelCreatorTest
    {
        private MaterialList CreateMaterialList()
        {
            var creator = new User()
            {
                Initials = "PDR",
                Name = "Pablo",
                LastName = "Rossi",
                LongUserName = @"ODEBRECHT\prossi"
            };


            var ml = new MaterialList()
            {
                Title = "Titulo de la lista",
                DocNumber = "I-GIO-XXXX-YYY-ZZ",
                Revision = "A",
                CreatedOn = new DateTime(2014,01,28), 
                Creator = creator,
                Revisor = creator,
                Approver = creator
            };

            return ml;
        }

        [TestMethod]
        public void CreateMaterialListBookTest()
        {
            string template = @"TemplateLm.xlsx";
            string output = "mongo.xlsx";
            var ml = CreateMaterialList();
            ExcelCreator.CreateMaterialListBook(ml, template, output);
        }
    }
}
