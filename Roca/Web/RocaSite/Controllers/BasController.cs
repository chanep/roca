using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cno.Roca.BackEnd.ExcelTools;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Cno.Roca.Web.RocaSite.Infrastructure;
using Cno.Roca.Web.RocaSite.Models.Dtos;

namespace Cno.Roca.Web.RocaSite.Controllers
{
    public class BasController : BaseController
    {
        private IBasService BasService { get; set; }


        private BasElementFactory _elementFactory;

        private BasElementFactory ElementFactory
        {
            get
            {
                if(_elementFactory == null)
                    _elementFactory = new BasElementFactory(BasService.GetAllElementTypes());
                return _elementFactory;
            }
        }

        public BasController(IRocaService rocaService, ISessionManager sessionManager)
            : base(rocaService, sessionManager)
        {
            BasService = RocaService.BasService;          
        }

        [CustomAuthorize(Roles = Roles.BasAdmin)]
        public ActionResult New()
        {
            return PartialView();
        }

        [CustomAuthorize(Roles = Roles.BasAdmin)]
        public ActionResult Edit()
        {
            return PartialView();
        }

        [CustomAuthorize(Roles = Roles.BasAdmin)]
        public ActionResult ElementDetails()
        {
            return PartialView();
        }

        [CustomAuthorize(Roles = Roles.BasWrite)]
        public ActionResult ElementList()
        {
            return PartialView();
        }

        public JsonResult GetAllElementTypes()
        {
            var elementTypes = BasService.GetAllElementTypes();
            if (!LoggedUser.IsInRole(Roles.SuperAdmin))
                elementTypes = elementTypes.Where(t => t.Specialties.Select(s => s.Id).Intersect(LoggedUser.Specialties.Select(s => s.Id)).Any())
                        .ToList();
            return Json(elementTypes, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetAllCodesByField(string fieldCode)
        {
            var list = RocaService.BasService.GetAllCodesByField(fieldCode);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCodesByFields(string[] fieldCodes)
        {
            var list = new List<IEnumerable<BasCode>>(); 
            foreach (var fieldCode in fieldCodes)
            {
                list.Add(RocaService.BasService.GetAllCodesByField(fieldCode));
            }
            
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetElement(int id)
        {
            var element = BasService.GetElement(id);
            var dto = BuildElementDetailsDto(element);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmptyElement(int typeId)
        {
            var element = ElementFactory.CreateElement(typeId);
            var dto = BuildElementDetailsDto(element);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllElements(BasElementFilter filter)
        {
            SetFilterSpecialty(filter);
            var count = BasService.GetAllElementsCount(filter);
            var elements = BasService.GetAllElements(filter);
            var dtos = new List<BasElementListDto>();
            foreach (var element in elements)
            {
                var dto = BuildElementListDto(element);
                dtos.Add(dto);
            }
            var result = new {Count = count, Elements = dtos};
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public FileResult GetSisepcCatalog(BasElementFilter filter)
        {
            //var str = "hola;chau";
            //var bytes = Encoding.UTF8.GetBytes(str);
            //return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "mongo.csv");

            SetFilterSpecialty(filter);;
            var elements = BasService.GetAllElements(filter);
            var sElements = new List<SisepcElement>();
            foreach (var e in elements)
            {
                var sElement = e.CreateSisepcElement();
                sElements.Add(sElement);
            }

            var templateName = Server.MapPath("~/App_Data/Sisepc.xlsx");
            var stream = new SisepcExport().GenerateCatalog(templateName, sElements);
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "sisepc.xlsx";
            return File(stream, contentType, fileName);
        }



        [HttpPost]
        [CustomAuthorize(Roles = Roles.Write)]
        public JsonResult SaveElement(BasElementDetailsDto element)
        {
            var entity = BuildElement(element);
            entity.Validate();

            if (entity.Id == 0)
            {
                if(BasService.GetAllElements(new BasElementFilter(){FullCode = entity.FullCode}).Any())
                    throw new InPageErrorException("Ya existe un elemento con codigo: " + entity.FullCode);
                entity = BasService.AddElement(entity);
            }
            else
                BasService.UpdateElement(entity);

            element = BuildElementDetailsDto(entity);

            return Json(element, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize(Roles = Roles.BasAdmin)]
        public void DeleteElement(int id)
        {
            RocaService.BasService.DeleteElement(id);
        }

        [HttpPost]
        [CustomAuthorize(Roles = Roles.BasAdmin)]
        public JsonResult AddCode(BasCode basCode)
        {
            var result =  RocaService.BasService.AddCode(basCode);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize(Roles = Roles.BasAdmin)]
        public void DeleteCode(int id)
        {
            RocaService.BasService.DeleteCode(id);
        }

        [HttpPost]
        [CustomAuthorize(Roles = Roles.BasAdmin)]
        public void UpdateCode(BasCode basCode)
        {
            try
            {
                RocaService.BasService.UpdateCode(basCode);
            }
            catch (RocaUserException ex)
            {
                throw new InPageErrorException(ex.Message);
            }
        }

        private BasElementDetailsDto BuildElementDetailsDto(BasElement element)
        {
            var dto = new BasElementDetailsDto()
            {
                Id = element.Id,
                FullCode = element.FullCode,
                TypeId = element.TypeId,
                Unit = element.Unit,
                Observations = element.Observations,
                Weight = Math.Round(element.Weight, 8),
                Fields = BasCodeFieldDto.CreateList(element.Fields),
                ExtraAttributes = ElementFactory.GetExtraAttributesWithValue(element)
            };
            return dto;
        }

        private BasElementListDto BuildElementListDto(BasElement element)
        {
            var dto = new BasElementListDto()
            {
                Id = element.Id,
                FullCode = element.FullCode,
                TypeId = element.TypeId,
                Unit = element.Unit,
                Description = element.Description,
                FamilyDescription = element.FamilyDescription,
                DimensionalDescription = element.DimensionalDescription,
                Observations = element.Observations
            };
            return dto;
        }

        private BasElement BuildElement(BasElementDetailsDto dto)
        {
            var element = ElementFactory.CreateElement(dto.TypeId, dto.ExtraAttributes);
            if (dto.Fields.Count != element.Fields.Count)
                throw new RocaException(string.Format(
                        "Cantidad de campos incorrectos de BasElementDetailsDto. Son {0} y deberian ser {1} para un elementTypeId={2}",
                        dto.Fields.Count, element.Fields.Count, dto.TypeId));
            foreach (var field in element.Fields)
            {
                var fieldDto = dto.Fields[field.FieldDefinition.Order];
                field.Id = fieldDto.Id;
                field.ElementId = fieldDto.ElementId;
                field.BasCodeId = fieldDto.BasCodeId;
            }
            element.Id = dto.Id;
            element.FullCode = dto.FullCode;
            element.Unit = dto.Unit;
            element.Observations = dto.Observations;
            element.Weight = dto.Weight;

            return element;
        }

        private void SetFilterSpecialty(BasElementFilter filter)
        {
            const int coordId = 8;
            if (filter.TypeId != 0)
                return;
            if(LoggedUser.IsInRole(Roles.SuperAdmin))
                return;
            if (LoggedUser.Specialties.Select(s => s.Id).Contains(coordId))
                return;
            filter.SpecialtyId = LoggedUser.Specialties.First().Id;
        }
    }
}
