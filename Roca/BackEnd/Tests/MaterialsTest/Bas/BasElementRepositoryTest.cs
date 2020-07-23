using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials.Bas
{
    [TestClass]
    public class BasElementRepositoryTest : BaseTest
    {
        [TestMethod]
        public void GetAllElementTypesTest()
        {
            using (var roca = CreateRocaUow())
            {
                var types = roca.BasElements.GetAllElementTypes().ToList();

            }
            
        }
    }
}
