using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials.Services
{
    [TestClass]
    public class CommonServiceTest : BaseTest
    {

        [TestMethod]
        public void GetAllRootProjectsTest()
        {
            using (var tx = new TransactionScope())
            {
                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var projects = rocaService.CommonService.GetAllRootProjects().ToList();
                    Assert.IsTrue(projects.Any());
                    foreach (var project in projects)
                    {
                        Assert.IsNull(project.ParentId);
                        foreach (var sub in project.Subprojects)
                        {
                            Assert.AreEqual(project.Code, sub.Parent.Code);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void GetUsersByRoleTest()
        {
            using (var tx = new TransactionScope())
            {
                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var role = Roles.Leader;
                    var users = rocaService.CommonService.GetUsersByRole(role);
                    Assert.IsTrue(users.Any());
                    foreach (var user in users)
                    {
                        Console.WriteLine(user);
                        Assert.IsTrue(user.Roles.Contains(role));
                    }
                }
            }
        }


        [TestMethod]
        public void GetDocumentsByFilterTest()
        {
            using (var tx = new TransactionScope())
            {
                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var filter = new DocumentFilter()
                    {
                        DocNumber = "T904",
                        ProjectId = 3,
                        Title = "ISOM"
                    };
                    var docs = rocaService.CommonService.GetDocuments(filter);
                    Console.WriteLine("Count: " + docs.Count());
                    Assert.IsTrue(docs.Any());
                    foreach (var document in docs)
                    {
                        Assert.IsTrue(document.DocNumber.Contains(filter.DocNumber));
                        Assert.AreEqual(filter.ProjectId, document.ProjectId);
                        Assert.IsTrue(document.Title.Contains(filter.Title));
                    }
                }
            }
        }

        [TestMethod]
        public void GetDocumentsByFilterWithAccentsTest()
        {
            using (var tx = new TransactionScope())
            {
                using (var roca = CreateRocaUow())
                {
                    var rocaService = CreateRocaService(roca);
                    var filter = new DocumentFilter()
                    {
                        Title = "indi"
                        //SpecialtyId = 3
                    };
                    var docs = rocaService.CommonService.GetDocuments(filter);
                    Console.WriteLine("Count: " + docs.Count());
                    Assert.IsTrue(docs.Any());
                    foreach (var document in docs)
                    {
                        Console.WriteLine("SpecialtyId: " + document.SpecialtyId + "  Title: " + document.Title);
                        //Assert.IsTrue(document.Title.Contains(filter.Title));
                    }
                }
            }
        }
    }
}
