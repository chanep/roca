using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using System.Web.Routing;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.Web.RocaSite.Infrastructure;
using Cno.Roca.Web.RocaSite.Models;
using Cno.Roca.Web.RocaSite.Models.Dtos;

namespace Cno.Roca.Web.RocaSite.Controllers
{
    public class MaterialListController : BaseController
    {
        public MaterialListController(IRocaService rocaService, ISessionManager sessionManager)
            : base(rocaService, sessionManager)
        {
        }

        #region MaterialList Actions

        public ActionResult List()
        {
            return PartialView();
        }

        public ActionResult RevisionHistory()
        {
            return PartialView("List");
        }

        public ActionResult View(int id)
        {
            return PartialView();
        }

        public ActionResult New()
        {
            return PartialView("Edit");
        }

        public ActionResult Edit()
        {
            return PartialView();
        }

        public ActionResult ActionsPanel()
        {
            return PartialView();
        }

        public ActionResult Header()
        {
            return PartialView();
        }

        public JsonResult GetAllHeadRevision()
        {
            var projId = SessionManager.GetSessionContext().Project.Id;
            var materialLists = RocaService.MaterialListService.GetAllHeadRevision(projId).OrderByDescending(ml => ml.Id);
            var mlsDto = MaterialListDto.CreateList(materialLists);
            return Json(mlsDto, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRevisionHistory(int id)
        {
            var materialLists = RocaService.MaterialListService.GetHistory(id);
            var mlsDto = MaterialListDto.CreateList(materialLists);
            return Json(mlsDto, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get(int id)
        {
            var ml = RocaService.MaterialListService.Get(id);
            var mlDto = new MaterialListDto(ml);
            return Json(mlDto, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFull(int id)
        {
            var ml = RocaService.MaterialListService.GetFull(id);
            var mlDto = new MaterialListDto(ml);
            return Json(mlDto, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVm(int id)
        {
            var ml = RocaService.MaterialListService.GetFull(id);
            var mlDto = new MaterialListDto(ml);
            var mlVm = CreateDetailsVm(mlDto);
            return Json(mlVm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNewVm()
        {
            var user = SessionManager.GetLoggedtUser();
            var mlDto = new MaterialListDto()
            {
                CreatorId = user.Id,
                CreatorShowName = user.ShowName,
                ProjectId = SessionManager.GetSessionContext().Project.Id,
                ProjectName = SessionManager.GetSessionContext().Project.Name,
                Status = MaterialListStatus.Elaboration
            };

            var mlVm = CreateDetailsVm(mlDto);

            return Json(mlVm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(MaterialList ml)
        {

            ml.CreatedOn = DateTime.Now;
            ml.CreatorId = SessionManager.GetLoggedtUser().Id;
            RocaService.MaterialListService.Add(ml);
            var mlDto = new MaterialListDto(ml);
            return Json(mlDto, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Save(MaterialList ml)
        {
            ml.UpdatedOn = DateTime.Now;
            ml.UpdaterId = SessionManager.GetLoggedtUser().Id;
            RocaService.MaterialListService.Update(ml);
        }

        [HttpPost]
        public void Issue(int id)
        {
            RocaService.MaterialListService.Issue(id);
        }

        [HttpPost]
        public void Delete(int id)
        {
            RocaService.MaterialListService.Delete(id);
        }

        [HttpPost]
        public JsonResult NewRevision(int prevRevisionId, string newRevision)
        {
            var prevRev = RocaService.MaterialListService.Get(prevRevisionId);
            var newRev = new MaterialList()
            {
                DocNumber = prevRev.DocNumber,
                Title = prevRev.Title,
                SpecialtyId = prevRev.SpecialtyId,
                Revision = newRevision,
                PreviousRevisionMlId = prevRevisionId,
                CreatorId = SessionManager.GetLoggedtUser().Id,
                CreatedOn = DateTime.Now,
                RevisorId = prevRev.RevisorId,
                ApproverId = prevRev.ApproverId,
                ProjectId = prevRev.ProjectId,
                Status = MaterialListStatus.Elaboration
            };
            RocaService.MaterialListService.CreateNewRevision(newRev);
            var mlDto = new MaterialListDto(newRev);
            return Json(mlDto, JsonRequestBehavior.AllowGet);
        }

        private MlDetailsVm CreateDetailsVm(MaterialListDto ml)
        {
            return new MlDetailsVm()
            {
                MaterialList = ml,
                Users = RocaService.CommonService.GetAllUsers(),
                Specialties = RocaService.CommonService.GetAllSpecialties(),
                Purposes = RocaService.CommonService.GetAllLookUpsByType(LookUpTypes.MlPurpose).Select(l => l.Value)
            };
        }

        #endregion


        #region Items Actions

        public ActionResult ItemList()
        {
            return PartialView();
        }

        public ActionResult EditItemList()
        {
            return PartialView("ItemList");
        }

        public JsonResult GetAllItems(int id)
        {
            var items = RocaService.MaterialListService.GetAllItems(id);
            var itemsDto = MlItemDto.CreateList(items);
            return Json(itemsDto, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddItem(int mlId, int materialId, double quantity = 0)
        {
            var item = new MlItem()
            {
                MaterialId = materialId,
                Quantity = quantity,
                MlId = mlId
            };
            RocaService.MaterialListService.AddItem(item);
            var itemDto = new MlItemDto(item);

            return Json(itemDto);
        }

        [HttpPost]
        public void UpdateItem(MlItemDto item)
        {
            var mlItem = item.GetEntityDeep();
            RocaService.MaterialListService.UpdateItem(mlItem); 
        }

        [HttpPost]
        public void DeleteItem(int id)
        {
            RocaService.MaterialListService.DeleteItem(id);         
        }


        #endregion

    }
}
