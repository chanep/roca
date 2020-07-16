namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public partial class EiMaterial : Material
    {
        public string Power { get; set; }
        public virtual EiMaterialDetails Details { get; set; }
    }
}
