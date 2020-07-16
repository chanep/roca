using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;
using Cno.Roca.Web.RocaSite.Models;
using Cno.Roca.Web.RocaSite.Models.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Omu.ValueInjecter;

namespace Cno.Roca.Web.RocaSite.Tests
{
    public class Dto
    {
        public string Name { get; set; }
        public string SubCode { get; set; }
    }

    public class Entity
    {
        public string Name { get; set; }
        public SubEntity Sub { get; set; }
    }

    public class SubEntity
    {
        public string Code { get; set; }
    }

    [TestClass]
    public class DtoTest
    {

        [TestMethod]
        public void EntityToDto2Test()
        {

            var entity = new Entity() { Name = "Nombre", Sub = new SubEntity() { Code = "Codigo" } };
            var dto = new Dto();
            dto.InjectFrom<FlatLoopValueInjection>(entity);

        }

        [TestMethod]
        public void DtoToEntity2Test()
        {

            var dto = new Dto() { Name = "Nombre", SubCode = "Codigo" };
            var entity = new Entity();
            entity.InjectFrom<UnflatLoopValueInjection>(dto);

        }

        [TestMethod]
        public void TimeSheetItemDtoTest()
        {
            var proj = new Project()
            {
                Id = 2,
                Code = "Poli",
                Name = "Poliducto",
                Subprojects = new List<Project>(),
                Parent = new Project(){Id = 1},
                ParentId = 1
            };
            var i = new TimeSheetItem()
            {
                Id = 1,
                TaskId = 3,
                Subproject = proj,
                Hours = 40,
                Task = new LookUp(){Id=3,Value = "tarea de cucamona"}
            };

            var dto = new TimeSheetItemDto(i);
            Assert.AreEqual(i.Id, dto.Id);
            Assert.AreEqual(i.TaskId, dto.TaskId);
            Assert.AreEqual(i.Subproject.Name, dto.Subproject.Name);
            Assert.AreEqual(i.Subproject.ParentId, dto.SubprojectParentId);
            Assert.AreEqual(i.Task.Value, dto.TaskValue);

            var i2 = dto.GetEntity();
            Assert.AreEqual(dto.Id, i2.Id);
            Assert.AreEqual(dto.TaskId, i2.TaskId);
            Assert.IsNull(i2.Task);

            var i3 = dto.GetEntityDeep();
            Assert.AreEqual(dto.Id, i3.Id);
            Assert.AreEqual(dto.TaskId, i3.TaskId);
            Assert.AreEqual(dto.TaskValue, i3.Task.Value);

        }
    }
}
