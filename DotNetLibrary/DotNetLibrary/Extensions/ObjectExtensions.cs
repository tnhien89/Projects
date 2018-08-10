using DotNetLibrary.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotNetLibrary.Extensions
{
    public static class ObjectExtensions
    {
        public static T ConvertTo<T>(this object obj) where T: new()
        {
            Type type = typeof(T);
            List<PropertyInfo> properties = new List<PropertyInfo>(type.GetProperties());

            T result = new T();
            foreach (PropertyInfo prop in properties)
            {
                if (!prop.CanWrite)
                {
                    continue;
                }

                PropertyInfo pr = obj.GetType().GetProperty(prop.Name);
                if (pr == null)
                {
                    prop.SetValue(result, null, null);
                    continue;
                }

                try
                {
                    object val = Convert.ChangeType(pr.GetValue(obj, null), prop.PropertyType);
                    prop.SetValue(result, val, null);
                }
                catch
                {
                    prop.SetValue(result, pr.GetValue(obj, null), null);
                }
            }

            return result;
        }

        public static Hashtable GetParameters(this object obj)
        {
            Type type = obj.GetType();
            List<PropertyInfo> listProperty = new List<PropertyInfo>(type.GetProperties());
            Hashtable result = new Hashtable();
            foreach (PropertyInfo prop in listProperty)
            {
                //result.Add(prop.Name, prop.GetValue(obj, null));
                object val = prop.GetValue(obj, null);
                if (prop.PropertyType == typeof(DateTime))
                {
                    if (val == null || (DateTime)val == DateTime.MinValue)
                    {
                        continue;
                    }
                }

                var attrs = prop.GetCustomAttributes(false);
                if (attrs.FirstOrDefault(a => a.GetType() == typeof(IgnoreParamAttribute)) != null)
                {
                    continue;
                }

                object colMapping = attrs.FirstOrDefault(a => a.GetType() == typeof(DbColumnAttribute));
                string colName = "";
                if (colMapping != null)
                {
                    colName = (colMapping as DbColumnAttribute).Name;
                }
                else
                {
                    colName = prop.Name;
                }

                result.Add(colName, val);
            }

            return result;
        }
    }
}
