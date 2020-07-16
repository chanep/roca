using System;
using System.Collections.Generic;
using System.ComponentModel;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public partial class MaterialList : Entity<int>
    {
        public MaterialList()
        {
            this.Items = new List<MlItem>();
            Status = MaterialListStatus.Elaboration;
        }

        public string DocNumber { get; set; }
        public string Title { get; set; }
        public int SpecialtyId { get; set; }
        public string Revision { get; set; }
        public int? PreviousRevisionMlId { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdaterId { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? RevisorId { get; set; }
        public int? ApproverId { get; set; }
        public int ProjectId { get; set; }
        public string Purpose { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int StatusInt { get; set; }
        public MaterialListStatus Status
        {
            get { return (MaterialListStatus)StatusInt; }
            set { StatusInt = (int)value; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)] 
        public int DeletedInt { get; set; }
        public bool Deleted
        {
            get { return DeletedInt != 0; }
            set {DeletedInt = value ? 1 : 0;}
        }

        public virtual Project Project { get; set; }
        public virtual Specialty Specialty { get; set; }
        public virtual MaterialList PreviousRevisionMl { get; set; }
        public virtual User Creator { get; set; }
        public virtual User Updater { get; set; }
        public virtual User Revisor { get; set; }
        public virtual User Approver { get; set; }
        public virtual ICollection<MlItem> Items { get; set; }

        public new MaterialList Clone()
        {
            var ml = (MaterialList)base.Clone();
            return ml;
        }

        public void Validate()
        {
            if (DocNumber == null)
                throw new RocaException("El DocNumber de MaterialList no esta definido");
            if (Title == null)
                throw new RocaException("El Title de MaterialList no esta definido");
            if (Revision == null)
                throw new RocaException("La Revision de MaterialList no esta definida");
            if (ProjectId == 0)
                throw new RocaException("El Proyecto de MaterialList no esta definido");
        }

    }
}
