using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cno.Roca.BackEnd.Materials.Data;

namespace Cno.Roca.Web.RocaSite.Models.Dtos
{
    public class ProjectDto : Dto<Project, ProjectDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string SubprojectType { get; set; }
        public int? ParentId { get; set; }
        public ICollection<ProjectDto> Subprojects { get; set; }
        public string ParentName { get; set; }
        public string ParentCode { get; set; }
        public string ParentShortName { get; set; }

        public ProjectDto()
        {
            Subprojects = new List<ProjectDto>();
        }

        public ProjectDto(Project entity) : base(entity)
        {
            Subprojects = new List<ProjectDto>();
            foreach (var subproject in entity.Subprojects)
            {
                Subprojects.Add(new ProjectDto(subproject));
            }
        }

        public new static IList<ProjectDto> CreateList(IEnumerable<Project> entities)
        {
            var list = new List<ProjectDto>();
            foreach (var e in entities)
            {
                var dto = new ProjectDto(e);
                list.Add(dto);
            }
            return list;
        }
    }
}