namespace Cno.Roca.BackEnd.Materials.BL.Filters
{
    public class DocumentFilter
    {
        public int? ProjectId { get; set; }
        public int? TypeId { get; set; }
        public int? SpecialtyId { get; set; }
        public string DocNumber { get; set; }
        public string Title { get; set; }
    }
}
