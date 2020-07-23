using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.EfDal;

namespace Cno.Roca.BackEnd.Tests.Materials.Bas
{
    public class CodeConverter : IDisposable
    {
        private IRocaUow uow;
        private IBasService basService;

        public CodeConverter()
        {
            uow = new RocaUow();
            basService = new BasService(uow);
        }

        private List<List<string>> ReadCsv(string fileName)
        {
            var lines = new List<List<string>>();
            using (var rd = new StreamReader(fileName, Encoding.Default))
            {
                while (!rd.EndOfStream)
                {
                    var line = rd.ReadLine().Split(';');
                    for (int i = 0; i < line.Length; i++)
                    {
                        line[i] = EliminarComillas(line[i]).Trim();
                    }
                    lines.Add(line.ToList());                    
                }
            }
            return lines;
        }

        private void WriteCsv(string fileName, IEnumerable<List<string>> lines)
        {
            var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read);
            using (var file = new StreamWriter(fs, Encoding.Default))
            {
                foreach (var line in lines)
                {
                    for (int i = 0; i < line.Count; i++)
                    {
                        line[i] = AgregarComillas(line[i]);
                    }
                    var str = string.Join(";", line);
                    file.WriteLine(str);
                }
            }
        }

        private string EliminarComillas(string s)
        {
            if (s.Length >= 2 && s[0] == '"' && s[s.Length - 1] == '"')
            {
                s = s.Substring(1, s.Length - 2);
                s = s.Replace("\"\"", "\"");
            }
            return s;
        }

        private string AgregarComillas(string s)
        {
            string result = s;
            if (s.Contains("\""))
            {
                result = s.Replace("\"", "\"\"");
                result = "\"" + result + "\"";

            }
            return result;
        }


        private Dictionary<string, string> _pfFamilyTypesByDesc = new Dictionary<string, string>(); 
        private string GetPfFamilyTypeByDesc(string desc)
        {
            if (!_pfFamilyTypesByDesc.ContainsKey(desc))
            {
                var types = new[] { "p.pipe", "p.fitting", "p.flange", "p.gasket", "p.stud-bolt", "p.miscelaneo" };
                foreach (var type in types)
                {
                    string code = basService.GetCodeByDesc(type + ".desc", desc);
                    if (code != null)
                    {
                        _pfFamilyTypesByDesc.Add(desc, type);
                        break;
                    }
                }
            }
            return _pfFamilyTypesByDesc[desc];
        }

        private Dictionary<string, BasCode> _pfBasCodeByFamilyCode = new Dictionary<string, BasCode>(); 
        private BasCode GetDescBasCodeByFamilyCode(string familyCode)
        {
            var descCode = familyCode.Substring(0, 4);
            if (!_pfBasCodeByFamilyCode.ContainsKey(descCode))
            {
                var types = new[] { "p.pipe", "p.fitting", "p.flange", "p.gasket", "p.stud-bolt", "p.miscelaneo" };
                foreach (var type in types)
                {
                    var basCode = basService.GetAllCodesByField(type + ".desc").FirstOrDefault(b => b.Code == descCode);
                    if (basCode != null)
                    {
                        _pfBasCodeByFamilyCode.Add(descCode, basCode);
                        break;
                    }
                }
            }
            return _pfBasCodeByFamilyCode[descCode];
        }

        private string GetPfBasFamilyCode(string desc, string estandar, string serie1, string serie2, string material,
            string extremo1, string extremo2)
        {

            var family = GetPfFamilyTypeByDesc(desc);

            string descCode = basService.GetCodeByDesc(family + ".desc", desc);
            if (descCode == null)
                throw new RocaException(family + ".desc no encontrada: " + desc);

            string estandarCode = basService.GetCodeByDesc(family + ".estandar", estandar);
            if (estandarCode == null)
                throw new RocaException(family + ".estandar no encontrado: " + estandar);

            string serie1Code = basService.GetCodeByDesc(family + ".serie1", serie1);
            if (serie1Code == null)
                throw new RocaException(family + ".serie1 no encontrada: " + serie1);

            string serie2Code = basService.GetCodeByDesc(family + ".serie2", serie2);
            if (serie2Code == null)
                throw new RocaException(family + ".serie2 no encontrada: " + serie2);

            string materialCode = basService.GetCodeByDesc(family + ".material", material);
            if (materialCode == null)
                throw new RocaException(family + ".material no encontrada: " + material);

            string extremo1Code = basService.GetCodeByDesc(family + ".extremo1", extremo1);
            if (extremo1Code == null)
                throw new RocaException(family + ".extremo1 no encontrada: " + extremo1);

            string extremo2Code = basService.GetCodeByDesc(family + ".extremo2", extremo2);
            if (extremo2Code == null)
                throw new RocaException(family + ".extremo2 no encontrada: " + extremo2);

            return descCode + estandarCode + serie1Code + serie2Code + materialCode + extremo1Code + extremo2Code;

        }

