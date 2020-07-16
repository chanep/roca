using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Cno.Roca.CoreData.Entity
{
    [Serializable]
    public abstract class PrimaryKey
    {
        public override bool Equals(object obj)
        {
            if ((this.GetType() == obj.GetType()) && (this.ToString() == obj.ToString()))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            try
            {
                string result = "";
                Type type = this.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    string valor = "null";
                    if (property.GetValue(this, null) != null)
                        valor = property.GetValue(this, null).ToString().Trim();
                    result = result + property.Name + ": " + valor + " - ";

                }
                return result;
            }
            catch
            {
                return base.ToString();
            }

        }
    }
}