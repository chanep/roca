using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cno.Roca.Web.RocaSite.Models.Dtos;

namespace Cno.Roca.Web.RocaSite.Models
{
    public class MaterialVm
    {
        /// <summary>
        /// La lista de materiales a la que se le van a agregar los materiales seleccionados
        /// </summary>
        public MaterialListDto MaterialList { get; set; }
    }
}