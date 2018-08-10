using FrontEnd.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FrontEnd
{

    public class BaseObject
    {
        private const string __tag = "[BaseObject]";
        public BaseObject()
        { }

        public BaseObject(object obj)
        {
            if (obj.GetType() == typeof(DataRow))
            {
                this.CreateBaseObject((DataRow)obj);
                return;
            }

            try
            {
                foreach (PropertyInfo prop in this.GetProperties())
                {
                    if (obj.GetType().GetProperty(prop.Name) == null)
                    {
                        prop.SetValue(this, null);
                        return;
                    }

                    prop.SetValue(this, obj.GetType().GetProperty(prop.Name).GetValue(obj));
                }
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
            }
        }

        public void SetValueForProperty(string name, DataRow row, string columnName)
        {
            try
            {
                PropertyInfo prop = this.GetType().GetProperty(name);
                if (!row.Table.Columns.Contains(columnName) ||
                    row[columnName] == null ||
                    row[columnName] == DBNull.Value)
                {
                    prop.SetValue(this, null);
                }
                else
                {
                    prop.SetValue(this, row[columnName]);
                }
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
            }
        }

        private void CreateBaseObject(DataRow row)
        {
            try
            {
                foreach (PropertyInfo prop in this.GetProperties())
                {
                    var attributes = prop.GetCustomAttributes(false);
                    string colName = prop.Name;

                    var colMapping = attributes.FirstOrDefault(a => a.GetType() == typeof(DbColumnAttribute));
                    if (colMapping != null)
                    {
                        colName = (colMapping as DbColumnAttribute).Name;
                    }

                    if (!row.Table.Columns.Contains(colName))
                    {
                        continue;
                    }

                    if (row[colName] == DBNull.Value)
                    {
                        prop.SetValue(this, null);
                        continue;
                    }

                    try
                    {
                        prop.SetValue(this, row[colName]);
                    }
                    catch
                    {
                        prop.SetValue(this, Convert.ChangeType(row[colName], prop.PropertyType));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
            }
        }

        public List<PropertyInfo> GetProperties()
        {
            List<PropertyInfo> result = new List<PropertyInfo>(this.GetType().GetProperties());

            return result;
        }

        public virtual Hashtable GetParameters()
        {
            Type type = this.GetType();
            List<PropertyInfo> listProperty = new List<PropertyInfo>(type.GetProperties());

            Hashtable result = new Hashtable();
            foreach (PropertyInfo prop in listProperty)
            {
                result.Add(prop.Name, prop.GetValue(this));
            }

            return result;
        }
    }
}