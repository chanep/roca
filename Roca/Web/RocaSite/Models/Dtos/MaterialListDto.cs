using System;
using System.Collections.Generic;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.Web.RocaSite.Models.Dtos
{
    
    public class MaterialListDto : Dto<MaterialList, MaterialListDto>
    {

        public int Id { get; set; }
        public string DocNumber { get; set; }
        public string Title { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int SpecialtyId { get; set; }
        public int? PreviousRevisionMlId { get; set; }
        public string SpecialtyAbbreviation { get; set; }
        public string SpecialtyName { get; set; }
        public string Revision { get; set; }
        public string Purpose { get; set; }
        public MaterialListStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int CreatorId { get; set; }
        public string CreatorShowName { get; set; }
        public int? UpdaterId { get; set; }
        public string UpdaterShowName { get; set; }
        public string RevisorShowName { get; set; }
        public int? RevisorId { get; set; }
        public string ApproverShowName { get; set; }
        public int? ApproverId { get; set; }
        public ICollection<MlItemDto> Items  { get; set; }

        public string StatusDescription
        {
            get
            {
                switch (Status)
                {
                        case MaterialListStatus.Elaboration:
                        return "En Elaboracion";
                        case MaterialListStatus.Issued:
                        return "Emitida";
                        case MaterialListStatus.Superseded:
                        return "Superada";
                }
                return null;
            }
        }

        public MaterialListDto()
        {         
            Items = new List<MlItemDto>();
        }

        public MaterialListDto(MaterialList ml):base(ml)
        {
            Items = new List<MlItemDto>();
            foreach (var item in ml.Items)
            {
                Items.Add(new MlItemDto(item));
            }
        }

    }
}