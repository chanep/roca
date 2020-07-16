using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Repositories;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public abstract class BaseService
    {
        protected IRocaUow RocaUow { get; set; }

        protected BaseService(IRocaUow rocaUow)
        {
            RocaUow = rocaUow;
        }
    }
}
