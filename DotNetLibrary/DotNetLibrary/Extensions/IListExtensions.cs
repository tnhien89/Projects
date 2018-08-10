using DotNetLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DotNetLibrary.Extensions
{
    public static class IListExtensions
    {
        public static IList<T> CreateListFromTable<T>(this DataTable table) where T : new()
        {
            List<T> list = new List<T>();
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    T item = new T();
                    SetValueForObject<T>(item, row);

                    list.Add(item);
                }
            }

            return list;
        }

        private static void SetValueForObject<T>(T obj, DataRow row) where T : new()
        {
            Type type = typeof(T);
            List<PropertyInfo> properties = new List<PropertyInfo>(type.GetProperties());

            foreach (PropertyInfo prop in properties)
            {
                if (!prop.CanWrite)
                {
                    continue;
                }

                object[] attrs = prop.GetCustomAttributes(false);
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

                if (!row.Table.Columns.Contains(colName) || row[colName] == null || row[colName] == DBNull.Value)
                {
                    prop.SetValue(obj, null, null);
                    continue;
                }

                try
                {
                    object val = Convert.ChangeType(row[colName], prop.PropertyType);
                    prop.SetValue(obj, val, null);
                }
                catch
                {
                    prop.SetValue(obj, row[colName], null);
                }
            }
        }
    }
}
