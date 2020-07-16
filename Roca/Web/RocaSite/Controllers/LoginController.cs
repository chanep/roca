using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Cno.Roca.Web.RocaSite.Infrastructure;
using Cno.Roca.Web.RocaSite.Models;

namespace Cno.Roca.Web.RocaSite.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(IRocaService rocaService, ISessionManager sessionManager) : base(rocaService, sessionManager)
        {
        }

        public ActionResult Home()
        {
            var user = SessionManager.GetLoggedtUser();
            return View(user);
        }

        public ActionResult Welcome()
        {
            var user = SessionManager.GetLoggedtUser();
            return PartialView(user);
        }

        [CustomAuthorize(Roles=Roles.SuperAdmin)]
        public ActionResult Impersonate()
        {
            return PartialView();
        }

        public ActionResult Unauthorized()
        {
            return PartialView();
        }

        public string GetLongUserName()
        {
            return SessionManager.GetLoggedtUser().LongUserName;
        }

        public JsonResult GetLoggedUser()
        {
            var user = SessionManager.GetLoggedtUser();
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllUsers()
        {
            var users = RocaService.CommonService.GetAllUsers();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize(Roles = Roles.SuperAdmin)]
        public void ImpersonateUser(int id)
        {
            var user = RocaService.CommonService.GetUser(id);
            SessionManager.ImpersonateUser(user);
        }


    }
}
