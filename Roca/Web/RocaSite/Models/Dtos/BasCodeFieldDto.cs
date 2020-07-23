using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.Web.RocaSite.Models.Dtos
{
    public class BasCodeFieldDto : Dto<BasCodeField, BasCodeFieldDto>
    {
        public int Id { get; set; }
        public int ElementId { get; set; }
        public int FieldDefinitionId { get; set; }
        public int BasCodeId { get; set; }

        public BasCodeFieldDto(BasCodeField entity) : base(entity)
        {
            
        }

        public BasCodeFieldDto()
        {
            
        }
    }
}