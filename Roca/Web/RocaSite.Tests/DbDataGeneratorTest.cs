using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Cno.Roca.Web.RocaSite.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.Web.RocaSite.Tests
{
    [TestClass]
    public class DbDataGeneratorTest : BaseTest
    {
        [TestMethod]
        public void CreateTestDataTest()
        {
            DbDataGenerator.CreateTestData();
                    
        }

        [TestMethod]
        public void DeleteTestDataTest()
        {
            DbDataGenerator.DeleteTestData();
        }


        [TestMethod]
        public void DeleteTimeSheetTestData()
        {
            DbDataGenerator.DeleteTimeSheetTestData();
        }
    }
}
