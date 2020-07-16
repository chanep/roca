using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.Web.RocaSite.Infrastructure;
using Cno.Roca.Web.RocaSite.Models;
using Cno.Roca.Web.RocaSite.Models.Dtos;

namespace Cno.Roca.Web.RocaSite.Controllers
{
    public class ProjectController : BaseController
    {
        //
        // GET: /Project/

        public ProjectController(IRocaService rocaService, ISessionManager sessionManager) : base(rocaService, sessionManager)
        {
        }

        public ActionResult Selection()
        {
            return PartialView();
        }


        public JsonResult GetProjects()
        {
            var current = SessionManager.GetSessionContext().Project;
            var all = RocaService.CommonService.GetAllRootProjects().ToList();
            var result = new {current, all};
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll()
        {
            var projects = RocaService.CommonService.GetAllRootProjects().ToList();
            var dtos = ProjectDto.CreateList(projects);
            return Json(dtos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void SelectProject(int id)
        {
            var project = RocaService.CommonService.GetProject(id);
            var sessionContext = SessionManager.GetSessionContext();
            sessionContext.Project = project;
            SessionManager.SaveSessionContext(sessionContext);
        }
    }
}
