using FastDeploy.Utilities.Attributes;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace FastDeploy.Utilities.Extensions
{
    public static class DataExtensions
    {
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            try
            {
                List<T> list = new();
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
            catch (Exception)
            {
                throw;
            }

        }

        public static T ToObject<T>(this DataRow row) where T : new()
        {
            try
            {
                T result = new();
                if (row != null)
                {
                    SetValueForObject<T>(result, row);
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static object? ToObject(this DataRow row, Type type)
        {
            object? result = Activator.CreateInstance(type);
            if (row != null && result != null)
            {
                List<PropertyInfo> listProperty = new(type.GetProperties());

                foreach (PropertyInfo prop in listProperty)
                {
                    object[] attrs = prop.GetCustomAttributes(false);
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

                    if (!row.Table.Columns.Contains(colName) || row[colName] == null || row[colName] == DBNull.Value)
                    {
                        prop.SetValue(result, null, null);
                        continue;
                    }

                    try
                    {
                        object? val = row[colName].GetType() == typeof(System.Guid) ? row[colName].ToString() : Convert.ChangeType(row[colName], prop.PropertyType);
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
            try
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

                    if (!row.Table.Columns.Contains(colName) || row[colName] == null || row[colName] == DBNull.Value)
                    {
                        prop.SetValue(obj, null, null);
                        continue;
                    }

                    try
                    {
                        object? val = null;
                        if (row[colName].GetType() == typeof(System.Guid))
                        {
                            val = row[colName].ToString();
                        }
                        else if (prop.PropertyType.IsEnum || 
                            (prop.PropertyType.FullName != null && prop.PropertyType.FullName.Contains("System.DateTime")))
                        {
                            val = row[colName];
                        }
                        else
                        {
                            val = Convert.ChangeType(row[colName], prop.PropertyType);
                        }

                        if (prop.PropertyType.IsEnum && val != null && !int.TryParse(val.ToString(), out int enumInt))
                            prop.SetValue(obj, Enum.Parse(prop.PropertyType, val.ToString() ?? ""));
                        else
                            prop.SetValue(obj, val, null);
                    }
                    catch
                    {
                        prop.SetValue(obj, row[colName], null);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static ArrayList ToArrayList(this DataSet ds, Type[] types)
        {
            ArrayList result = new();
            if (ds == null)
            {
                return result;
            }

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataTable table = ds.Tables[i];
                Type type = types[i];
                List<object> list = new();
                foreach (DataRow row in table.Rows)
                {
                    object? item = row.ToObject(type);

                    if (item != null)
                    {
                        list.Add(item);
                    }
                }

                result.Add(list);
            }

            return result;
        }
    }
}
