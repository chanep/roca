using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Util;
using System.Xml.Schema;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials.Bas
{
    [TestClass]
    public class BasElementTest : BaseTest
    {

        [TestMethod]
        public void GetAllBasElementTypesTest()
        {

            using (var uow = CreateRocaUow())
            {
                var basService = CreateRocaService(uow).BasService;

                var elementTypes = basService.GetAllElementTypes().ToList();
                Console.WriteLine("ElementType Count: " + elementTypes.Count);

            }

        }


        [TestMethod]
        public void BasElementPipingFittingTest()
        {
            var desc = "PE1L";
            var estandar = "P02";
            var serie1 = "B";
            var serie2 = "0";
            var material = "FAA";
            var extremo1 = "C";
            var extremo2 = "0";
            var dim1 = "10";
            var dim2 = "100";
            var sched1 = "0081";
            var sched2 = "S120";

            var familyCode = desc + estandar + serie1 + serie2 + material + extremo1 + extremo2;
            var dimCode = dim1 + dim2 + sched1 + sched2;
            var fullCode = familyCode + dimCode;
            using (var tx = new TransactionScope())
            {
                BasElement element = null;
                BasElement element2 = null;
                double weight = 12.3;


                using (var uow = CreateRocaUow())
                {
                    var basService = CreateRocaService(uow).BasService;
                    var elementType = basService.GetAllElementTypes().First(t => t.Code == "p.fitting");
                    element = basService.BuildElementByCode(fullCode, elementType.Id);
                    element.Unit = "pz";
                    ((BasElementPiping)element).Weight = weight;

                    uow.BasElements.Add(element);
                    uow.Commit();
                }

                using (var uow = CreateRocaUow())
                {
                    element2 = uow.BasElements.Get(element.Id);
                    Assert.IsNotNull(element2, "Fallo BaseElementRepository.Get");
                    Assert.IsInstanceOfType(element2, typeof(BasElementPiping), "Type incorrecto");
                    Assert.AreEqual(element.FullCode, element2.FullCode, "FullCodeIncorrecto");
                    Assert.AreEqual(element.Fields.Count, element2.Fields.Count, "Cantidad de fields incorrecto");
                    Assert.AreEqual(weight, ((BasElementPiping)element).Weight, "Weight incorrecto");
                    var fields = element.Fields.ToList();
                    var fields2 = element2.Fields.ToList();
                    for (int i=0; i< fields.Count; i++)
                    {
                        var field = fields[i];
                        var field2 = fields2[i];
                        Assert.AreEqual(field.BasCodeId, field2.BasCodeId, "bascodeId incorrecto");
                        Assert.AreEqual(field.ElementId, field2.ElementId, "elementId incorrecto");
                        Assert.AreEqual(field.FieldDefinitionId, field2.FieldDefinitionId, "FieldDefinitionId incorrecto");

                    }

                }

            }

        }


        [TestMethod]
        public void BasElementPipingValveTest()
        {
            var desc = "VB01";
            var estandar = "01";
            var serie = "A1";
            var extremo = "S1";
            var operacion = "L";
            var material = "FA";
            var trim = "TA";
            var sello = "10";
            var observaciones = "03";
            var diametro = "32";

            var familyCode = desc + estandar + serie + extremo + operacion + material + trim + sello + observaciones; ;
            var dimCode = diametro;
            var fullCode = familyCode + dimCode;

            using (var tx = new TransactionScope())
            {
                BasElement element = null;
                BasElement element2 = null;


                using (var uow = CreateRocaUow())
                {
                    var basService = CreateRocaService(uow).BasService;
                    var elementType = basService.GetAllElementTypes().First(t => t.Code == "p.valve");
                    element = basService.BuildElementByCode(fullCode, elementType.Id);
                    element.Unit = "u";
                    uow.BasElements.Add(element);
                    uow.Commit();
                }

                using (var uow = CreateRocaUow())
                {
                    element2 = uow.BasElements.Get(element.Id);
                    Assert.IsNotNull(element2, "Fallo BaseElementRepository.Get");
                    Assert.AreEqual(element2.GetType(), typeof(BasElementValve), "Type incorrecto");
                    Assert.AreEqual(element.FullCode, element2.FullCode, "FullCodeIncorrecto");
                    Assert.AreEqual(element.Fields.Count, element2.Fields.Count, "Cantidad de fields incorrecto");
                    var fields = element.Fields.ToList();
                    var fields2 = element2.Fields.ToList();
                    for (int i = 0; i < fields.Count; i++)
                    {
                        var field = fields[i];
                        var field2 = fields2[i];
                        Assert.AreEqual(field.BasCodeId, field2.BasCodeId, "bascodeId incorrecto");
                        Assert.AreEqual(field.ElementId, field2.ElementId, "elementId incorrecto");
                        Assert.AreEqual(field.FieldDefinitionId, field2.FieldDefinitionId, "FieldDefinitionId incorrecto");

                    }

                }

            }
        }


        [TestMethod]
        public void GetAllByFilterTest()
        {
            var desc = "X123";
            var estandar = "X12";
            var serie1 = "Ñ";
            var serie2 = "Ñ";
            var material = "FAA";
            var extremo1 = "C";
            var extremo2 = "0";
            var dim1 = "10";
            var dim2 = "100";
            var sched1 = "0081";
            var sched2 = "S120";

            var bcList = new List<BasCode>();
            BasCode bc = null;

            bcList.Add(new BasCode()
            {
                Code = desc,
                Description = "abc 123",
                Field = "p.fitting.desc",
                ShortDescription = ""
            });

            bcList.Add(new BasCode()
            {
                Code = estandar,
                Description = "def 456",
                Field = "p.fitting.estandar",
                ShortDescription = ""
            });

            bcList.Add(new BasCode()
            {
                Code = serie1,
                Description = "ghi 789",
                Field = "p.fitting.serie",
                ShortDescription = ""
            });


            var familyCode = desc + estandar + serie1 + serie2 + material + extremo1 + extremo2;
            var dimCode = dim1 + dim2 + sched1 + sched2;
            var fullCode = familyCode + dimCode;
            BasElementType elementType  = null;
            int elementId = 0;

            using (var tx = new TransactionScope())
            {
               
                using (var uow = CreateRocaUow())
                {
                    foreach (var basCode in bcList)
                    {
                        uow.BasCodes.Add(basCode);
                    }
                    uow.Commit();

                    var basService = CreateRocaService(uow).BasService;
                    BasElement element = null;
                    elementType = basService.GetAllElementTypes().First(t => t.Code == "p.fitting");
                    element = basService.BuildElementByCode(fullCode, elementType.Id);
                    element.Unit = "pz";
                    element.Observations = "Clase:A";
                    ((BasElementPiping)element).Weight = 12.3;

                    uow.BasElements.Add(element);

                    uow.Commit();
                    elementId = element.Id;
                }

                using (var uow = CreateRocaUow())
                {
                    var basService = CreateRocaService(uow).BasService;

                    var search1 = "456";
                    var search2 = "abc def";                    
                    var search3 = "abc valve";
                    var search4 = "abc Clase:A";
                    var search5 = "Clase:A ekjgdkfg";
                    var filter = new BasElementFilter();

                    filter.Description = search1;
                    var elements = basService.GetAllElements(filter).ToList();
                    Assert.AreEqual(1, elements.Count, "No encontro el elemento buscando el termino: " + filter.Description);
                    Assert.AreEqual(elementId, elements[0].Id, "No encontro el elemento buscando el termino: " + filter.Description);

                    filter.Description = search2;
                    elements = basService.GetAllElements(filter).ToList();
                    Assert.AreEqual(1, elements.Count, "No encontro el elemento buscando el termino: " + filter.Description);
                    Assert.AreEqual(elementId, elements[0].Id, "No encontro el elemento buscando el termino: " + filter.Description);

                    filter.Description = search3;
                    elements = basService.GetAllElements(filter).ToList();
                    Assert.AreEqual(0, elements.Count, "Encontro el elemento buscando el termino: " + filter.Description);

                    filter.Description = search4;
                    elements = basService.GetAllElements(filter).ToList();
                    Assert.AreEqual(1, elements.Count, "No encontro el elemento buscando el termino: " + filter.Description);
                    Assert.AreEqual(elementId, elements[0].Id, "No encontro el elemento buscando el termino: " + filter.Description);

                    filter.Description = search5;
                    elements = basService.GetAllElements(filter).ToList();
                    Assert.AreEqual(0, elements.Count, "Encontro el elemento buscando el termino: " + filter.Description);

                    filter.Description = null;
                    filter.FullCode = fullCode;
                    elements = basService.GetAllElements(filter).ToList();
                    Assert.AreEqual(1, elements.Count, "No encontro el elemento buscando el fullcode: " + fullCode);
                    Assert.AreEqual(elementId, elements[0].Id, "No encontro el elemento buscando el fullcode: " + fullCode);

                    filter.TypeId = elementType.Id;
                    elements = basService.GetAllElements(filter).ToList();
                    Assert.AreEqual(1, elements.Count, "No encontro el elemento buscando por fullcode y elementTypeId");
                    Assert.AreEqual(elementId, elements[0].Id, "No encontro el elemento buscando por fullcode y elementTypeId");

                    filter.TypeId = elementType.Id + 1;
                    elements = basService.GetAllElements(filter).ToList();
                    Assert.AreEqual(0, elements.Count, "encontro el elemento buscando por fullcode y elementTypeId incorrectos");

                    filter.Description = null;
                    filter.TypeId = null;
                    filter.FullCode = fullCode;
                    filter.SpecialtyId = 1; //piping
                    elements = basService.GetAllElements(filter).ToList();
                    Assert.AreEqual(1, elements.Count, "No encontro el elemento buscando por fullcode y specialtyId");
                    Assert.AreEqual(elementId, elements[0].Id, "No encontro el elemento buscando por fullcode y specialtyId");

                    filter.SpecialtyId = 2;
                    elements = basService.GetAllElements(filter).ToList();
                    Assert.AreEqual(0, elements.Count, "encontro el elemento buscando por fullcode y specialtyId incorrecta");
                }

            }

        }


        //[TestMethod]
        //public void BasElementXTest()
        //{
        //    using (var tx = new TransactionScope())
        //    {
        //        BasElement element = null;
        //        BasElement element2 = null;


        //        using (var uow = CreateRocaUow())
        //        {
        //            element = new BasElementX();
        //            element.TypeId = 3;
        //            element.FullCode = "codigo";

        //            uow.BasElements.Add(element);
        //            uow.Commit();
        //        }

        //        using (var uow = CreateRocaUow())
        //        {
        //            element2 = uow.BasElements.Get(element.Id);
        //            Assert.IsNotNull(element2, "Fallo BaseElementRepository.Get");
        //            Assert.IsInstanceOfType(element2, typeof(BasElementX), "Type incorrecto");
        //            Assert.AreEqual(element.FullCode, element2.FullCode, "FullCode Incorrecto");

        //        }

        //    }
        //}
    }
}
