using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials.Bas
{
    [TestClass]
    public class BasCodeTest : BaseTest
    {
        //[TestMethod]
        public void ReformatDimensionsTest()
        {
            using (var roca = CreateRocaUow())
            {
                var fields = new[] { ".dim1", ".dim2", ".diametro" };
                foreach (var field in fields)
                {
                    var basCodes = roca.BasCodes.GetAll().Where(b => b.Field.Contains(field)).ToList();
                    foreach (var basCode in basCodes)
                    {
                        if (basCode.Description == "-")
                            continue;
                        basCode.Description = basCode.Description.Replace('+', '.');
                        if (basCode.Description.Contains("mm"))
                        {
                            basCode.Description = basCode.Description.Replace("mm", "(mm)");
                        }
                        else
                        {
                            basCode.Description += "(\")";
                        }
                        basCode.ShortDescription = basCode.Description;

                        roca.BasCodes.Update(basCode);
                    }
                }

                fields = new[] { ".sched1", ".sched2" };
                foreach (var field in fields)
                {
                    var basCodes = roca.BasCodes.GetAll().Where(b => b.Field.Contains(field)).ToList();
                    foreach (var basCode in basCodes)
                    {
                        if (basCode.Description.Contains("mm"))
                        {
                            basCode.Description = basCode.Description.Replace("mm", "(mm)");
                            basCode.ShortDescription = basCode.Description;
                            roca.BasCodes.Update(basCode);
                        }                       
                    }
                }

                roca.Commit();
            }
        }

        //[TestMethod]
        public void ReformatDimensionsTest2()
        {
            using (var roca = CreateRocaUow())
            {
                var fields = new[] { ".dim1", ".dim2", ".diametro", ".sched1", ".sched2" };
                foreach (var field in fields)
                {
                    var basCodes = roca.BasCodes.GetAll().Where(b => b.Field.Contains(field)).ToList();
                    foreach (var basCode in basCodes)
                    {
                        if (basCode.Description == "-(\")")
                        {
                            basCode.Description = "-";
                            basCode.ShortDescription = "-";
                            roca.BasCodes.Update(basCode);
                        }

                    }
                }

                roca.Commit();
            }
        }
    }
}
