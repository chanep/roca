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
    public class TaggableTypeServiceTest : BaseTest
    {
        [TestMethod]
        public void OverriteAttributesTest()
        {
            using (var tx = new TransactionScope())
            {

                var t = Helper.CreateTaggableType("", 1, 2);
                int subTypeId;

                using (var roca = CreateRocaUow())
                {
                    roca.TaggableTypes.Add(t);
                    roca.Commit();
                    Assert.IsTrue(t.Id > 0);
                    var t2 = roca.TaggableTypes.GetFull(t.Id);
                    subTypeId = t2.Subtypes.ToList()[0].Id;
                    
                }

                using (var roca = CreateRocaUow())
                {
                    string newAttrName = "AtributoMongo123";
                    var newAttr = new TaggableAttribute()
                    {
                        Name = newAttrName,
                        TypeId = subTypeId
                    };
                    var attrs = new List<TaggableAttribute>() {newAttr};

                    var rocaService = CreateRocaService(roca);
                    rocaService.TaggableTypeService.OverwriteAttributes(attrs);

                    var subType = roca.TaggableTypes.GetFull(subTypeId);
                    Assert.AreEqual(1, subType.Attributes.Count);
                    Assert.AreEqual(newAttrName, subType.Attributes.ToList()[0].Name);
                }              

                //tx.Complete();
            }
        }
    }
}
