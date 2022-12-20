using FastDeploy.Utilities.Attributes;
using System.Collections;
using System.Reflection;

namespace FastDeploy.Utilities.Extensions
{
    public static class ObjectExtensions
    {
        public static T ConvertTo<T>(this object obj) where T : new()
        {
            Type type = typeof(T);
            List<PropertyInfo> properties = new(type.GetProperties());

            T result = new();
            foreach (PropertyInfo prop in properties)
            {
                if (!prop.CanWrite)
                {
                    continue;
                }

                PropertyInfo? pr = obj.GetType().GetProperty(prop.Name);
                if (pr == null)
                {
                    prop.SetValue(result, null, null);
                    continue;
                }

                try
                {
                    object? val = Convert.ChangeType(pr.GetValue(obj, null), prop.PropertyType);
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
            List<PropertyInfo> listProperty = new(type.GetProperties());
            Hashtable result = new();

            foreach (PropertyInfo prop in listProperty)
            {
                //result.Add(prop.Name, prop.GetValue(obj, null));
                object? val = prop.GetValue(obj, null);
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

                object? colMapping = attrs.FirstOrDefault(a => a.GetType() == typeof(DbColumnAttribute));
                string colName = "";
                if (colMapping != null)
                {
                    colName = ((DbColumnAttribute)colMapping).Name;
                }
                else
                {
                    colName = prop.Name;
                }
                if (result.ContainsKey(colName)) continue;
                result.Add(colName, val);
            }

            return result;
        }
    }
}
