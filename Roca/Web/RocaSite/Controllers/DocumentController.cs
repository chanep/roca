using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Cno.Roca.Web.RocaSite.Infrastructure;
using Cno.Roca.Web.RocaSite.Models.Dtos;
using Newtonsoft.Json;

namespace Cno.Roca.Web.RocaSite.Controllers
{
    public class DocumentController : BaseController
    {
        //
        // GET: /Document/

        public DocumentController(IRocaService rocaService, ISessionManager sessionManager) : base(rocaService, sessionManager)
        {
        }

        [CustomAuthorize(Roles = Roles.Admin)]
        public ActionResult New()
        {
            return PartialView();
        }

        [CustomAuthorize(Roles = Roles.Admin)]
        public ActionResult Edit()
        {
            return PartialView();
        }

        public JsonResult GetAutosuggestDoc(string property, string term, string filtersStr)
        {
            var filter = JsonConvert.DeserializeObject<DocumentFilter>(filtersStr);
            var list = RocaService.CommonService.GetDocuments(filter);

            var suggestList = list.Select(d => new { value = GetPropertyString(d, property) })
                                  .Distinct()
                                  .Take(50)
                                  .ToArray();
            return Json(suggestList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocument(DocumentFilter filter)
        {
            var docs = RocaService.CommonService.GetDocuments(filter, true).ToList();
            Document doc = null;
            if (docs.Count() == 1)
                doc = docs.First();
            return Json(doc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOptions()
        {          
            var specialties = RocaService.CommonService.GetAllSpecialties();
            var projects = RocaService.CommonService.GetAllRootProjects();
            var projDtos = ProjectDto.CreateList(projects);
            var docTypes = RocaService.CommonService.GetAllLookUpsByType(LookUpTypes.DocType);

            var options = new { Projects = projDtos, DocTypes = docTypes, Specialties = specialties };
            return Json(options, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [CustomAuthorize(Roles = Roles.Admin)]
        public JsonResult Add(Document document)
        {
            var exists = RocaService.CommonService
                            .GetDocuments(new DocumentFilter() {DocNumber = document.DocNumber}, true).Any();
            if(exists)
                throw new InPageErrorException(string.Format("El documento {0} ya existe", document.DocNumber));

            var newDoc = RocaService.CommonService.AddDocument(document);
            return Json(newDoc, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize(Roles = Roles.Admin)]
        public void Update(Document document)
        {
            RocaService.CommonService.UpdateDocument(document);
        }
    }
}
