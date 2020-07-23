using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials.Services
{
    [TestClass]
    public class TimeSheetServiceTest : BaseTest
    {
        [TestMethod]
        public void AddTiemSheetTest()
        {
            using (var tx = new TransactionScope())
            {
                TimeSheet t1 = null;
                using (var roca = CreateRocaUow())
                {
                    t1 = Helper.CreateTimeSheet();
                    var rocaService = CreateRocaService(roca);
                    rocaService.TimeSheetService.Add(t1);
                }

                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var t2 = rocaService.TimeSheetService.GetFull(t1.Id);
                    Assert.AreEqual(t1.Id, t2.Id);
                    Assert.AreEqual(t1.ControlDate, t2.ControlDate);
                    Assert.AreEqual(t1.UserId, t2.UserId);
                    Assert.AreEqual(t1.LeaderId, t2.LeaderId);
                    Assert.AreEqual(t1.SpecialtyId, t2.SpecialtyId);
                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void SaveTimeSheetItemsTest()
        {
            using (var tx = new TransactionScope())
            {
                TimeSheet t1 = null;
                IList<TimeSheetItem> items;
                using (var roca = CreateRocaUow())
                {
                    t1 = Helper.CreateTimeSheet();
                    var rocaService = CreateRocaService(roca);
                    rocaService.TimeSheetService.Add(t1);
                    
                    var docItems = Helper.CreateDocTimeSheetItem(t1.Id, 2);
                    var taskItems = Helper.CreateTaskTimeSheetItem(t1.Id, 2);
                    items = docItems.Concat(taskItems).ToList();
                    items[0].Hours = 37;
                    rocaService.TimeSheetService.SaveItems(t1.Id, items);
                }

                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var items2 = rocaService.TimeSheetService.GetAllItems(t1.Id).ToList();
                    Assert.AreEqual(4, items2.Count);
                    foreach (var item in items)
                    {
                        var item2 = items2.FirstOrDefault(i => i.Id == item.Id);
                        Assert.IsNotNull(item2, "No se encontro el item salvado");
                        Assert.AreEqual(item.DocumentId, item2.DocumentId);
                        Assert.AreEqual(item.TaskId, item2.TaskId);
                        Assert.AreEqual(item.Hours, item2.Hours);
                    }
                }

                //tx.Complete();
            }
        }

        //[TestMethod]
        //public void UpdateTimeSheetTest()
        //{
        //    using (var tx = new TransactionScope())
        //    {
        //        TimeSheet t1 = Helper.InsertTimeSheet(2, 2);
        //        TimeSheet t2, t3;
        //        TimeSheetItem newItem = Helper.CreateDocTimeSheetItem(0, 1)[0];
        //        using (var roca = CreateRocaUow())
        //        {
        //            var rocaService = CreateRocaService(roca);
        //            t2 = rocaService.TimeSheetService.GetFull(t1.Id);
        //            t2.Items.Add(newItem);
        //            Assert.AreEqual(5, t2.Items.Count);
        //            t2.Leader = rocaService.CommonService.GetUsersByRole(Roles.Leader).ToList()[1];
        //            t2.LeaderId = t2.Leader.Id;
        //            t2.Leader = null;
        //            t2.Specialty = null;
        //            t2.User = null;
        //            t2.Items.Clear();
        //            rocaService.TimeSheetService.Update(t2);
        //        }

        //        using (var roca = CreateRocaUow())
        //        {
        //            var rocaService = CreateRocaService(roca);
        //            t3 = rocaService.TimeSheetService.GetFull(t1.Id);
        //            Assert.AreEqual(4, t3.Items.Count);
        //        }

        //        //tx.Complete();
        //    }
        //}

        [TestMethod]
        public void OverwriteTimeSheetItemsTest()
        {
            using (var tx = new TransactionScope())
            {
                TimeSheet t1 = Helper.InsertTimeSheet(2,2);
                IList<TimeSheetItem> items, items2, items3;
                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    items = rocaService.TimeSheetService.GetAllItems(t1.Id).ToList();
                }

                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    items2 = Helper.CreateDocTimeSheetItem(t1.Id, 1);
                    items2[0].Hours = 40;
                    rocaService.TimeSheetService.SaveItems(t1.Id, items2);
                }

                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    items3 = rocaService.TimeSheetService.GetAllItems(t1.Id).ToList();
                    Assert.AreEqual(1, items3.Count);
                    Assert.AreEqual(40, items3[0].Hours);
                }

                //tx.Complete();
            }
        }

        [TestMethod]
        public void GetDefaultersTest()
        {
            using (var tx = new TransactionScope())
            {
                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var toDate = DateTime.Now.Date;
                    var fromDate = toDate.AddDays(-6);
                    var items = rocaService.TimeSheetService.GetDefaulters(fromDate, toDate).ToList();
                }


                //tx.Complete();
            }
        }

        [TestMethod]
        public void GetAllDocItemsTest()
        {
            using (var tx = new TransactionScope())
            {
                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var toDate = DateTime.Now.Date;
                    var fromDate = toDate.AddDays(-6);
                    var filter = new TimeSheetItemFilter()
                    {
                        FromDate = fromDate,
                        ToDate = toDate
                    };
                    var items = rocaService.TimeSheetService.GetAllDocItems(filter).ToList();
                    Assert.IsNotNull(items[0].TimeSheet.User.FullName);
                }


                //tx.Complete();
            }
        }


        [TestMethod]
        public void GetAllUserLastTest()
        {
            using (var tx = new TransactionScope())
            {
                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var items = rocaService.TimeSheetService.GetAllUserLast().ToList();
                }


                //tx.Complete();
            }
        }
    }
}
