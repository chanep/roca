using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Transactions;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials.Repositories
{
    [TestClass]
    public class MaterialListRepositoryTest : BaseTest
    {
        [TestMethod]
        public void GetFullTest()
        {
            var roca = CreateRocaUow();
            var ml = roca.MaterialLists.GetFull(23);
        }

        [TestMethod]
        public void AddMaterialListTest()
        {
            using (var tx = new TransactionScope())
            {

                var ml = Helper.InsertMaterialList();

                using (var roca = CreateRocaUow())
                {
                    
                    var ml2 = roca.MaterialLists.GetFull(ml.Id);
                    var str1 = ml.ToString();
                    var str2 = ml2.ToString();
                    Assert.AreEqual(str1, str2, "No se agrego correctamente la entidad");
                    Assert.AreEqual(ml.Items.Count, ml2.Items.Count, "No se agregaron correctamente los Items de la entidad");
                    Console.WriteLine(ml2.ToString(1));
                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void UpdateMaterialListTest()
        {
            using (var tx = new TransactionScope())
            {

                var ml = Helper.InsertMaterialList();
                var revision = "Z";
                ml.Revision = revision;

                using (var roca = CreateRocaUow())
                {

                    roca.MaterialLists.Update(ml);
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var ml2 = roca.MaterialLists.Get(ml.Id);
                    Assert.AreEqual(revision, ml2.Revision, "No se actualizo correctamente la entidad");
                }

                //tx.Complete();
            }
        }


        [TestMethod]
        public void DeleteMaterialListTest()
        {
            using (var tx = new TransactionScope())
            {

                var ml = Helper.InsertMaterialList();
                var mlId = ml.Id;
                Material mat = null;
                Specialty specialty = null;

                using (var roca = CreateRocaUow())
                {
                    ml = roca.MaterialLists.GetFull(mlId);
                    mat = ml.Items.First().Material;
                    specialty = ml.Specialty;
                    roca.MaterialLists.Delete(ml);
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var ml2 = roca.MaterialLists.Get(mlId);
                    Assert.IsNull(ml2, "No se elimino correctamente la entidad");
                    var items = roca.MaterialLists.GetAllItems(mlId).ToList();
                    Assert.AreEqual(0, items.Count, "No se borraron los items en cascada al borrarse la MaterialList");
                    var matAux = roca.Materials.Get(mat.Id);
                    Assert.IsNotNull(matAux, "Al borrar la MaterialList se eliminaron los materiales");
                    var spAux = roca.Specialties.Get(specialty.Id);
                    Assert.IsNotNull(spAux, "Al borrar la MaterialList se elimino la specialty");
                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void LogicalDeleteMaterialListTest()
        {
            using (var tx = new TransactionScope())
            {

                var ml = Helper.InsertMaterialList();
                var mlId = ml.Id;

                using (var roca = CreateRocaUow())
                {

                    roca.MaterialLists.LogicalDelete(mlId);
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var ml2 = roca.MaterialLists.Get(mlId);
                    Assert.IsNotNull(ml2, "No se encontro la entidad borrada logicamente");
                    Assert.IsTrue(ml2.Deleted, "El campo Deleted no es true");
                    var ml3 = roca.MaterialLists.GetAll().SingleOrDefault(l => l.Id == mlId);
                    Assert.IsNull(ml3, "No se borro logicamente la entidad");
                }

                //tx.Complete();
            }
        }



        [TestMethod]
        public void GetAllItemsTest()
        {
            using (var tx = new TransactionScope())
            {

                var ml = Helper.InsertMaterialList();
                int itemCount = ml.Items.Count;

                using (var roca = CreateRocaUow())
                {
                    var items = roca.MaterialLists.GetAllItems(ml.Id);
                    Assert.AreEqual(itemCount, items.Count(), "No devolvio correctamente la lista de items");
                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void AddItemTest()
        {
            using (var tx = new TransactionScope())
            {

                var ml = Helper.InsertMaterialList();
                int itemCount = ml.Items.Count;


                using (var roca = CreateRocaUow())
                {
                    var newItem = Helper.CreateMlItem("New");
                    var repo = roca.MaterialLists;
                    newItem.MlId = ml.Id;
                    var item = repo.AddItem(newItem);
                    roca.Commit();
                    Console.WriteLine(item);
                }

                using (var roca = CreateRocaUow())
                {
                    var ml2 = roca.MaterialLists.GetFull(ml.Id);
                    Assert.AreEqual(itemCount + 1, ml2.Items.Count, "No se agrego correctamete el item");
                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void UpdateItemTest()
        {
            using (var tx = new TransactionScope())
            {

                var ml = Helper.InsertMaterialList();
                int itemCount = ml.Items.Count;
                var item1 = ml.Items.First();
                var newQuantity = 55.5;

                using (var roca = CreateRocaUow())
                {
                    item1.Quantity = newQuantity;
                    roca.MaterialLists.UpdateItem(item1);
                    roca.Commit();
                    
                }

                using (var roca = CreateRocaUow())
                {
                    var ml2 = roca.MaterialLists.GetFull(ml.Id);
                    var item2 = ml2.Items.FirstOrDefault(i => i.Id == item1.Id);
                    Assert.AreEqual(newQuantity, item2.Quantity, "No se actualizo correctamete el item");
                    Console.WriteLine(item2);
                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void UpdateItemAttachedTest()
        {
            using (var tx = new TransactionScope())
            {

                var ml = Helper.InsertMaterialList();
                int itemCount = ml.Items.Count;
                var item1 = ml.Items.First();
                var newQuantity = 55.5;

                using (var roca = CreateRocaUow())
                {
                    var itemAux = roca.MaterialLists.GetAllItems(ml.Id).FirstOrDefault(i => i.Id == item1.Id);
                    item1.Quantity = newQuantity;
                    roca.MaterialLists.UpdateItem(item1);
                    roca.Commit();

                }

                using (var roca = CreateRocaUow())
                {
                    var ml2 = roca.MaterialLists.GetFull(ml.Id);
                    var item2 = ml2.Items.FirstOrDefault(i => i.Id == item1.Id);
                    Assert.AreEqual(newQuantity, item2.Quantity, "No se actualizo correctamete el item");
                    Console.WriteLine(item2);
                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void DelteItemTest()
        {
            using (var tx = new TransactionScope())
            {

                var ml = Helper.InsertMaterialList();
                int itemCount = ml.Items.Count;
                var item1 = ml.Items.First();

                using (var roca = CreateRocaUow())
                {
                    roca.MaterialLists.DeleteItem(item1);
                    roca.Commit();

                }

                using (var roca = CreateRocaUow())
                {
                    var ml2 = roca.MaterialLists.GetFull(ml.Id);
                    var items = ml2.Items;
                    Assert.AreEqual(itemCount - 1, items.Count, "No se elimino correctamete el item");
                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void AddItemsTest()
        {
            using (var tx = new TransactionScope())
            {

                var ml = Helper.InsertMaterialList();
                int itemCount = ml.Items.Count;


                using (var roca = CreateRocaUow())
                {
                    var items = new List<MlItem>()
                    {
                        Helper.CreateMlItem("01"),
                        Helper.CreateMlItem("02")
                    };
                    

                    var repo = roca.MaterialLists;
                    foreach (var item in items)
                    {
                        item.MlId = ml.Id;
                        repo.AddItem(item);
                    }
                     
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var ml2 = roca.MaterialLists.GetFull(ml.Id);
                    Assert.AreEqual(itemCount + 2, ml2.Items.Count, "No se agregaron correctamete los items");
                }

                //tx.Complete();
            }
        }



    }
}
