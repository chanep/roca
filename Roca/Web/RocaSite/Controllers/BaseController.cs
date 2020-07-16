﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cno.Roca.BackEnd.Materials.BL;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Cno.Roca.Web.RocaSite.Infrastructure;

namespace Cno.Roca.Web.RocaSite.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IRocaService RocaService { get; set; }
        protected ISessionManager SessionManager { get; set; }
        protected User LoggedUser { get; set; }

        protected BaseController(IRocaService rocaService, ISessionManager sessionManager)
        {
            RocaService = rocaService;
            SessionManager = sessionManager;
            LoggedUser = sessionManager.GetLoggedtUser();
        }

    }
}
