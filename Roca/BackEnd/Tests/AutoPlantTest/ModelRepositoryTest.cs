using System;
using System.Linq;
using Cno.Roca.BackEnd.AutoPlant.BL;
using Cno.Roca.BackEnd.AutoPlant.Data;
using Cno.Roca.CoreData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.AutoPlant
{
    [TestClass]
    public class ModelRepositoryTest
    {
        [TestMethod]
        public void GetMaterialsByLine()
        {
            string projId = "0007";
            string areaId = "0000000149";
            var repo = new ModelRepository();
            var materials = repo.GetMaterials(projId, areaId, MaterialOptionalFields.Line | MaterialOptionalFields.Service,  MaterialPipingOrder.Line).ToList();

            Assert.AreEqual(450, materials.Count, "Cantidad de materiales incorrecta");
            Console.Out.WriteLine("Primer Material");
            Console.Out.WriteLine(materials[0].ToString());
        }

        [TestMethod]
        public void GetProjects()
        {

            var repo = new ModelRepository();
            var projects = repo.GetProjects();

            var project = projects.First(p => p.Id == "0007");


            Assert.AreEqual("Prueba", project.Name, "Nombre de proyecto incorrecto");

            foreach (var proj in projects)
            {
                Console.WriteLine(proj.ToString());
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void EnumTest()
        {
            MaterialOptionalFields opt = MaterialOptionalFields.Line | MaterialOptionalFields.Service;
            var allFlags = (MaterialOptionalFields)Enum.GetValues(typeof(MaterialOptionalFields))
                             .Cast<int>().Aggregate((acc, next) => acc | next);
            var str = opt.GetStrings().ToList();
            Console.WriteLine(allFlags);
        }

    }
}
