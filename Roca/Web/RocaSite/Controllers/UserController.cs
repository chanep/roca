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
    public class UserController : BaseController
    {
        private IAuthProvider AuthProvider { get; set; }
        public UserController(IRocaService rocaService, ISessionManager sessionManager, IAuthProvider authProvider) : base(rocaService, sessionManager)
        {
            AuthProvider = authProvider;
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Login()
        {
            return PartialView();
        }

        public ActionResult Welcome()
        {
            return PartialView();
        }

        public ActionResult Menu()
        {
            return PartialView();
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
            return SessionManager.GetCurrentUser().LongUserName;
        }

        public JsonResult GetCurrentUser()
        {
            var user = SessionManager.GetCurrentUser();
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllUsers()
        {
            var users = RocaService.CommonService.GetAllUsers();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsFormsMode()
        {
            var formsMode = AuthProvider.FormsMode;
            var result = new { Result = formsMode };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Authenticate(string username, string password)
        {
            var authenticated = AuthProvider.Authenticate(username, password);
            var result = new {Result = authenticated};
            return  Json(result);
        }

        [HttpPost]
        [CustomAuthorize(Roles = Roles.SuperAdmin)]
        public void ImpersonateUser(int id)
        {
            var user = RocaService.CommonService.GetUser(id);
            SessionManager.SetCurrentUser(user);
        }


    }
}
