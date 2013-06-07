using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using MyFramework.Data.ORM;
using System.IO;

namespace MyFramework.Utility
{
    public class ConvertUtility
    {

        public static DataTable ConvertToDT<T>(ICollection<T> Value) where T:BaseEntity
        {

            Type type = typeof(T);

            PropertyInfo[] pis = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            DataTable dt = new DataTable();
            dt.TableName = type.Name;

            foreach (PropertyInfo pi in pis)
            {
                DataColumn dc = new DataColumn(pi.Name);
                dt.Columns.Add(dc);
            }

            foreach (T obj in Value)
            {
                DataRow dr = dt.Rows.Add();

                foreach (DataColumn tmdc in dt.Columns)
                {
                    object value = obj[tmdc.ColumnName];
                    if (value == null) value = DBNull.Value;
                    dr[tmdc] = value;
                }
            }

            return dt;
        }

        public static string ConvertToCSV<T>(ICollection<T> Value,bool needhead) where T : BaseEntity
        {
            StringBuilder sb = new StringBuilder();
            Type type = typeof(T);

            PropertyInfo[] pis = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            if (needhead)
            {
                //string filename = type.Name + ".csv";
                string head = string.Empty;
                foreach (PropertyInfo pi in pis)
                {
                    head += pi.Name + ",";
                }
                head = head.Remove(head.Length - 1, 1);

                sb = sb.AppendLine(head);
            }
            string row = string.Empty;

            foreach (T obj in Value)
            {
                row = string.Empty;
                foreach (PropertyInfo pi in pis)
                {
                    if ((!Convert.IsDBNull(obj[pi.Name])) && (obj[pi.Name] != null))
                    {
                        row += obj[pi.Name].ToString() + ",";
                    }
                    else
                    {
                        row += ",";
                    }
                }
                row = row.Remove(row.Length - 1, 1);
                sb = sb.AppendLine(row);
            }

            return sb.ToString();
        }

        public static object ConvertTo<T>(object value)
        {
            Type type = typeof(T);
            switch (type.ToString())
            {
                case "System.Nullable`1[System.Int32]": return Convert.ToInt32(value);
                case "System.Nullable`1[System.Double]": return Convert.ToDouble(value);
                case "System.Nullable`1[System.Decimal]": return Convert.ToDecimal(value);
                case "System.Nullable`1[System.Boolean]": value = Convert.ToInt32(value); return Convert.ToBoolean(value);
                case "System.Int32": return Convert.ToInt32(value);
                case "System.String": return Convert.ToString(value);
                case "System.Double": return Convert.ToDouble(value);
                case "System.DateTime": return Convert.ToDateTime(value);
                case "System.Decimal": return Convert.ToDecimal(value);
                case "System.Boolean": value = Convert.ToInt32(value); return Convert.ToBoolean(value);
            }
            return value;
        }


        public static string Obj2Json<T>(T data)
        {

            try
            {

                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(data.GetType());

                using (MemoryStream ms = new MemoryStream())
                {

                    serializer.WriteObject(ms, data);

                    return Encoding.UTF8.GetString(ms.ToArray());

                }

            }

            catch
            {

                return null;

            }

        }
    }
}
