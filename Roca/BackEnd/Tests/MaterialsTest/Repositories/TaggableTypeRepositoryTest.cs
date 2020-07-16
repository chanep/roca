using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials.Repositories
{
    [TestClass]
    public class TaggableTypeRepositoryTest : BaseTest
    {

        [TestMethod]
        public void AddTypeTest()
        {
            using (var tx = new TransactionScope())
            {

                var t = Helper.CreateTaggableType();

                using (var roca = CreateRocaUow())
                {
                    roca.TaggableTypes.Add(t);
                    roca.Commit();
                    Assert.IsTrue(t.Id > 0);
                }

                using (var roca = CreateRocaUow())
                {
                    var t2 = roca.TaggableTypes.Get(t.Id);
                    Assert.AreEqual(t.SpecialtyId, t2.SpecialtyId);
                    Assert.AreEqual(t.Name, t2.Name);

                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void AddTypeWithSubTypesTest()
        {
            using (var tx = new TransactionScope())
            {

                var t = Helper.CreateTaggableType("", 3);

                using (var roca = CreateRocaUow())
                {
                    roca.TaggableTypes.Add(t);
                    roca.Commit();
                    Assert.IsTrue(t.Id > 0);
                }

                using (var roca = CreateRocaUow())
                {
                    var t2 = roca.TaggableTypes.GetFull(t.Id);
                    Assert.AreEqual(3, t2.Subtypes.Count);
                    Assert.AreEqual(0, t2.Attributes.Count);
                    foreach (var child in t2.Subtypes)
                    {
                        Assert.AreEqual(t2.Id, child.ParentId);
                        Assert.AreEqual(0, child.Attributes.Count);
                    }
                    Console.WriteLine(t2.ToString(2));

                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void GetAllRootTest()
        {
            using (var tx = new TransactionScope())
            {

                var t = Helper.CreateTaggableType("", 3);

                using (var roca = CreateRocaUow())
                {
                    roca.TaggableTypes.Add(t);
                    roca.Commit();
                    Assert.IsTrue(t.Id > 0);
                }

                using (var roca = CreateRocaUow())
                {
                    var types = roca.TaggableTypes.GetAllRoot(t.SpecialtyId).ToList();
                    Assert.IsTrue(types.Count > 0);
                    foreach (var type in types)
                    {
                        Assert.IsNull(type.ParentId);
                    }
                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void DelteTypeTest()
        {
            using (var tx = new TransactionScope())
            {

                var t = Helper.CreateTaggableType("", 3, 2);


                using (var roca = CreateRocaUow())
                {
                    roca.TaggableTypes.Add(t);
                    roca.Commit();
                    Assert.IsTrue(t.Id > 0);
                }

                using (var roca = CreateRocaUow())
                {
                    roca.TaggableTypes.Delete(t);
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var t2 = roca.TaggableTypes.Get(t.Id);
                    Assert.IsNull(t2);
                }
                //tx.Complete();
            }
        }

        [TestMethod]
        public void DelteSubtypeTest()
        {
            using (var tx = new TransactionScope())
            {

                var t = Helper.CreateTaggableType("", 3, 2);
                int subtypeCount = t.Subtypes.Count;


                using (var roca = CreateRocaUow())
                {
                    roca.TaggableTypes.Add(t);
                    roca.Commit();
                    Assert.IsTrue(t.Id > 0);
                }

                using (var roca = CreateRocaUow())
                {
                    var subtype = t.Subtypes.ToList()[0];
                    roca.TaggableTypes.Delete(subtype);
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var t2 = roca.TaggableTypes.GetFull(t.Id);
                    Assert.AreEqual(subtypeCount - 1, t2.Subtypes.Count);
                }
                //tx.Complete();
            }
        }

        [TestMethod]
        public void UpdateTypeTest()
        {
            using (var tx = new TransactionScope())
            {

                var t = Helper.CreateTaggableType("", 3, 2);
                var newName = "mongo123";


                using (var roca = CreateRocaUow())
                {
                    roca.TaggableTypes.Add(t);
                    roca.Commit();
                    Assert.IsTrue(t.Id > 0);
                }

                using (var roca = CreateRocaUow())
                {
                    t.Name = newName;
                    roca.TaggableTypes.Update(t);
                    roca.Commit();
                }

                using (var roca = CreateRocaUow())
                {
                    var t2 = roca.TaggableTypes.GetFull(t.Id);
                    Assert.AreEqual(newName, t2.Name);
                    Assert.AreEqual(t.Subtypes.Count, t2.Subtypes.Count);
                }
                //tx.Complete();
            }
        }


        [TestMethod]
        public void AddTypeWithAttributesTest()
        {
            using (var tx = new TransactionScope())
            {

                var t = Helper.CreateTaggableType("", 3, 2);
                

                using (var roca = CreateRocaUow())
                {
                    roca.TaggableTypes.Add(t);
                    roca.Commit();
                    Assert.IsTrue(t.Id > 0);
                }

                using (var roca = CreateRocaUow())
                {
                    var t2 = roca.TaggableTypes.GetFull(t.Id);
                    Assert.AreEqual(3, t2.Subtypes.Count);
                    Assert.AreEqual(0, t2.Attributes.Count);
                    foreach (var child in t2.Subtypes)
                    {
                        Assert.AreEqual(t2.Id, child.ParentId);
                        Assert.AreEqual(2, child.Attributes.Count);
                        foreach (var attribute in child.Attributes)
                        {
                            Assert.AreEqual(child.Id, attribute.TypeId);
                        }
                    }
                    Console.WriteLine(t2.ToString(2));

                }

                //tx.Complete();
            }
        }


        [TestMethod]
        public void AddAttributeTest()
        {
            using (var tx = new TransactionScope())
            {

                var t = Helper.CreateTaggableType("", 3);

                using (var roca = CreateRocaUow())
                {
                    roca.TaggableTypes.Add(t);
                    roca.Commit();
                    Assert.IsTrue(t.Id > 0);
                }

                int subTypeId = t.Subtypes.ToList()[0].Id;

                using (var roca = CreateRocaUow())
                {
                    var attr = new TaggableAttribute() {Name = "AtributoX", TypeId = subTypeId};
                    var t2 = roca.TaggableTypes.AddAttribute(attr);
                    roca.Commit();
                    Assert.IsNotNull(attr.Id);
                }

                using (var roca = CreateRocaUow())
                {
                    var subType = roca.TaggableTypes.GetFull(subTypeId);
                    Assert.AreEqual(1, subType.Attributes.Count);
                    var attr = subType.Attributes.ToList()[0];
                    Assert.AreEqual(subTypeId, attr.TypeId);

                }

                //tx.Complete();
            }
        }
    }
}
