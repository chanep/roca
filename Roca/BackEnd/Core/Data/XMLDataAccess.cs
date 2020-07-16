using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Cno.Roca.Core.Data
{
    public abstract class XMLDataAccess<K,T> where T : new()
    {
        private string _fileName;
        private Dictionary<K, T> _dictionary = new Dictionary<K,T>();

        public abstract string IdField
        {
            get;
        }

        public abstract string[] Fields
        {
            get;
        }


        public XMLDataAccess(string fileName)
        {
            _fileName = fileName;
            CreateDictionary();
        }

        public T Get(K id)
        {
            return _dictionary[id];
        }

        public List<T> GetAll()
        {
            List<T> list = new List<T>();
            foreach (T t in _dictionary.Values)
                list.Add(t);
            return list;
        }

        public Dictionary<K, T> GetAllAsDictionary()
        {
            return _dictionary;
        }


        private void CreateDictionary()
        {
            DataTable table = CreateTable();
            foreach (DataRow row in table.Rows)
            {
                T t = new T();
                foreach (string field in Fields)
                {
                    System.Reflection.PropertyInfo pi = t.GetType().GetProperty(field);
                    object value = ConvertToType(row[field].ToString(), pi.PropertyType);
                    pi.SetValue(t, value, null);
                }
                K id = (K) ConvertToType(row[IdField].ToString(), typeof(K));
                _dictionary.Add(id, t);

            }

        }

        private DataTable CreateTable()
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(_fileName);
            return dataSet.Tables[0];
        }


        private object ConvertToType(string value, Type type)
        {
            if(type == typeof(string))
                return value;
            else if (type == typeof(double))
                return Convert.ToDouble(value);
            else if (type == typeof(int))
                return Convert.ToInt32(value);
            throw new InvalidCastException("Fields must be either int or string or double");

        }



    }
}
