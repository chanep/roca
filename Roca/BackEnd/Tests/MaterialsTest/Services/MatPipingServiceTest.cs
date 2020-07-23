using System;
using System.Linq;
using System.Transactions;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials.Services
{
    [TestClass]
    public class MatPipingServiceTest : BaseTest
    {
        [TestMethod]
        public void GetAllTest()
        {
            string commCode = "LS1A11A12BAA";
            var filter = new MatPipingFilter()
            {
                ProjectId = "0002",
                CommodityCode = commCode,
            };

            using (var tx = new TransactionScope())
            {
                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var mats = rocaService.MatPipingService.GetAll(filter).ToList();
                    Assert.IsTrue(mats.Count>0);
                    foreach (var mat in mats)
                    {
                        Assert.AreEqual(commCode, mat.CommodityCode);
                    }
                }

                //tx.Complete();
            }
        }
    }
}
