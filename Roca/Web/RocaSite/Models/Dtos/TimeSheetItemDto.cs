using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cno.Roca.BackEnd.AutoPlant.Data;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;

namespace Cno.Roca.Web.RocaSite.Models.Dtos
{
    public class TimeSheetItemDto : Dto<TimeSheetItem, TimeSheetItemDto>
    {
        public int Id { get; set; }
        public int TimeSheetId { get; set; }
        public int SubprojectId { get; set; }
        public int SubprojectParentId{ get; set; }
        public int? DocumentId { get; set; }
        public int? TaskId { get; set; }
        public int Hours { get; set; }
        public int TimeSheetSpecialtyId { get; set; }
        public int TimeSheetSpecialtyName { get; set; }

        public string TaskValue { get; set; }

        public ProjectDto Subproject { get; set; }
        public DocumentDto Document { get; set; }

        

        public TimeSheetItemDto(TimeSheetItem entity):base(entity)
        {
            if (entity.Subproject != null)
            {
                Subproject = new ProjectDto(entity.Subproject);
                if (entity.Subproject.ParentId != null) 
                    SubprojectParentId = entity.Subproject.ParentId.Value;
            }
            if (entity.Document != null)
                Document = new DocumentDto(entity.Document);
        }

        public TimeSheetItemDto()
        {
        }
    }
}