        private string GetValveBasFamilyCode(string desc, string estandar, string serie, string material, string extremo,
                        string operacion, string trim, string sello, string observaciones)
        {

            string descCode = basService.GetCodeByDesc("p.valve.desc", desc);
                if (descCode == null)
                    throw new RocaException("Bas desc no encontrada: " + desc);

                string estandarCode = basService.GetCodeByDesc("p.valve.estandar", estandar);
                if (estandarCode == null)
                    throw new RocaException("Bas estandar no encontrado: " + estandar);

                string serieCode = basService.GetCodeByDesc("p.valve.serie", serie);
                if (serieCode == null)
                    throw new RocaException("Bas serie no encontrada: " + serie);

                string extremoCode = basService.GetCodeByDesc("p.valve.extremo", extremo);
                if (extremoCode == null)
                    throw new RocaException("Bas extremo no encontrada: " + extremo);

                string operacionCode = basService.GetCodeByDesc("p.valve.operacion", operacion);
                if (operacionCode == null)
                    throw new RocaException("Bas operacion no encontrada: " + operacion);

                string materialCode = basService.GetCodeByDesc("p.valve.material", material);
                if (materialCode == null)
                    throw new RocaException("Bas material no encontrada: " + material);

                string trimCode = basService.GetCodeByDesc("p.valve.trim", trim);
                if (trimCode == null)
                    throw new RocaException("Bas trim no encontrada: " + trim);

                string selloCode = basService.GetCodeByDesc("p.valve.sello", sello);
                if (selloCode == null)
                    throw new RocaException("Bas sello no encontrada: " + sello);

                string observacionesCode = basService.GetCodeByDesc("p.valve.observaciones", observaciones);
                if (observacionesCode == null)
                    throw new RocaException("Bas observaciones no encontrada: " + observaciones);

                return descCode + estandarCode + serieCode + extremoCode + operacionCode + materialCode + trimCode + selloCode + observacionesCode;

        }

        public string GetPfDimensionalCode(BasCode descBasCode, string dim1, string dim2, string sched1, string sched2)
        {
            var type = descBasCode.Field.Substring(0, descBasCode.Field.Length - 5);

            string dim1Code = basService.GetCodeByDesc(type + ".dim1", dim1);
            if (dim1Code == null)
                throw new RocaException("Bas dim1 no encontrado: " + dim1 + " del tipo " + type);

            string dim2Code = basService.GetCodeByDesc(type + ".dim2", dim2);
            if (dim2Code == null)
                throw new RocaException("Bas dim2 no encontrado: " + dim2 + " del tipo " + type);

            string sched1Code = basService.GetCodeByDesc(type + ".sched1", sched1);
            if (sched1Code == null)
                throw new RocaException("Bas sched1 no encontrado: " + sched1 + " del tipo " + type);

            string sched2Code = basService.GetCodeByDesc(type + ".sched2", sched2);
            if (sched2Code == null)
                throw new RocaException("Bas sched2 no encontrado: " + sched2 + " del tipo " + type);

            return dim1Code + dim2Code + sched1Code + sched2Code;

        }

        public string GetValveDimensionalCode(string diametro)
        {


            string diametroCode = basService.GetCodeByDesc("p.valve.diametro", diametro);
                if (diametroCode == null)
                    throw new RocaException("Bas diametro no encontrado: " + diametro);

                return diametroCode;

        }


        /// <summary>
        /// Genera mapping OEP Commodity Code -> BAS Family Code
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Diccionario (OEP Commodity Code, BAS Family Code)</returns>
        public Dictionary<string, string> GeneratePfFamilyCodeMap(List<List<string>> input)
        {
            var codeMap = new Dictionary<string, string>();



                for (int i = 1; i < input.Count; i++)
                {
                    var line = input[i];
                    string desc = line[1];
                    string estandar = line[2];
                    string serie1 = line[3];
                    string serie2 = line[4];
                    string material = line[5];
                    string extremo1 = line[6];
                    string extremo2 = line[7];

                    string basFamilyCode = GetPfBasFamilyCode(desc, estandar, serie1, serie2, material,
                        extremo1, extremo2);

                    codeMap.Add(line[0], basFamilyCode);
                }


            return codeMap;
        }

