using Cno.Roca.BackEnd.AutoPlant.Data;

namespace Cno.Roca.Web.RocaSite.Models
{
    public class FiltersViewModel
    {
        public string ProjectId { get; set; }
        public string AreaId { get; set; }
        public string Service { get; set; }
        public string Line { get; set; }
        public string Tag { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string NominalDiam { get; set; }
        public string Rating { get; set; }
        public string Schedule { get; set; }
        public string PieceMark { get; set; }
        public string Spool { get; set; }
        public string Order { get; set; }
        public MaterialOptionalFields OptionalFields { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}