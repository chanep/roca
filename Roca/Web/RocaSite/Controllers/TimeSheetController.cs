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
    public class TimeSheetController : BaseController
    {
        public TimeSheetController(IRocaService rocaService, ISessionManager sessionManager) : base(rocaService, sessionManager)
        {
        }

        [CustomAuthorize(Roles = Roles.Write)]
        public ActionResult Details()
        {
            return PartialView();
        }

        public ActionResult List()
        {
            return PartialView();
        }

        [CustomAuthorize(Roles = Roles.Admin)]
        public ActionResult Reports()
        {
            return PartialView();
        }

        [CustomAuthorize(Roles = Roles.Admin)]
        public ActionResult Defaulters()
        {
            return PartialView();
        }

        [CustomAuthorize(Roles = Roles.Admin)]
        public ActionResult Status()
        {
            return PartialView();
        }

        public JsonResult GetFull(int id)
        {
            var ts = RocaService.TimeSheetService.GetFull(id);
            var dto = new TimeSheetDto(ts);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByDate(DateTime controlDate, int? specialtyId)
        {
            if (specialtyId == null || specialtyId.Value == 0)
                specialtyId = LoggedUser.Specialties.First().Id;
            var specialty = RocaService.CommonService.GetSpecialty(specialtyId.Value);
            var ts = RocaService.TimeSheetService.GetFull(LoggedUser.Id, specialty.Id, controlDate);
            TimeSheetDto dto = null;
            if (ts != null)
                dto = new TimeSheetDto(ts);
            else
                dto = CreateEmptyTimeSheet(LoggedUser, specialty, controlDate);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLast(int specialtyId)
        {
            var user = SessionManager.GetLoggedtUser();
            var ts = RocaService.TimeSheetService.GetLast(user.Id, specialtyId);
            TimeSheetDto dto = null;
            if (ts != null)
                dto = new TimeSheetDto(ts);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll(TimeSheetFilter filter)
        {
            var list = RocaService.TimeSheetService.GetAll(filter).OrderByDescending(t => t.ControlDate).ThenBy(t=> t.User.FullName);
            var dtoList = TimeSheetDto.CreateList(list);
            return Json(dtoList, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetDetailsOptions()
        {
            var leaders = RocaService.CommonService.GetUsersByRole(Roles.Leader);
            var projects = RocaService.CommonService.GetAllRootProjects();
            var projDtos = ProjectDto.CreateList(projects);
            var docTypes = RocaService.CommonService.GetAllLookUpsByType(LookUpTypes.DocType);
            var tasks = RocaService.CommonService.GetAllLookUpsByType(LookUpTypes.TsTask);
            var specialties = SessionManager.GetLoggedtUser().Specialties;
            var options = new { Projects = projDtos, DocTypes = docTypes, Tasks = tasks, Leaders = leaders, Specialties = specialties };
            return Json(options, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListOptions()
        {
            IEnumerable<User> users = null;
            IEnumerable<Specialty> specialties = null;
            var user = SessionManager.GetLoggedtUser();
            if (user.IsAdmin())
            {
                users = RocaService.CommonService.GetAllUsers();
                specialties = RocaService.CommonService.GetAllSpecialties();
            }
            else
            {
                users = new List<User>() {user};
                specialties = SessionManager.GetLoggedtUser().Specialties;
            }
            var options = new { Users = users, Specialties = specialties };
            return Json(options, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetAutosuggestDoc(string property, string term, string filtersStr)
        {
            var filter = JsonConvert.DeserializeObject<DocumentFilter>(filtersStr);
            var list = RocaService.CommonService.GetDocuments(filter);

            var suggestList = list.Select(d => new { value = GetPropertyString(d, property) })
                                  .Distinct()
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

        public JsonResult GetDefaulters()
        {
            var toDate = DateTime.Now.Date;
            var fromDate = toDate.AddDays(-6);
            var defaulters = RocaService.TimeSheetService.GetDefaulters(fromDate, toDate);
            var usersLastTimeSheet = RocaService.TimeSheetService.GetAllUserLast().ToList();
            var list = new List<object>();
            foreach (var user in defaulters)
            {
                var lastTimeSheet = usersLastTimeSheet.FirstOrDefault(t => t.UserId == user.Id);
                DateTime? lastLoad = null;
                if (lastTimeSheet != null)
                    lastLoad = lastTimeSheet.ControlDate;
                list.Add(new{User = user, LastLoad = lastLoad});

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByDocReport(TimeSheetItemFilter filter)
        {
            var items = RocaService.TimeSheetService.GetAllDocItems(filter).ToList();
            var report = items.GroupBy(i => i.Document).Select(g => new { Document = new DocumentDto(g.Key), Hours = g.Sum(t => t.Hours) });
            return Json(report, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByTaskReport(TimeSheetItemFilter filter)
        {
            var items = RocaService.TimeSheetService.GetAllTaskItems(filter).ToList();
            var report = items.GroupBy(i => new{ i.Subproject, i.Task}).Select(g => new { Subproject = new ProjectDto(g.Key.Subproject), g.Key.Task , Hours = g.Sum(t => t.Hours) });
            return Json(report, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBySpecialtyReport(TimeSheetItemFilter filter)
        {
            var items = RocaService.TimeSheetService.GetAllItems(filter).ToList();
            var report = items.GroupBy(i => new { i.Subproject, i.TimeSheet.Specialty }).Select(g => new { Subproject = new ProjectDto(g.Key.Subproject), g.Key.Specialty, Hours = g.Sum(t => t.Hours) });
            return Json(report, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByProjectReport(TimeSheetItemFilter filter)
        {
            var items = RocaService.TimeSheetService.GetAllItems(filter).ToList();
            var report = items.GroupBy(i => i.Subproject).Select(g => new { Subproject = new ProjectDto(g.Key), Hours = g.Sum(t => t.Hours) });
            return Json(report, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize(Roles=Roles.Write)]
        public JsonResult Save(TimeSheetDto timeSheet)
        {
            if(LoggedUser.Id != timeSheet.UserId)
                throw new RocaException("No se puede salvar una timesheet de otro usuario");
            var ts = timeSheet.GetEntity();
            ts = RocaService.TimeSheetService.Save(ts);
            ts = RocaService.TimeSheetService.GetFull(ts.Id);
            var dto = new TimeSheetDto(ts);
            return Json(dto);
        }

        [HttpPost]
        [CustomAuthorize(Roles = Roles.Admin)]
        public void Update(TimeSheetDto timeSheet)
        {
            var ts = timeSheet.GetEntity();
            ts.Items.Clear();
            RocaService.TimeSheetService.Update(ts);
        }

        [HttpPost]
        [CustomAuthorize(Roles = Roles.Admin)]
        public void UpdateAll(IEnumerable<TimeSheetDto> timeSheets)
        {
            var entities = new List<TimeSheet>();
            foreach (var timeSheetDto in timeSheets)
            {
                timeSheetDto.Items.Clear();
                entities.Add(timeSheetDto.GetEntity());
            }
            RocaService.TimeSheetService.UpdateAll(entities);
        }


        private TimeSheetDto CreateEmptyTimeSheet(User user, Specialty specialty, DateTime controlDate)
        {
            var ts = new TimeSheetDto()
            {
                UserId = user.Id,
                SpecialtyId = specialty.Id,
                ControlDate = TimeSheet.GetNextFriday(controlDate),
                UserFullName = user.FullName,
                SpecialtyAbbreviation = specialty.Abbreviation,
                SpecialtyName = specialty.Name,
                Status = (int)TimeSheetStatus.Open               
            };
            return ts;
        }

        private string GetPropertyString(Document doc, string property)
        {
            var value = doc.GetType().GetProperty(property).GetValue(doc, null);
            if (value == null)
                return "";
            return value.ToString();
        }

    }
}
