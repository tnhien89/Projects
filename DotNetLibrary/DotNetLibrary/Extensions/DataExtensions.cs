using DotNetLibrary.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DotNetLibrary.Extensions
{
    public static class DataExtensions
    {
        public static IList<T> ToList<T>(this DataTable table) where T: new()
        {
            List<T> list = new List<T>();
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    T item = row.ToObject<T>();
                    list.Add(item);
                }
            }

            return list;
        }

        public static ArrayList ToArrayList(this DataSet ds, Type[] types)
        {
            ArrayList result = new ArrayList();
            if (ds == null)
            {
                return result;
            }

            for (int i = 0; i < ds.Tables.Count; i++)
            { 
                DataTable table = ds.Tables[i];
                Type type = types[i];
                List<object> list = new List<object>();
                foreach (DataRow row in table.Rows)
                {
                    object item = row.ToObject(type);
                    list.Add(item);
                }

                result.Add(list);
            }

            return result;
        }

        public static T ToObject<T>(this DataRow row) where T: new()
        {
            T result = new T();
            if (row != null)
            {
                SetValueForObject<T>(result, row);
            }

            return result;
        }

        public static object ToObject(this DataRow row, Type type)
        {
            object result = Activator.CreateInstance(type);
            if (row != null)
            {
                List<PropertyInfo> listProperty = new List<PropertyInfo>(type.GetProperties());

                foreach (PropertyInfo prop in listProperty)
                {
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
                        prop.SetValue(result, null, null);
                        continue;
                    }

                    try
                    {
                        object val = Convert.ChangeType(row[colName], prop.PropertyType);
                        prop.SetValue(result, val, null);
                    }
                    catch
                    {
                        prop.SetValue(result, row[colName], null);
                    }
                }
            }

            return result;
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