        /// <summary>
        /// Genera mapping OEP Commodity Code -> BAS Family Code
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Diccionario (OEP Commodity Code, BAS Family Code)</returns>
        public Dictionary<string, string> GenerateValveFamilyCodeMap(List<List<string>> input)
        {
            var codeMap = new Dictionary<string, string>();



                for (int i = 1; i < input.Count; i++)
                {
                    var line = input[i];
                    string desc = line[1];
                    string estandar = line[2];
                    string serie = line[3];
                    string extremo = line[4];
                    string operacion = line[5];
                    string material = line[6];
                    string trim = line[7];
                    string sello = line[8];
                    string observaciones = line[9];

                    string basFamilyCode = GetValveBasFamilyCode(desc, estandar, serie, material, extremo, operacion,
                        trim, sello, observaciones);

                    codeMap.Add(line[0], basFamilyCode);
                }


            return codeMap;
        }

        private string VacioAGuion(string input)
        {
            var output = input.Trim();
            if (output == "")
                output = "-";
            return output;
        }

        /// <summary>
        /// Genera mapping OEP Code -> BAS Code
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Diccionario (OEP Code, BAS Code)</returns>
        public Dictionary<string, BasElement> GenerateCodeMap(List<List<string>> input, Dictionary<string, string> pfFamilyCodeMap,
                                                                Dictionary<string, string> valveFamilyCodeMap)
        {
            var codeMap = new Dictionary<string, BasElement>();

                var elementTypes = basService.GetAllElementTypes();

                int valveTypeId = elementTypes.First(t => t.Code == "p.valve").Id;

                for (int i = 1; i < input.Count; i++)
                {
                    var line = input[i];

                    string oepCommodityCode = line[19];

                    BasElement element = null;
                    string basFamilyCode = "";
                    string dimensionalCode = "";
                    int typeId = 0;
                    string oepCode = line[0];

                    if (pfFamilyCodeMap.ContainsKey(oepCommodityCode))
                    {
                        string dim1 =  VacioAGuion(line[1]);
                        string unit1 = line[2].Trim();
                        string dim2 = VacioAGuion(line[3]);
                        string unit2 = line[4].Trim();
                        string sched1 = VacioAGuion(line[5]);
                        string unit3 = line[6].Trim();
                        string sched2 = VacioAGuion(line[7]);
                        string unit4 = line[8].Trim();

                        if (unit1 != "")
                            dim1 += "(" + unit1 + ")";
                        if (unit2 != "")
                            dim2 += "(" + unit2 + ")";
                        if (unit3 != "")
                            sched1 += "(" + unit3 + ")";
                        if (unit4 != "")
                            sched2 += "(" + unit4 + ")";

                        basFamilyCode = pfFamilyCodeMap[oepCommodityCode];
                        var descBasCode = GetDescBasCodeByFamilyCode(basFamilyCode);
                        var typeCode = descBasCode.Field.Substring(0, descBasCode.Field.Length - 5);
                        typeId = elementTypes.First(t => t.Code == typeCode).Id;

                        dimensionalCode = GetPfDimensionalCode(descBasCode, dim1, dim2, sched1, sched2);
                        
                    }
                    else if (valveFamilyCodeMap.ContainsKey(oepCommodityCode))
                    {
                        typeId = valveTypeId;
                        string diametro = line[1];
                        string unit = line[2].Trim();
                        if (unit != "")
                            diametro += "(" + unit + ")";

                        basFamilyCode = valveFamilyCodeMap[oepCommodityCode];
                        dimensionalCode = GetValveDimensionalCode(diametro);
                    }

                    element = basService.BuildElementByCode(basFamilyCode + dimensionalCode, typeId, true);

                    codeMap.Add(oepCode, element);
                }


            return codeMap;
        }

        public void CreatePfBasFamilyFile(string inputFile, string outputFile)
        {
            var lines = ReadCsv(inputFile);

            var codeMap = GeneratePfFamilyCodeMap(lines);

            lines[0].Insert(0, "Codigo Familia BAS");
            for (int i = 1; i < lines.Count; i++)
            {
                var line = lines[i];
                string commodityCode = line[0];
                string basFamilyCode = codeMap[commodityCode];

                line.Insert(0, basFamilyCode);
            }

            WriteCsv(outputFile, lines);
        }

