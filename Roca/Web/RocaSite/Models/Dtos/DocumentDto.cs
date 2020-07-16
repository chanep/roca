using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;

namespace Cno.Roca.Web.RocaSite.Models.Dtos
{
    public class DocumentDto : Dto<Document, DocumentDto>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int SpecialtyId { get; set; }
        public int TypeId { get; set; }
        public string DocNumber { get; set; }
        public string Title { get; set; }

        public string SpecialtyAbbreviation { get; set; }
        public string SpecialtyName { get; set; }
        public string TypeCode { get; set; }
        public string TypeValue { get; set; }

        public ProjectDto Project { get; set; }

        public DocumentDto(Document entity): base(entity)
        {
            if (entity.Project != null)
                Project = new ProjectDto(entity.Project);
        }

        public DocumentDto()
        {
        }
    }
}