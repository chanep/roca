using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class SisepcElement
    {
        public string Code { get; set; }
        public string Dim1 { get; set; }
        public string Unit1 { get; set; }
        public string Dim2 { get; set; }
        public string Unit2 { get; set; }
        public string Dim3 { get; set; }
        public string Unit3 { get; set; }
        public string PurchaseUnit { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
    }
}
