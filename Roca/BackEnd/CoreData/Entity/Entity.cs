using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Cno.Roca.CoreData.Entity
{
    [Serializable]
    public abstract class Entity<K> : Entity
    {
        public K Id { get; set; }

    }

    [Serializable]
	public abstract class Entity : ICloneable, IEditableObject
    {
        private const int MaxToStringDepth = 1;

        public override string ToString()
        {
            return InternalToString(0, 0, true);
        }

        public string ToString(int depth)
        {
            return InternalToString(0, depth, false);
        }

        protected string InternalToString(int indentation, int depth, bool onlyValueTypes)
		{
            if (indentation > depth)
                return "";
			try
			{

				string result = "";
			    string strIndent = "";
			    if (indentation > 0)
                    result = Environment.NewLine;
			    for (int i = 0; i < indentation; i++)
			        strIndent = "\t" + strIndent;
                    
				Type type = GetType();
				var properties = type.GetProperties().Where(p => p.Name == "Id").ToList();
			    var properties2 = type.GetProperties().Where(p => p.Name != "Id").ToList();
			    properties.AddRange(properties2);
				foreach (PropertyInfo property in properties)
				{
				    var value = property.GetValue(this, null);
                    if(onlyValueTypes && !IsValueType(property.PropertyType))
				        continue;
					if (!(value is PrimaryKey))
					{
						string strValue = "null";
					    if (value != null)
					    {

                            if (indentation < depth &&
                                value.GetType().IsGenericType && 
                                IsAssignableToGenericType(value.GetType(), typeof (ICollection<>)) &&
					            value.GetType().GetGenericArguments()[0].IsSubclassOf(typeof (Entity)))
                            {

					            strValue = Environment.NewLine;
					            int n = 0;
					            foreach (var item in (IEnumerable)value)
					            {
                                    strValue = strValue + strIndent + "\t" + "[" + n + "]" + ((Entity)item).InternalToString(indentation + 1, depth, false);
					                n++;
					            }

					            strValue = strValue + Environment.NewLine;

					        }
					        else
					        {
					            if (value is Entity)
                                    strValue = ((Entity)value).InternalToString(indentation + 1, depth, false).TrimEnd();      
					            else
					                strValue = value.ToString().TrimEnd();
					        }

					    }

                        result = result + strIndent + property.Name + ": " + strValue + Environment.NewLine;
					}
				}
				return result;
			}
			catch
			{
				return base.ToString();
			}

		}

        protected bool IsValueType(Type type)
        {
            if(type.IsSubclassOf(typeof(Entity)))
                return false;
            if (type.IsGenericType &&
                IsAssignableToGenericType(type, typeof (ICollection<>)) &&
                type.GetGenericArguments()[0].IsSubclassOf(typeof (Entity)))
                return false;
            return true;
        }

        protected static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            if (genericType.IsAssignableFrom(givenType))
                return true;

            if (givenType.IsGenericType)
                if (givenType.GetGenericTypeDefinition() == genericType) return true;

            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
                if (it.IsGenericType)
                    if (it.GetGenericTypeDefinition() == genericType) return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return baseType.IsGenericType &&
                baseType.GetGenericTypeDefinition() == genericType ||
                IsAssignableToGenericType(baseType, genericType);
        }

		#region ICloneable Members

		public object Clone()
		{
			return MemberwiseClone();
		}

		#endregion

		public override bool Equals(object obj)
		{
		    if (obj == null)
		        return false;
		    if (!(obj is Entity))
		        return false;
		    var str = ToString();
		    var str2 = ((Entity) obj).ToString();
			return (str == str2);
		}

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

		public void BeginEdit()
		{
			Original = (from property in Properties
						where property.GetSetMethod() != null
						select property.GetValue(this, null))
						.ToArray();
		}

		public void EndEdit()
		{
			Original = null;
		}

		public void CancelEdit()
		{
			if (Original != null)
			{
				for (var i = 0; i < Properties.Length; i++)
				{
					Properties[i].SetValue(this, Original[i], null);
				}
			}
		}

		PropertyInfo[] Properties
		{
			get { return (GetType()).GetProperties(BindingFlags.Public | BindingFlags.Instance); }
		}

		object[] Original;
	}
}