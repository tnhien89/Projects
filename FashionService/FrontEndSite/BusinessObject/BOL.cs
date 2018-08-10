using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndSite.BusinessObject
{
    public class BOL
    {
        public BOL()
        { 
        
        }

        public BOL(DataRow row)
        {
            List<PropertyInfo> listProperty = this.GetProperties();
            foreach (PropertyInfo prop in listProperty)
            {
                if (!row.Table.Columns.Contains(prop.Name) ||
                    row[prop.Name] == null || 
                    row[prop.Name] == DBNull.Value)
                {
                    prop.SetValue(this, null);
                    continue;
                }

                try
                {
                    object value = Convert.ChangeType(row[prop.Name], prop.PropertyType);
                    prop.SetValue(this, value);
                }
                catch
                {
                    prop.SetValue(this, null);
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
                result.Add(prop.Name, prop.GetValue(this));
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
