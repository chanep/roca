using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Clutch.Diagnostics.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.Web.RocaSite.Tests
{
    [TestClass]
    public abstract class BaseTest
    {

        [TestInitialize]
        public virtual void Initialize()
        {
            DbTracing.Enable();
            DbTracing.AddListener(new EfListener());
        }

    }
}
