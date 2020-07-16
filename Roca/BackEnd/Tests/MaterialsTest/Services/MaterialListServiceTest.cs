using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials.Services
{
    [TestClass]
    public class MaterialListServiceTest : BaseTest
    {
        [TestMethod]
        public void GetAllHeadRevisionTest()
        {
            using (var tx = new TransactionScope())
            {
                MaterialList ml1 = null, ml2 = null, ml3 = null;
                using (var roca = CreateRocaUow())
                {
                    ml1 = Helper.InsertMaterialList();
                    ml1.Revision = "B";
                    roca.MaterialLists.Update(ml1);

                    ml2 = Helper.InsertMaterialList();
                    ml1.PreviousRevisionMlId = ml2.Id;
                    roca.MaterialLists.Update(ml1);

                    ml3 = Helper.InsertMaterialList();
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var mls = rocaService.MaterialListService.GetAllHeadRevision(ml1.ProjectId).ToList();
                    Assert.IsNotNull(mls.SingleOrDefault(ml => ml.Id == ml1.Id), "No devolvio una head revision");
                    Assert.IsNotNull(mls.SingleOrDefault(ml => ml.Id == ml3.Id), "No devolvio una head revision");
                    Assert.IsNull(mls.SingleOrDefault(ml => ml.Id == ml2.Id), "Devolvio una revision superada");
                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void GetHeadRevisionTest()
        {
            using (var tx = new TransactionScope())
            {
                MaterialList ml1 = null, ml2 = null, ml3 = null;
                using (var roca = CreateRocaUow())
                {
                    ml1 = Helper.InsertMaterialList();
                    ml1.Revision = "B";
                    roca.MaterialLists.Update(ml1);

                    ml2 = Helper.InsertMaterialList();
                    ml1.PreviousRevisionMlId = ml2.Id;
                    roca.MaterialLists.Update(ml1);

                    ml3 = Helper.InsertMaterialList();
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var ml4 = rocaService.MaterialListService.GetHeadRevision(ml1.Id);
                    var ml5 = rocaService.MaterialListService.GetHeadRevision(ml2.Id);
                    var ml6 = rocaService.MaterialListService.GetHeadRevision(ml3.Id);
                    Assert.AreEqual(ml1.Id, ml4.Id, "No devolvio la head revision");
                    Assert.AreEqual(ml1.Id, ml5.Id, "No devolvio la head revision");
                    Assert.AreEqual(ml3.Id, ml6.Id, "No devolvio la head revision");
                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void GetHistoryTest()
        {
            using (var tx = new TransactionScope())
            {
                MaterialList ml1 = null, ml2 = null, ml3 = null;
                using (var roca = CreateRocaUow())
                {
                    ml1 = Helper.InsertMaterialList();
                    ml1.Revision = "B";
                    roca.MaterialLists.Update(ml1);

                    ml2 = Helper.InsertMaterialList();
                    ml1.PreviousRevisionMlId = ml2.Id;
                    roca.MaterialLists.Update(ml1);

                    ml3 = Helper.InsertMaterialList();
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var ml4 = rocaService.MaterialListService.GetHistory(ml1.Id).ToList();
                    var ml5 = rocaService.MaterialListService.GetHistory(ml2.Id).ToList();
                    var ml6 = rocaService.MaterialListService.GetHistory(ml3.Id).ToList();

                    Assert.AreEqual(2, ml4.Count(), "No devolvio correctamente la historia de la ml");
                    Assert.IsTrue(ml4.Any(ml => ml.Id == ml1.Id), "No devolvio correctamente la historia de la ml");
                    Assert.IsTrue(ml4.Any(ml => ml.Id == ml2.Id), "No devolvio correctamente la historia de la ml");

                    Assert.AreEqual(2, ml5.Count(), "No devolvio correctamente la historia de la ml");
                    Assert.IsTrue(ml5.Any(ml => ml.Id == ml1.Id), "No devolvio correctamente la historia de la ml");
                    Assert.IsTrue(ml5.Any(ml => ml.Id == ml2.Id), "No devolvio correctamente la historia de la ml");

                    Assert.AreEqual(1, ml6.Count(), "No devolvio correctamente la historia de la ml");
                    Assert.IsTrue(ml6.Any(ml => ml.Id == ml3.Id), "No devolvio correctamente la historia de la ml");

                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void CreateNewRevisionTest()
        {
            using (var tx = new TransactionScope())
            {
                MaterialList ml1 = null, ml2 = null;
                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);

                    ml1 = Helper.InsertMaterialList();
                    ml1.Status = MaterialListStatus.Issued;
                    rocaService.MaterialListService.Update(ml1);

                    ml2 = Helper.CreateMaterialList();
                    ml2.ProjectId = ml1.ProjectId;
                    ml2.Title = ml1.Title;
                    ml2.DocNumber = ml1.DocNumber;
                    ml2.PreviousRevisionMlId = ml1.Id;
                    ml2.Revision = ml1.Revision + "2";

                    

                    
                    rocaService.MaterialListService.CreateNewRevision(ml2);
                }

                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var ml3 = rocaService.MaterialListService.GetFull(ml1.Id);
                    var ml4 = rocaService.MaterialListService.GetFull(ml2.Id);

                    Assert.AreEqual(ml3.Items.Count, ml4.Items.Count, "La nueva revision no tiene la misma cantidad de items que la revision anterior");

                    foreach (var prevItem in ml3.Items)
                    {
                        var newItem = ml4.Items.SingleOrDefault(i => i.MaterialId == prevItem.MaterialId);
                        Assert.IsNotNull(newItem, "La nueva revision no tiene uno de los materiales de la revision anterior ");
                        Assert.AreEqual(ml2.Id, newItem.MlId);
                        Assert.AreEqual(prevItem.Quantity, newItem.Quantity, "La cantidad del item de la nueva revision no coincide con la cantidad de la revision previa");
                    }

                }

                //tx.Complete();
            }
        }
    }
}
