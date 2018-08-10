using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MvcFrontEnd.BusinessObjects
{
    public class BusinessObjectBase
    {
        public BusinessObjectBase()
        {

        }

        public BusinessObjectBase(DataRow row)
        {
            List<PropertyInfo> listProperty = this.GetProperties();
            foreach (PropertyInfo prop in listProperty)
            {
                if (!prop.CanWrite)
                {
                    continue;
                }

                if (!row.Table.Columns.Contains(prop.Name) ||
                    row[prop.Name] == null ||
                    row[prop.Name] == DBNull.Value)
                {
                    prop.SetValue(this, null, null);
                    continue;
                }

                try
                {
                    object value = Convert.ChangeType(row[prop.Name], prop.PropertyType);
                    prop.SetValue(this, value, null);
                }
                catch
                {
                    prop.SetValue(this, null, null);
                }
            }
        }

        public virtual Hashtable GetParameters()
        {
            Type type = this.GetType();
            List<PropertyInfo> listProperty = new List<PropertyInfo>(type.GetProperties());

            Hashtable result = new Hashtable();
            foreach (PropertyInfo prop in listProperty)
            {
                result.Add(prop.Name, prop.GetValue(this, null));
            }

            return result;
        }

        public List<PropertyInfo> GetProperties()
        {
            Type type = this.GetType();
            List<PropertyInfo> listProperty = new List<PropertyInfo>(type.GetProperties());

            return listProperty;
        }
    }
}