        public void CreateValveBasFamilyFile(string inputFile, string outputFile)
        {
            var lines = ReadCsv(inputFile);

            var codeMap = GenerateValveFamilyCodeMap(lines);

            lines[0].Insert(0, "Codigo Familia BAS");
            for (int i = 1; i < lines.Count; i++)
            {
                var line = lines[i];
                string commodityCode = line[0];
                string basFamilyCode = codeMap[commodityCode];

                line.Insert(0, basFamilyCode);
            }

            WriteCsv(outputFile, lines);
        }

        private string GetDim3(BasElement element)
        {
            var field = element.Fields.FirstOrDefault(f => f.FieldDefinition.Code.Contains(".sched1"));
            if (field == null)
                return "";
            var sched1 = field.BasCode.Description;
            field = element.Fields.FirstOrDefault(f => f.FieldDefinition.Code.Contains(".sched2"));
            var sched2 = field.BasCode.Description;
            string dim31 = GetDim(sched1);
            string dim32 = GetDim(sched2);
            if (dim31 != "" && dim32 != "")
                return dim31 + " / " + dim32;
            return  dim31 + dim32;
        }

        private string GetUnit3(BasElement element)
        {
            var field = element.Fields.FirstOrDefault(f => f.FieldDefinition.Code.Contains(".sched1"));
            if (field == null)
                return "";
            var sched1 = field.BasCode.Description;
            field = element.Fields.FirstOrDefault(f => f.FieldDefinition.Code.Contains(".sched2"));
            var sched2 = field.BasCode.Description;
            string unit31 = GetUnit(sched1);
            string unit32 = GetUnit(sched2);
            if (unit31 != "" && unit32 != "")
            {
                if(unit31 != unit32)
                    return unit31 + " / " + unit32;
                return unit31;
            }
                           
            return unit31 + unit32;
        }

        private string GetDim(string dimUnit)
        {
            if (dimUnit == "-")
                return "";
            var i = dimUnit.IndexOf('(');
            if (i == -1)
                return dimUnit;
            return dimUnit.Substring(0, i);                
        }

        private string GetUnit(string dimUnit)
        {
            if (dimUnit == "-")
                return "";
            var i = dimUnit.IndexOf('(');
            if (i == -1)
                return "";
            var j = dimUnit.IndexOf(')');
            return dimUnit.Substring(i+1, j-i-1);
        }

        public void CreateBasCodeFile(string pfFamilyCodeFile, string valveFamilyCodeFile, string oepCodeFile, string outputFile)
        {
            var pfFamilyCodeLines = ReadCsv(pfFamilyCodeFile); 
            var valveFamilyCodeLines = ReadCsv(valveFamilyCodeFile);
            var pfFamilyCodeMap = GeneratePfFamilyCodeMap(pfFamilyCodeLines);
            var valveFamilyCodeMap = GenerateValveFamilyCodeMap(valveFamilyCodeLines);

            var oepCodeLines = ReadCsv(oepCodeFile);

            var codeMap = GenerateCodeMap(oepCodeLines, pfFamilyCodeMap, valveFamilyCodeMap);

            oepCodeLines[0].Insert(0, "Unit 3");
            oepCodeLines[0].Insert(0, "Dim 3");
            oepCodeLines[0].Insert(0, "Descripcion BAS");
            oepCodeLines[0].Insert(0, "Descripcion Corta BAS");
            oepCodeLines[0].Insert(0, "Codigo Dimensional BAS");
            oepCodeLines[0].Insert(0, "Codigo Familia BAS");

            for (int i = 1; i < oepCodeLines.Count; i++)
            {
                var line = oepCodeLines[i];

                string oepCode = line[0];
                var element = codeMap[oepCode];

                if (element != null)
                {
                    line.Insert(0, GetUnit3(element));
                    line.Insert(0, GetDim3(element));
                    line.Insert(0, element.FamilyDescription);
                    line.Insert(0, element.ShortFamilyDescription);
                    line.Insert(0, element.DimensionalCode);
                    line.Insert(0, element.FamilyCode);
                }
                else
                {
                    line.Insert(0, "");
                    line.Insert(0, "");
                    line.Insert(0, "");
                    line.Insert(0, "");
                    line.Insert(0, "");
                    line.Insert(0, "");
                }

            }

            WriteCsv(outputFile, oepCodeLines);
        }


