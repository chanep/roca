using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.AutoPlant.Data
{
    /// <summary>
    /// Representa la clave primaria de la entidad Area
    /// </summary>
    [Serializable]
    [DataContract]
    public class AreaPk : PrimaryKey
    {
        [DataMember]
        public string AreaId { get; set; }

        [DataMember]
        public string ProjectId { get; set; }



        public AreaPk()
        {
        }

        public AreaPk(string areaId, string projectId)
        {
            AreaId = areaId;
            ProjectId = projectId;
        }

    }
}
