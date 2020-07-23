using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cno.Roca.BackEnd.AutoPlant.BL;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.Web.RocaSite.Infrastructure;
using Cno.Roca.Web.RocaSite.Models.Dtos;
using Newtonsoft.Json;

namespace Cno.Roca.Web.RocaSite.Controllers
{
    public class MatPipingController : BaseController
    {
        IMatPipingMigrator Migrator { get; set; }

        public MatPipingController(IMatPipingMigrator migrator, IRocaService rocaService, ISessionManager sessionManager) : base(rocaService, sessionManager)
        {
            Migrator = migrator;
        }

        public ActionResult Import()
        {
            return PartialView();
        }

        public ActionResult LmReport()
        {
            return PartialView();
        }

        public int ImportFromAp(string projectId)
        {
            return Migrator.MigrateMaterials(projectId);
        }

        public JsonResult GetAutosuggest(string property, string term, string filterStr)
        {
            var filter = JsonConvert.DeserializeObject<MatPipingFilter>(filterStr);
            var list = RocaService.MatPipingService.GetAll(filter);

            var suggestList = list.Select(m => new { value = GetPropertyString(m, property) })
                                  .Distinct()
                                  .Take(50)
                                  .ToArray();
            return Json(suggestList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll(MatPipingFilter filter)
        {
            var mats = RocaService.MatPipingService.GetAll(filter).ToList();
            var commCodes = mats.Select(m => m.CommodityCode).Distinct().ToList();
            if (commCodes.Count > 1)
                return null;
            var dtos = new MatPipingFamilyDto(mats);
            return Json(dtos, JsonRequestBehavior.AllowGet);
        }

    }
}