        public void CheckOepPfFamilyFile(string inputFile, string outputFile)
        {
            var errors = new List<string>();
            var lines = ReadCsv(inputFile);



                for (int i = 1; i < lines.Count; i++)
                {
                    var line = lines[i];
                    string commodity = line[0];
                    string desc = line[1];
                    string estandar = line[2];
                    string serie1 = line[3];
                    string serie2 = line[4];
                    string material = line[5];
                    string extremo1 = line[6];
                    string extremo2 = line[7];


                    string code;
                    string error;

                    var family = GetPfFamilyTypeByDesc(desc);


                    code = basService.GetCodeByDesc(family + ".desc", desc);
                    error = family + ".desc: " + desc + " (" + commodity + ")";
                    if (code == null && !errors.Contains(error))
                        errors.Add(error);

                    code = basService.GetCodeByDesc(family + ".estandar", estandar);
                    error = family + ".estandar: " + estandar + " (" + commodity + ")";
                    if (code == null && !errors.Contains(error))
                        errors.Add(error);

                    code = basService.GetCodeByDesc(family + ".material", material);
                    error = family + ".material: " + material + " (" + commodity + ")";
                    if (code == null && !errors.Contains(error))
                        errors.Add(error);

                    code = basService.GetCodeByDesc(family + ".extremo1", extremo1);
                    error = family + ".extremo1: " + extremo1 + " (" + commodity + ")";
                    if (code == null && !errors.Contains(error))
                        errors.Add(error);

                    code = basService.GetCodeByDesc(family + ".extremo2", extremo2);
                    error = family + ".extremo2: " + extremo2 + " (" + commodity + ")";
                    if (code == null && !errors.Contains(error))
                        errors.Add(error);

                    code = basService.GetCodeByDesc(family + ".serie1", serie1);
                    error = family + ".serie1: " + serie1 + " (" + commodity + ")";
                    if (code == null && !errors.Contains(error))
                        errors.Add(error);

                    code = basService.GetCodeByDesc(family + ".serie2", serie2);
                    error = family + ".serie2: " + serie2 + " (" + commodity + ")";
                    if (code == null && !errors.Contains(error))
                        errors.Add(error);
                }


            var errorLines = new List<List<string>>();
            foreach (var error in errors.OrderBy(s => s))
            {
                errorLines.Add(new List<string>(){error});
            }

            WriteCsv(outputFile, errorLines);
        }


        public void CheckOepValveFamilyFile(string inputFile, string outputFile)
        {
            var errors = new List<string>();
            var lines = ReadCsv(inputFile);



                for (int i = 1; i < lines.Count; i++)
                {
                    var line = lines[i];
                    string desc = line[1];
                    string estandar = line[2];
                    string serie = line[3];
                    string extremo = line[4];
                    string operacion = line[5];
                    string material = line[6];
                    string trim = line[7];
                    string sello = line[8];
                    string observaciones = line[9];

                    string code;

                    code = basService.GetCodeByDesc("p.valve.desc", desc);
                    if (code == null && !errors.Contains("desc: " + desc))
                        errors.Add("desc: " + desc);

                    code = basService.GetCodeByDesc("p.valve.estandar", estandar);
                    if (code == null && !errors.Contains("estandar: " + estandar))
                        errors.Add("estandar: " + estandar);

                    code = basService.GetCodeByDesc("p.valve.serie", serie);
                    if (code == null && !errors.Contains("serie: " + serie))
                        errors.Add("serie: " + serie);

                    code = basService.GetCodeByDesc("p.valve.extremo", extremo);
                    if (code == null && !errors.Contains("extremo: " + extremo))
                        errors.Add("extremo: " + extremo);

                    code = basService.GetCodeByDesc("p.valve.operacion", operacion);
                    if (code == null && !errors.Contains("operacion: " + operacion))
                        errors.Add("operacion: " + operacion);

                    code = basService.GetCodeByDesc("p.valve.material", material);
                    if (code == null && !errors.Contains("material: " + material))
                        errors.Add("material: " + material);

                    code = basService.GetCodeByDesc("p.valve.trim", trim);
                    if (code == null && !errors.Contains("trim: " + trim))
                        errors.Add("trim: " + trim);

                    code = basService.GetCodeByDesc("p.valve.sello", sello);
                    if (code == null && !errors.Contains("sello: " + sello))
                        errors.Add("sello: " + sello);

                    code = basService.GetCodeByDesc("p.valve.observaciones", observaciones);
                    if (code == null && !errors.Contains("observaciones: " + observaciones))
                        errors.Add("observaciones: " + observaciones);
                }


            var errorLines = new List<List<string>>();
            foreach (var error in errors.OrderBy(s => s))
            {
                errorLines.Add(new List<string>() { error });
            }

            WriteCsv(outputFile, errorLines);
        }


