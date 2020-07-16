using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clutch.Diagnostics.EntityFramework;
using Cno.Roca.BackEnd.Materials.BL;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.EfDal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cno.Roca.BackEnd.Tests.Materials
{
    [TestClass]
    public abstract class BaseTest
    {
        private TestHelper _helper;
        protected TestHelper Helper { get { return _helper; } }

        [TestInitialize]
        public virtual void Initialize()
        {
            _helper = new TestHelper();
            DbTracing.Enable();
            DbTracing.AddListener(new EfListener());
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            if(_helper != null)
                _helper.Dispose();
        }

        protected virtual IRocaUow CreateRocaUow()
        {
            return new RocaUow();
        }

        protected virtual IRocaService CreateRocaService(IRocaUow rocaUow)
        {
            return new RocaService(rocaUow);
        }
    }
}
