using System.Text;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class BasElementEi : BasElement
    {
        protected virtual string AppendSeparator(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";
            if (text[text.Length - 1] == 'x' || text[text.Length - 1] == ':')
                return text + " ";
            if (text[text.Length - 1] == '*')
                return text.Substring(0, text.Length - 1) + " ";
            return text + ", ";
        }

        protected override string GetFieldDescription(BasCodeField field)
        {
            if (field.BasCode.Description == "-")
                return "";
            string desc;
            if (field.FieldDefinition.Type == BasFieldType.Dimensional)
                desc = field.FieldDefinition.Name + ": " + field.BasCode.Description.Replace("(", "").Replace(")", "");
            else
                desc = field.BasCode.Description;
            return AppendSeparator(desc);
        }


        protected override string GetFieldShortDescription(BasCodeField field)
        {
            if (field.BasCode.ShortDescription == "-" || string.IsNullOrEmpty(field.BasCode.ShortDescription))
                return "";
            string desc;
            if (field.FieldDefinition.Type == BasFieldType.Dimensional)
                desc = field.FieldDefinition.Name + ": " + field.BasCode.ShortDescription.Replace("(", "").Replace(")", "");
            else
                desc = field.BasCode.ShortDescription;
            return AppendSeparator(desc);
        }


    }
}
