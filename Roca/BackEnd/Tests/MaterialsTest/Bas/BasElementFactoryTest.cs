using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.EfDal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials.Bas
{
    [TestClass]
    public class BasElementFactoryTest
    {
        private IList<BasElementType> GetAllElementTypes()
        {
            IList<BasElementType> types;
            using (var uow = new RocaUow())
            {
                types = uow.BasElements.GetAllElementTypes();
            }
            return types;
        }

        [TestMethod]
        public void CreateElementWithExtraAttributesTest()
        {
            var elementTypes = GetAllElementTypes();
            var factory = new BasElementFactory(elementTypes);
            var pipeType = elementTypes.First(t => t.Code == "p.pipe");

            double peso = 3.3;

            var attWV = new BasClassAttributeWithValue(pipeType.Class.ExtraAttributes.First(a => a.Property == "Weight"));
            attWV.Value = peso.ToString();

            var element = factory.CreateElement(pipeType.Id, new List<BasClassAttributeWithValue>() {attWV});

            Assert.IsInstanceOfType(element, typeof(BasElementPiping),"Tipo de elemento creado incorrecto");
            Assert.AreEqual(peso, ((BasElementPiping)element).Weight, "Peso del elemento creo incorrecto");
        }

        [TestMethod]
        public void GetExtraAttributesTest()
        {
            var elementTypes = GetAllElementTypes();
            var factory = new BasElementFactory(elementTypes);
            var element = new BasElementPiping();
            element.Weight = 3.3;
            var attributes = factory.GetExtraAttributesWithValue(element);
            var weightAttr = attributes.First(a => a.Property == "Weight");
            Assert.AreEqual(element.Weight.ToString(), weightAttr.Value, "Value del attribute incorrecto");
        }
    }
}
