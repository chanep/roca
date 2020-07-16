using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;

namespace Cno.Roca.Web.RocaSite.Models.Dtos
{
    public class TimeSheetDto : Dto<TimeSheet, TimeSheetDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? LeaderId { get; set; }
        public int SpecialtyId { get; set; }
        public DateTime ControlDate { get; set; }
        public int Status { get; set; }

        public string UserFullName { get; set; }
        public string LeaderFullName { get; set; }
        public string SpecialtyAbbreviation { get; set; }
        public string SpecialtyName { get; set; }


        public ICollection<TimeSheetItemDto> Items { get; set; }

        public TimeSheetDto()
        {
            Items = new List<TimeSheetItemDto>();
        }

        public TimeSheetDto(TimeSheet entity): base(entity)
        {
            Items = new List<TimeSheetItemDto>();
            foreach (var item in entity.Items)
            {
                Items.Add(new TimeSheetItemDto(item));
            }
        }

        public override TimeSheet GetEntity()
        {
            var entity = base.GetEntity();
            foreach (var item in Items)
            {
                entity.Items.Add(item.GetEntity());
            }
            return entity;
        }

        public override TimeSheet GetEntityDeep()
        {
            var entity = base.GetEntityDeep();
            foreach (var item in Items)
            {
                entity.Items.Add(item.GetEntityDeep());
            }
            return entity;
        }
    }
}