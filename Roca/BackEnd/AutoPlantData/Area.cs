using System;
using System.Runtime.Serialization;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.AutoPlant.Data
{
    /// <summary>
    /// (tabla AREA_3D)
    /// </summary>
    [Serializable]
    [DataContract]
    public class Area : Entity<AreaPk>
    {
        public Area()
        {
            Id = new AreaPk();

        }

        public string AreaId
        {
            get { return Id.AreaId; }
            set { Id.AreaId = value; }
        }

        public string ProjectId
        {
            get { return Id.ProjectId; }
            set { Id.ProjectId = value; }
        }

        [DataMember]
        public string Name { get; set; }


    }
}
