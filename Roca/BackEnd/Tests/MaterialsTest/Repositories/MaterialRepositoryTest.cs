using System;
using System.Linq;
using System.Transactions;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials.Repositories
{
    [TestClass]
    public class MaterialRepositoryTest : BaseTest
    {


        [TestMethod]
        public void AddPMaterialTest()
        {
            using (var tx = new TransactionScope())
            {
                var mat = CreatePMaterial();

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    matRepo.Add(mat);
                    roca.Commit();
                    Assert.AreNotEqual(0, mat.Id, "No se genero correctamente el Id del PMaterial");
                }

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    var mat2 = matRepo.Get(mat.Id);
                    Assert.IsNotNull(mat2, "Fallo Get por Id del PMaterial");
                    Assert.AreEqual(mat, mat2, "Fallo el Get por Id del PMaterial, no se obtuvo el mismo Pmaterial que se agrego");
                    Console.WriteLine(mat2);
                }

                //tx.Complete();


            }
        }

        [TestMethod]
        public void GetOfTypeTest()
        {
            using (var tx = new TransactionScope())
            {
                var mat1 = CreatePMaterial();
                var mat2 = CreateEiMaterial();
                mat1.Description = "Mongo01";
                mat2.Description = mat1.Description;

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    matRepo.Add(mat2);
                    matRepo.Add(mat1);                   
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    var pMats = matRepo.GetAll<PMaterial>().Where(m => m.Description == mat1.Description).ToList();
                    Assert.AreEqual(1, pMats.Count, "Get por typo no devolvio 1 elemneto");
                    Assert.IsInstanceOfType(pMats[0], typeof (PMaterial),
                        "Get por type no devolvio el material del tipo correcto");
                }

                //tx.Complete();


            }
        }

        [TestMethod]
        public void SelectLikeTest()
        {
            using (var tx = new TransactionScope())
            {
                var mat = Helper.CreateEiMaterial();
                string subStr = "mongo5639";
                string subStr2 = "Chongo";
                mat.Description = subStr2 + subStr;

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    matRepo.Add(mat);
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    var mats2 = matRepo.GetAll<EiMaterial>().Where(m => m.Description.Contains(subStr)).ToList();
                    Assert.AreEqual(1, mats2.Count, "El get con matcheo de substring (like) no funciono");
                    Assert.AreEqual(mat.Description, mats2[0].Description, "El get con matcheo de substring (like) no funciono");

                    var mats3 = matRepo.GetAll<EiMaterial>().Where(m => m.Description.ToLower().Contains(subStr2.ToLower())).ToList();
                    Assert.AreEqual(1, mats3.Count, "El get con matcheo case insensitive de substring (like) no funciono");
                    Assert.AreEqual(mat.Description, mats3[0].Description, "El get con matcheo case insensitive de substring (like) no funciono");
                }

                //tx.Complete();


            }
        }

        [TestMethod]
        public void DeletMaterialTest()
        {
            using (var tx = new TransactionScope())
            {
                var mat = CreatePMaterial();

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    matRepo.Add(mat);
                    roca.Commit();
                    Assert.AreNotEqual(0, mat.Id, "No se genero correctamente el Id del PMaterial");
                }

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    var mat2 = matRepo.Get(mat.Id);
                    Assert.IsNotNull(mat2, "Fallo Get por Id del PMaterial");
                    matRepo.Delete(mat2);
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    var mat2 = matRepo.Get(mat.Id);
                    Assert.IsNull(mat2, "Fallo Delete, no borro el material");
                }

                //tx.Complete();


            }
        }

        [TestMethod]
        public void DeletMaterialByIdTest()
        {
            using (var tx = new TransactionScope())
            {
                var mat = CreatePMaterial();

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    matRepo.Add(mat);
                    roca.Commit();
                    Assert.AreNotEqual(0, mat.Id, "No se genero correctamente el Id del PMaterial");
                }

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    matRepo.Delete(mat.Id);
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    var mat2 = matRepo.Get(mat.Id);
                    Assert.IsNull(mat2, "Fallo Delete, no borro el material");
                }

                //tx.Complete();


            }
        }

        [TestMethod]
        public void UpdateMaterialTest()
        {
            using (var tx = new TransactionScope())
            {
                var mat = CreateEiMaterial();

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    matRepo.Add(mat);
                    roca.Commit();
                    Assert.AreNotEqual(0, mat.Id, "No se genero correctamente el Id del Material");
                }

                string newDesc = "Mongo27";
                string newPower = "MongoWatts";

                using (var roca = CreateRocaUow())
                {
                    mat.Description = newDesc;
                    mat.Power = newPower;
                    var matRepo = roca.Materials;
                    matRepo.Update(mat);
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    var mat2 = (EiMaterial)matRepo.Get(mat.Id);
                    Assert.AreEqual(newDesc, mat2.Description, "No se acutlizo el Material correctamente");
                    Assert.AreEqual(newPower, mat2.Power, "No se acutlizo el Material correctamente");
                }

                //tx.Complete();


            }
        }

        [TestMethod]
        public void AddEiMaterialTest()
        {
            using (var tx = new TransactionScope())
            {
                var mat = Helper.CreateEiMaterial();

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    matRepo.Add(mat);
                    roca.Commit();
                    Assert.AreNotEqual(0, mat.Id, "No se genero correctamente el Id del EiMaterial");
                }

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    var mat2 = matRepo.GetFullEiMaterial(mat.Id);
                    Assert.IsNotNull(mat2, "Fallo Get por Id del PMaterial");
                    Assert.IsNotNull(mat2.Details);
                    Assert.AreEqual(mat.Id, mat.Details.Id);
                    Console.WriteLine(mat2);
                }

                //tx.Complete();


            }
        }

        [TestMethod]
        public void DeleteEiMaterialTest()
        {
            using (var tx = new TransactionScope())
            {
                var mat = Helper.CreateEiMaterial();
                var mat2 = Helper.CreateEiMaterial("2"); 
                mat2.Details = null; //EiMaterial sin Details

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    matRepo.Add(mat);
                    matRepo.Add(mat2);
                    roca.Commit();
                    Assert.AreNotEqual(0, mat.Id, "No se genero correctamente el Id del EiMaterial");
                    Assert.AreNotEqual(0, mat2.Id, "No se genero correctamente el Id del EiMaterial");
                }

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    var mat3 = matRepo.Get(mat.Id);
                    var mat4 = matRepo.Get(mat2.Id);
                    matRepo.Delete(mat3);
                    matRepo.Delete(mat4);
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var matRepo = roca.Materials;
                    var mat3 = matRepo.GetFullEiMaterial(mat.Id);
                    var mat4 = matRepo.GetFullEiMaterial(mat2.Id);
                    Assert.IsNull(mat3, "Fallo Get por Id del PMaterial");
                    Assert.IsNull(mat4, "Fallo Get por Id del PMaterial");

                }

                //tx.Complete();


            }
        }


        private PMaterial CreatePMaterial()
        {
            var mat = new PMaterial()
            {
                Description = "Caño",
                IdentCode = "P12345678",
                Lenght = 12.3

            };

            return mat;
        }

        private EiMaterial CreateEiMaterial()
        {
            var mat = new EiMaterial()
            {
                Description = "Termica",
                IdentCode = "E12345678",
                Power = "100W"

            };

            return mat;
        }

        
    }
}