        public void CheckOepCodeFile(string pfFamilyCodeFile, string valveFamilyCodeFile, string oepCodeFile, string outputFile)
        {
            var errors = new List<string>();
            var oepCodeByError = new Dictionary<string, string>();
            var pfFamilyCodeLines = ReadCsv(pfFamilyCodeFile);
            var pfFamilyCodeMap = GeneratePfFamilyCodeMap(pfFamilyCodeLines);

            var valveFamilyCodeLines = ReadCsv(valveFamilyCodeFile);
            var valveFamilyCodeMap = GenerateValveFamilyCodeMap(valveFamilyCodeLines);

            var oepCodeLines = ReadCsv(oepCodeFile);


                for (int i = 1; i < oepCodeLines.Count; i++)
                {
                    var line = oepCodeLines[i];
                    if(line.Count < 20)
                        throw new RocaException("Linea invalida: " + oepCodeLines[i]);
                    string oepCommodityCode = line[19];

                    string code;


                    if (!pfFamilyCodeMap.ContainsKey(oepCommodityCode) && !valveFamilyCodeMap.ContainsKey(oepCommodityCode)  
                        && !errors.Contains("OEP Commodity Code: " + oepCommodityCode))
                        errors.Add("OEP Commodity Code: " + oepCommodityCode);

                    if (pfFamilyCodeMap.ContainsKey(oepCommodityCode))
                    {
                        string dim1 = VacioAGuion(line[1]);
                        string unit1 = line[2].Trim();
                        string dim2 = VacioAGuion(line[3]);
                        string unit2 = line[4].Trim();
                        string sched1 = VacioAGuion(line[5]);
                        string unit3 = line[6].Trim();
                        string sched2 = VacioAGuion(line[7]);
                        string unit4 = line[8].Trim();

                        if (unit1 != "")
                            dim1 += "(" + unit1 + ")";
                        if (unit2 != "")
                            dim2 += "(" + unit2 + ")";
                        if (unit3 != "")
                            sched1 += "(" + unit3 + ")";
                        if (unit4 != "")
                            sched2 += "(" + unit4 + ")";

                        var basFamilyCode = pfFamilyCodeMap[oepCommodityCode];
                        var descBasCode = GetDescBasCodeByFamilyCode(basFamilyCode);
                        var type = descBasCode.Field.Substring(0, descBasCode.Field.Length - 5);

                        string error = type + ".dim1: " + dim1;
                        code = basService.GetCodeByDesc(type + ".dim1", dim1);
                        if (code == null && !errors.Contains(error))
                        {
                            errors.Add(error);
                            oepCodeByError.Add(error, line[0]);
                        }

                        error = type + ".dim2: " + dim2;
                        code = basService.GetCodeByDesc(type + ".dim2", dim2);
                        if (code == null && !errors.Contains(error))
                        {
                            errors.Add(error);
                            oepCodeByError.Add(error, line[0]);
                        }

                        error = type + ".sched1: " + sched1;
                        code = basService.GetCodeByDesc(type + ".sched1", sched1);
                        if (code == null && !errors.Contains(error))
                        {
                            errors.Add(error);
                            oepCodeByError.Add(error, line[0]);
                        }

                        error = type + ".sched2: " + sched2;
                        code = basService.GetCodeByDesc(type + ".sched2", sched2);
                        if (code == null && !errors.Contains(error))
                        {
                            errors.Add(error);
                            oepCodeByError.Add(error, line[0]);
                        }
                    }
                    else if (valveFamilyCodeMap.ContainsKey(oepCommodityCode))
                    {
                        string diametro = line[1];
                        string unit = line[2].Trim();
                        if (unit != "")
                            diametro += "(" + unit + ")";

                        var error = "p.valve.diametro: " + diametro;
                        code = basService.GetCodeByDesc("p.valve.diametro", diametro);
                        if (code == null && !errors.Contains(error))
                        {
                            errors.Add(error);
                            oepCodeByError.Add(error, line[0]);
                        }
                    }

                }


            var errorLines = new List<List<string>>();
            foreach (var error in errors.OrderBy(s => s))
            {
                string ej = "";
                if (!error.Contains("OEP Commodity Code"))
                    ej = "  (ej: " + oepCodeByError[error] + ")";
                errorLines.Add(new List<string>() { error +  ej});
            }

            WriteCsv(outputFile, errorLines);
        }


        public void Dispose()
        {
            uow.Dispose();
        }
    }
}
