using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Cno.Roca.BackEnd.AutoPlant.BL;
using Cno.Roca.BackEnd.AutoPlant.Data;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Cno.Roca.CoreData;
using Cno.Roca.Web.RocaSite.Infrastructure;
using Cno.Roca.Web.RocaSite.Models;
using Newtonsoft.Json;

namespace Cno.Roca.Web.RocaSite.Controllers
{
    public class AutoplantController : Controller
    {
        private readonly IModelRepository _materialRepository;

        public AutoplantController(IModelRepository materialRepository)
        {
            _materialRepository = materialRepository;

        }

        //[CustomAuthorize(Roles = Roles.Read )]
        public ActionResult List()
        {
            return PartialView();
        }

        public JsonResult GetProjects()
        {
            var projects = _materialRepository.GetProjects().ToArray();
            return Json(projects, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAreas(string projId)
        {
            var areas = _materialRepository.GetAreas(projId).ToList();
            areas.Add(new Area { Id = new AreaPk { AreaId = "*", ProjectId = projId }, Name = "Todas" });
            var result = areas.Select(a => new { Id = a.AreaId, Name = a.Name }).ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult GetMaterials(FiltersViewModel filters)
        {
            var mats = GetMaterialsInternal(filters);
            var count = mats.Count();
            if (filters.Take > 0)
                mats = mats.Skip(filters.Skip).Take(filters.Take);
            var result = new {Count = count, Materials = mats};
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }




        public JsonResult GetAutosuggest(string property, string term, string filtersStr)
        {
            //dynamic filter = JObject.Parse(filterValues);
            var filters = JsonConvert.DeserializeObject<FiltersViewModel>(filtersStr);

            IEnumerable<MaterialPiping> list = GetMaterialsInternal(filters);

            var suggestList = list.Select(m => new {value = GetPropertyString(m, property)})
                                  .Distinct()
                                  .Take(50)
                                  .ToArray();
            return Json(suggestList, JsonRequestBehavior.AllowGet);
        }

        private string GetPropertyString(MaterialPiping mat, string property)
        {
            var value = mat.GetType().GetProperty(property).GetValue(mat, null);
            if (value == null)
                return "";
            return value.ToString();
        }


        private IEnumerable<MaterialPiping> GetMaterialsInternal(FiltersViewModel filters)
        {
            var order = MaterialPipingOrder.Line;
            if (filters.Order == "material")
                order = MaterialPipingOrder.SortOrder;

            IEnumerable<MaterialPiping> materials = _materialRepository.GetMaterials(filters.ProjectId,
                                                                                     filters.AreaId,
                                                                                     filters.OptionalFields,
                                                                                     order);

            if (!String.IsNullOrEmpty(filters.Service))
                materials = materials.Where(m => m.Service != null && m.Service.ContainsCaseInsensitive(filters.Service));
            if (!String.IsNullOrEmpty(filters.Line))
                materials = materials.Where(m => m.Line != null && m.Line.ContainsCaseInsensitive(filters.Line));
            if (!String.IsNullOrEmpty(filters.Tag))
                materials = materials.Where(m => m.Tag != null && m.Tag.ContainsCaseInsensitive(filters.Tag));
            if (!String.IsNullOrEmpty(filters.ShortDescription))
                materials =
                    materials.Where(m => m.ShortDescription != null && m.ShortDescription.ContainsCaseInsensitive(filters.ShortDescription));
            if (!String.IsNullOrEmpty(filters.LongDescription))
                materials =
                    materials.Where(m => m.LongDescription != null && m.LongDescription.ContainsCaseInsensitive(filters.LongDescription));

            if (!String.IsNullOrEmpty(filters.NominalDiam))
                materials = materials.Where(m => m.NominalDiam != null && m.NominalDiam.ContainsCaseInsensitive(filters.NominalDiam));
            if (!String.IsNullOrEmpty(filters.Rating))
                materials = materials.Where(m => m.Rating != null && m.Rating.ContainsCaseInsensitive(filters.Rating));
            if (!String.IsNullOrEmpty(filters.Schedule))
                materials = materials.Where(m => m.Schedule != null && m.Schedule.ContainsCaseInsensitive(filters.Schedule));
            if (!String.IsNullOrEmpty(filters.PieceMark))
                materials = materials.Where(m => m.PieceMark != null && m.PieceMark.ContainsCaseInsensitive(filters.PieceMark));
            if (!String.IsNullOrEmpty(filters.Spool))
                materials = materials.Where(m => m.Spool != null && m.Spool.ContainsCaseInsensitive(filters.Spool));

            return materials;
        }


    }
}
    