using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cno.Roca.Web.RocaSite.Models;

namespace Cno.Roca.Web.RocaSite.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult DetailedError()
        {
            var error = (ErrorVm)TempData["model"];
            return PartialView(error);
        }

        public ActionResult SimpleError()
        {
            var error = (ErrorVm)TempData["model"];
            return PartialView(error);
        }

    }
}
