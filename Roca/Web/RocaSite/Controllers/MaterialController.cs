using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.Web.RocaSite.Infrastructure;
using Cno.Roca.Web.RocaSite.Models;
using Cno.Roca.Web.RocaSite.Models.Dtos;
using Newtonsoft.Json.Linq;

namespace Cno.Roca.Web.RocaSite.Controllers
{
    public class MaterialController : BaseController
    {
        public MaterialController(IRocaService rocaService, ISessionManager sessionManager) : base(rocaService, sessionManager)
        {
        }

        public PartialViewResult List()
        {
            return PartialView();
        }

        public JsonResult GetAll(string description, int skip, int take)
        {
            var matFilter = new EiMaterialFilter()
            {
                Description = description
            };
            var materials = RocaService.MaterialService.GetEiMaterialsPaged(matFilter, skip, take);
            var count = RocaService.MaterialService.GetEiMaterialsCount(matFilter);
            var result = new { Count = count, Materials = materials };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllForMl(int mlId, string description, int skip, int take)
        {
            var matFilter = new EiMaterialFilter()
            {
                Description = description
            };
            var materials = RocaService.MaterialService.GetEiMaterialsForMlPaged(mlId, matFilter, skip, take);
            var count = RocaService.MaterialService.GetEiMaterialsForMlCount(mlId, matFilter);
            var result = new { Count = count, Materials = materials };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
