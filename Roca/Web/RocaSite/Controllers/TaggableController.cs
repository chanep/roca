using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.Web.RocaSite.Infrastructure;
using Newtonsoft.Json;

namespace Cno.Roca.Web.RocaSite.Controllers
{
    public class TaggableController : BaseController
    {
        //
        // GET: /Taggable/

        public TaggableController(IRocaService rocaService, ISessionManager sessionManager) : base(rocaService, sessionManager)
        {
        }

        public ActionResult Types()
        {
            return PartialView();
        }

        public JsonResult GetTypes(int specialtyId)
        {
            var types = RocaService.TaggableTypeService.GetAllRoot(specialtyId).ToArray();
            return Json(types, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddType(TaggableType type)
        {
            var newType = RocaService.TaggableTypeService.AddType(type);
            return Json(newType, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateType(TaggableType type)
        {
            RocaService.TaggableTypeService.UpdateType(type);
        }

        [HttpPost]
        public void DeleteType(TaggableType type)
        {
            RocaService.TaggableTypeService.DeleteType(type);
        }

        [HttpPost]
        public void SaveAttributes(IEnumerable<TaggableAttribute> attributes)
        {
            RocaService.TaggableTypeService.OverwriteAttributes(attributes);
        }

    }
}
